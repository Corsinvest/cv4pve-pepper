/*
 * SPDX-FileCopyrightText: Copyright Corsinvest Srl
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System;
using System.CommandLine;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Corsinvest.ProxmoxVE.Api.Extension;
using Corsinvest.ProxmoxVE.Api.Extension.Utils;
using Corsinvest.ProxmoxVE.Api.Shared.Models.Vm;
using Corsinvest.ProxmoxVE.Api.Shell.Helpers;
using Microsoft.Extensions.Logging;

var app = ConsoleHelper.CreateApp("cv4pve-pepper", "Launching SPICE remote-viewer for Proxmox VE");
var loggerFactory = ConsoleHelper.CreateLoggerFactory<Program>(app.GetLogLevelFromDebug());

var optVmId = app.VmIdOrNameOption();

var optProxy = app.AddOption<string>("--proxy",
                                     @"SPICE proxy server. This can be used by the client to specify the proxy server." +
                                      " All nodes in a cluster runs 'spiceproxy', so it is up to the client to choose one." +
                                      " By default, we return the node to connect." +
                                      " If specify http(s)://[host]:[port] then replace proxy option in file .vv. E.g. for reverse proxy.");

var optRemoteViewer = app.AddOption<string>("--viewer", "Executable SPICE client remote viewer.")
                         .AddValidatorExistFile();
optRemoteViewer.IsRequired = true;

var optViewerOptions = app.AddOption<string>("--viewer-options", "Send options directly SPICE Viewer (quote value).");
var optStartOrResume = app.AddOption<bool>("--start-or-resume", "Run stopped or paused VM");

var optWaitForStartup = app.AddOption<int>("--wait-for-startup", "Wait sec. for startup VM");
optWaitForStartup.SetDefaultValue(5);

app.SetHandler(async (ctx) =>
{
    var client = await app.ClientTryLoginAsync(loggerFactory);
    var proxy = ctx.ParseResult.GetValueForOption(optProxy);
    if (string.IsNullOrWhiteSpace(proxy)) { proxy = client.Host; }

    var vmId = ctx.ParseResult.GetValueForOption(optVmId);

    var vm = await client.GetVmAsync(vmId);
    if (ctx.ParseResult.GetValueForOption(optStartOrResume) && (vm.IsStopped || vm.IsPaused))
    {
        var status = vm.IsStopped
                        ? VmStatus.Start
                        : VmStatus.Resume;

        if (app.DebugIsActive())
        {
            await Console.Out.WriteLineAsync($"VM is {(vm.IsStopped ? "stopped" : "paused")}. {status} now!");
        }

        //start VM
        var result = await VmHelper.ChangeStatusVmAsync(client, vm.Node, vm.VmType, vm.VmId, status);
        await client.WaitForTaskToFinishAsync(result, timeout: ctx.ParseResult.GetValueForOption(optWaitForStartup) * 1000);

        //check VM is running
        vm = await client.GetVmAsync(vmId);
        if (app.DebugIsActive()) { await Console.Out.WriteLineAsync($"VM is {vm.Status}."); }
    }

    var (success, reasonPhrase, content) = await client.Nodes[vm.Node].Qemu[vm.Id].Spiceproxy.GetSpiceFileVVAsync(proxy);
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

        var viewerOptions = ctx.ParseResult.GetValueForOption(optViewerOptions);
        var remoteViewer = ctx.ParseResult.GetValueForOption(optRemoteViewer);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            startInfo.FileName = "/bin/bash";
            startInfo.Arguments = $"-c \"{remoteViewer} {fileName} {viewerOptions}\"";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            startInfo.FileName = $"\"{remoteViewer}\"";
            startInfo.Arguments = $"\"{fileName}\" {viewerOptions}";
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

return await app.ExecuteAppAsync(args, loggerFactory.CreateLogger(typeof(Program)));