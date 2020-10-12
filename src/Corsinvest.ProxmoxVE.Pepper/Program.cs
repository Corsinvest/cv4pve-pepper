/*
 * This file is part of the cv4pve-pepper https://github.com/Corsinvest/cv4pve-pepper,
 *
 * This source file is available under two different licenses:
 * - GNU General Public License version 3 (GPLv3)
 * - Corsinvest Enterprise License (CEL)
 * Full copyright and license information is available in
 * LICENSE.md which is distributed with this source code.
 *
 * Copyright (C) 2016 Corsinvest Srl	GPLv3 and CEL
 */

using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Corsinvest.ProxmoxVE.Api.Extension.Helpers;
using Corsinvest.ProxmoxVE.Api.Shell.Helpers;
using McMaster.Extensions.CommandLineUtils;

namespace Corsinvest.ProxmoxVE.Pepper
{
    class Program
    {
        static int Main(string[] args)
        {
            var app = ShellHelper.CreateConsoleApp("cv4pve-pepper", "Launching SPICE on Proxmox VE");

            var optVmId = app.VmIdOrNameOption().DependOn(app, CommandOptionExtension.HOST_OPTION_NAME);

            var optProxy = app.Option("--proxy",
                                      @"SPICE proxy server. This can be used by the client to specify the proxy server." +
                                      " All nodes in a cluster runs 'spiceproxy', so it is up to the client to choose one." +
                                      " By default, we return the node where the VM is currently running.",
                                      CommandOptionType.SingleValue);

            var optRemoteViewer = app.Option("--viewer",
                                             "Executable SPICE client remote viewer",
                                             CommandOptionType.SingleValue)
                                     .DependOn(app, CommandOptionExtension.HOST_OPTION_NAME);
            optRemoteViewer.Accepts().ExistingFile();



            app.OnExecute(() =>
            {
                var fileName = Path.GetTempFileName().Replace(".tmp", ".vv");
                var client = app.ClientTryLogin();

                var ret = SpiceHelper.CreateFileSpaceClient(client,
                                                            optVmId.Value(),
                                                            optProxy.HasValue() ? optProxy.Value() : null,
                                                            fileName);

                if (ret)
                {
                    var startInfo = new ProcessStartInfo
                    {
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = false,
                    };

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        startInfo.FileName = "/bin/bash";
                        startInfo.Arguments = $"-c \"{optRemoteViewer.Value()} {fileName}\"";
                    }
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        startInfo.FileName = StringHelper.Quote(optRemoteViewer.Value());
                        startInfo.Arguments = StringHelper.Quote(fileName);
                    }

                    var process = new Process
                    {
                        StartInfo = startInfo
                    };

                    if (app.DebugIsActive())
                    {
                        app.Out.WriteLine($"Run FileName: {process.StartInfo.FileName}");
                        app.Out.WriteLine($"Run Arguments: {process.StartInfo.Arguments}");
                    }

                    if (!app.DryRunIsActive())
                    {
                        process.Start();
                        ret = !process.HasExited || process.ExitCode == 0;
                    }
                }
                else
                {
                    if (!client.LastResult.IsSuccessStatusCode)
                    {
                        app.Out.WriteLine($"Error: {client.LastResult.ReasonPhrase}");
                    }
                }

                return ret ? 0 : 1;
            });

            return app.ExecuteConsoleApp(args);
        }
    }
}