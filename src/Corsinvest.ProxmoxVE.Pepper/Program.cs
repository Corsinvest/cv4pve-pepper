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

using System;
using System.IO;
using Corsinvest.ProxmoxVE.Api.Extension.Helpers;
using Corsinvest.ProxmoxVE.Api.Extension.Helpers.Shell;
using McMaster.Extensions.CommandLineUtils;

namespace Corsinvest.ProxmoxVE.Pepper
{
    class Program
    {
        public static readonly string APP_NAME = "cv4pve-pepper";
        static int Main(string[] args)
        {
            var app = ShellHelper.CreateConsoleApp(APP_NAME, "Launching SPICE on Proxmox VE", true);

            var optVmId = app.VmIdOrNameOption();
            var optRemoteViewer = app.Option("--viewer",
                                             "Executable SPICE client remote viewer",
                                             CommandOptionType.SingleValue);
            optRemoteViewer.Accepts().ExistingFile();

            app.OnExecute(() =>
            {
                var fileName = Path.GetTempFileName().Replace(".tmp", ".vv");
                var ret = SpiceHelper.CreateFileSpaceClient(app.ClientTryLogin(),
                                                            optVmId.Value(),
                                                            fileName);

                if (ret)
                {
                    var cmd = StringHelper.Quote(optRemoteViewer.Value()) +
                                                 " " +
                                                 StringHelper.Quote(fileName);

                    ret = ShellHelper.Execute(cmd,
                                              true,
                                              null,
                                              Console.Out,
                                              app.DryRunIsActive(),
                                              app.DebugIsActive()).ExitCode == 0;
                }

                return ret ? 0 : 1;
            });

            return app.ExecuteConsoleApp(Console.Out, args);
        }
    }
}