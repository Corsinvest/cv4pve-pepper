/*
 * SPDX-FileCopyrightText: Copyright Corsinvest Srl
 * SPDX-License-Identifier: GPL-3.0-only
 */

using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace Corsinvest.ProxmoxVE.Pepper;

/// <summary>
/// Sends the lines written by RemoteViewerHelper to the logger at Debug level,
/// masking the SPICE password and the VNC ticket they contain.
/// </summary>
internal sealed partial class LoggerTextWriter(ILogger logger) : TextWriter
{
    [GeneratedRegex(@"(vncticket=)[^&\s]+")]
    private static partial Regex VncTicketRegex();

    [GeneratedRegex(@"^(password=).*$", RegexOptions.Multiline)]
    private static partial Regex PasswordRegex();

    public override Encoding Encoding => Encoding.UTF8;

    public override void WriteLine(string? value)
    {
        if (string.IsNullOrEmpty(value)) { return; }

        value = VncTicketRegex().Replace(value, "$1****");
        value = PasswordRegex().Replace(value, "$1****");
        logger.LogDebug("{Message}", value);
    }
}
