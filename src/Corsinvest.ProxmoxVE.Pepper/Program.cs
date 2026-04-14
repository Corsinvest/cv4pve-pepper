/*
 * SPDX-FileCopyrightText: Copyright Corsinvest Srl
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Runtime.InteropServices;
using Corsinvest.ProxmoxVE.Api.Console.Helpers;
using Corsinvest.ProxmoxVE.Api.Extension;
using Corsinvest.ProxmoxVE.Api.Extension.Utils;
using Corsinvest.ProxmoxVE.Api.Shared.Models.Vm;
using Microsoft.Extensions.Logging;

namespace Corsinvest.ProxmoxVE.Pepper;

internal partial class Program
{
    [DllImport("kernel32.dll", SetLastError = true)]
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    private static extern bool AttachConsole(int dwProcessId);

    private static async Task<int> Main(string[] args)
    {
        // On Windows (WinExe), re-attach to the parent console if launched from a terminal
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) { AttachConsole(-1); }

        var app = ConsoleHelper.CreateApp("Launching SPICE/VNC remote-viewer for Proxmox VE");
        var loggerFactory = ConsoleHelper.CreateLoggerFactory<Program>(app.GetLogLevelFromDebug());

        var optVmId = app.VmIdOrNameOption();

        var optProxy = app.AddOption<string>("--proxy",
                                             "SPICE proxy server. This can be used by the client to specify the proxy server." +
                                             " All nodes in a cluster runs 'spiceproxy', so it is up to the client to choose one." +
                                             " By default, we return the node to connect." +
                                             " If specify http(s)://[host]:[port] then replace proxy option in file .vv. E.g. for reverse proxy.");

        var optRemoteViewer = app.AddOption<string>("--viewer", "Executable SPICE client remote viewer (remote-viewer executable)")
                                 .AddValidatorExistFile();
        optRemoteViewer.Required = true;

        var optViewerOptions = app.AddOption<string>("--viewer-options", "Send options directly SPICE Viewer (quote value).");
        var optStartOrResume = app.AddOption<bool>("--start-or-resume", "Run stopped or paused VM");
        var optVnc = app.AddOption<bool>("--vnc", "Use VNC instead of SPICE (works on any running VM/CT without display configuration)");

        var optWaitForStartup = app.AddOption<int>("--wait-for-startup", "Wait sec. for startup VM");
        optWaitForStartup.DefaultValueFactory = (_) => 5;

        app.SetAction(async (action) =>
        {
            var client = await app.ClientTryLoginAsync(loggerFactory);
            var vmId = action.GetValue(optVmId);

            var output = app.DebugIsActive() ? Console.Out : null;

            var vm = await client.GetVmAsync(vmId);
            if (action.GetValue(optStartOrResume) && (vm.IsStopped || vm.IsPaused))
            {
                var status = vm.IsStopped ? VmStatus.Start : VmStatus.Resume;

                if (output != null) { await output.WriteLineAsync($"VM is {(vm.IsStopped ? "stopped" : "paused")}. {status} now!"); }

                var result = await VmHelper.ChangeStatusVmAsync(client, vm.Node, vm.VmType, vm.VmId, status);
                if (!result.IsSuccessStatusCode)
                {
                    await Console.Out.WriteLineAsync($"Error with code: {result.StatusCode}, phrase {result.ReasonPhrase}!");
                }
                await client.WaitForTaskToFinishAsync(result, timeout: action.GetValue(optWaitForStartup) * 1000);

                vm = await client.GetVmAsync(vmId);
                if (output != null) { await output.WriteLineAsync($"VM is {vm.Status}."); }
            }

            var remoteViewer = action.GetValue(optRemoteViewer)!;
            var viewerOptions = action.GetValue(optViewerOptions) ?? string.Empty;

            if (action.GetValue(optVnc))
            {
                var (Error, FileName, Bridge) = await RemoteViewerHelper.PrepareVncAsync(client,
                                                                                         vm.Node,
                                                                                         vm.VmType,
                                                                                         vm.VmId,
                                                                                         output);
                if (Error != null) { await Console.Out.WriteLineAsync($"Error: {Error}"); return 1; }
                if (!app.DryRunIsActive())
                {
                    await using (Bridge)
                    {
                        return RemoteViewerHelper.Launch(remoteViewer, FileName!, viewerOptions, true, output);
                    }
                }

                return 0;
            }
            else
            {
                var (Error, FileName) = await RemoteViewerHelper.PrepareSpiceAsync(client,
                                                                                   vm.Node,
                                                                                   vm.VmType,
                                                                                   vm.VmId,
                                                                                   action.GetValue(optProxy),
                                                                                   output);
                if (Error != null) { await Console.Out.WriteLineAsync($"Error: {Error}"); return 1; }
                if (!app.DryRunIsActive())
                {
                    return RemoteViewerHelper.Launch(remoteViewer, FileName!, viewerOptions, false, output);
                }

                return 0;
            }
        });

        return await app.ExecuteAppAsync(args, loggerFactory.CreateLogger<Program>());
    }
}
