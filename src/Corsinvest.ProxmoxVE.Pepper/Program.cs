/*
 * SPDX-FileCopyrightText: Copyright Corsinvest Srl
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Corsinvest.ProxmoxVE.Api.Extension;
using Corsinvest.ProxmoxVE.Api.Extension.Utils;
using Corsinvest.ProxmoxVE.Api.Shared.Models.Vm;
using Corsinvest.ProxmoxVE.Api.Shell.Helpers;

namespace Corsinvest.ProxmoxVE.Pepper
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var app = ConsoleHelper.CreateApp("cv4pve-pepper", "Launching SPICE on Proxmox VE");
            var optVmId = app.VmIdOrNameOption();

            var optProxy = app.AddOption("--proxy",
                                         @"SPICE proxy server. This can be used by the client to specify the proxy server." +
                                         " All nodes in a cluster runs 'spiceproxy', so it is up to the client to choose one." +
                                         " By default, we return the node to connect." +
                                         " If specify http(s)://[host]:[port] then replace proxy option in file .vv. E.g. for reverse proxy.");

            var optRemoteViewer = app.AddOption("--viewer", "Executable SPICE client remote viewer.").AddValidatorExistFile();
            optRemoteViewer.IsRequired = true;

            var optViewerOptions = app.AddOption("--viewer-options", "Send options directly SPICE Viewer (quote value).");

            var optStartOrResume = app.AddOption<bool>("--start-or-resume", "Run stopped or paused VM");

            var optWaitForStartup = app.AddOption<int>("--wait-for-startup", "Wait sec. for startup VM");
            optWaitForStartup.SetDefaultValue(5);

            app.SetHandler(async (InvocationContext ctx) =>
            {
                var loggerFactory = ConsoleHelper.CreateLoggerFactory<Program>(app.GetLogLevelFromDebug());
                var client = await app.ClientTryLogin(loggerFactory);

                var proxy = optProxy.GetValue();
                if (string.IsNullOrWhiteSpace(proxy)) { proxy = client.Host; }

                var vm = await client.GetVm(optVmId.GetValue());

                if (optStartOrResume.GetValue() && (vm.IsStopped || vm.IsPaused))
                {
                    var status = vm.IsStopped ? VmStatus.Start : VmStatus.Resume;

                    if (app.DebugIsActive())
                    {
                        await Console.Out.WriteLineAsync($"VM is {(vm.IsStopped ? "stopped" : "paused")}. {status} now!");
                    }

                    //start VM
                    var result = await VmHelper.ChangeStatusVm(client, vm.Node, vm.VmType, vm.VmId, status);
                    await client.WaitForTaskToFinish(result, timeout: optWaitForStartup.GetValue() * 1000);

                    //check VM is running
                    vm = await client.GetVm(optVmId.GetValue());
                    if (app.DebugIsActive()) { await Console.Out.WriteLineAsync($"VM is {vm.Status}."); }
                }

                var (success, reasonPhrase, content) = await VmHelper.GetQemuSpiceFileVV(client, vm.Node, vm.VmId, proxy);
                if (success)
                {
                    //proxy force
                    if (new Regex(@"^(http|https|)://.*$").IsMatch(proxy))
                    {
                        var lines = content.Split("\n");
                        for (int i = 0; i < lines.Length; i++)
                        {
                            if (lines[i].StartsWith("proxy="))
                            {
                                lines[i] = $"proxy={proxy}";
                                break;
                            }
                        }
                        content = string.Join("\n", lines);

                        if (app.DebugIsActive())
                        {
                            await Console.Out.WriteLineAsync($"Replace Proxy: {proxy}");
                            await Console.Out.WriteLineAsync(content);
                        }
                    }

                    var fileName = Path.GetTempFileName().Replace(".tmp", ".vv");
                    File.WriteAllText(fileName, content);
                    var startInfo = new ProcessStartInfo
                    {
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = false,
                    };

                    var viewerOpts = optViewerOptions.GetValue();

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        startInfo.FileName = "/bin/bash";
                        startInfo.Arguments = $"-c \"{optRemoteViewer.GetValue()} {fileName} {viewerOpts}\"";
                    }
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        startInfo.FileName = $"\"{optRemoteViewer.GetValue()}\"";
                        startInfo.Arguments = $"\"{fileName}\" {viewerOpts}";
                    }

                    var process = new Process
                    {
                        StartInfo = startInfo
                    };

                    if (app.DebugIsActive())
                    {
                        await Console.Out.WriteLineAsync($"Run FileName: {process.StartInfo.FileName}");
                        await Console.Out.WriteLineAsync($"Run Arguments: {process.StartInfo.Arguments}");
                    }

                    if (!app.DryRunIsActive())
                    {
                        process.Start();
                        ctx.ExitCode = !process.HasExited || process.ExitCode == 0
                                            ? 0
                                            : 1;
                    }
                }
                else
                {
                    await Console.Out.WriteLineAsync($"Error: {reasonPhrase}");
                    ctx.ExitCode = 1;
                }
            });

            return await app.ExecuteApp(args);
        }
    }
}
