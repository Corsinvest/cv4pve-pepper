/*
 * SPDX-FileCopyrightText: Copyright Corsinvest Srl
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Runtime.InteropServices;
using Corsinvest.ProxmoxVE.Api;
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
        var logger = loggerFactory.CreateLogger<Program>();

        var optVmId = app.VmIdOrNameOption();

        var optProxy = app.AddOption<string>("--proxy",
                                             "SPICE proxy: IP address or host name (no http:// and no port), reached on port 3128." +
                                             " Every node in the cluster runs 'spiceproxy', so any node can be used." +
                                             " Default: the host used to connect.");

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

            // Follows --debug and --log-level like the API calls; secrets are masked
            using var output = logger.IsEnabled(LogLevel.Debug) ? new LoggerTextWriter(logger) : null;

            var vm = await client.GetVmAsync(vmId);
            if (action.GetValue(optStartOrResume) && (vm.IsStopped || vm.IsPaused))
            {
                var status = vm.IsStopped ? VmStatus.Start : VmStatus.Resume;

                logger.LogDebug("VM is {State}. {Status} now!", vm.IsStopped ? "stopped" : "paused", status);

                var result = await VmHelper.ChangeStatusVmAsync(client, vm.Node, vm.VmType, vm.VmId, status);
                if (!result.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException($"{status} VM/CT {vm.VmId} failed: {result.ReasonPhrase}");
                }
                var waitForStartup = action.GetValue(optWaitForStartup);
                await client.WaitForTaskToFinishAsync(result, timeout: waitForStartup * 1000);

                // Read the task result: /cluster/resources reports the new status only after a few seconds
                var task = result.ToData<string>();
                if (await client.TaskIsRunningAsync(task))
                {
                    logger.LogDebug("{Status} still running after {Seconds}s, continuing.", status, waitForStartup);
                }
                else
                {
                    var exitStatus = await client.GetExitStatusTaskAsync(task);
                    if (exitStatus != "OK") { throw new InvalidOperationException($"{status} VM/CT {vm.VmId} failed: {exitStatus}"); }
                    logger.LogDebug("{Status} VM/CT {VmId}: {ExitStatus}.", status, vm.VmId, exitStatus);
                }
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
                if (Error != null) { throw new InvalidOperationException(Error); }

                await using (Bridge)
                {
                    if (!app.DryRunIsActive())
                    {
                        return RemoteViewerHelper.Launch(remoteViewer, FileName!, viewerOptions, true, output);
                    }
                }

                // The viewer deletes the .vv (delete-this-file=1); with --dry-run it is never launched
                File.Delete(FileName!);
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
                if (Error != null) { throw new InvalidOperationException(Error); }
                if (!app.DryRunIsActive())
                {
                    return RemoteViewerHelper.Launch(remoteViewer, FileName!, viewerOptions, false, output);
                }

                // The viewer deletes the .vv (delete-this-file=1); with --dry-run it is never launched
                File.Delete(FileName!);
                return 0;
            }
        });

        return await app.ExecuteAppAsync(args, logger);
    }
}
