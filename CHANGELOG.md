# Changelog

## [Unreleased]

### Documentation
- **Documentation site.** The documentation moved from the README to [corsinvest.github.io/cv4pve-pepper](https://corsinvest.github.io/cv4pve-pepper/): getting started, permissions, options, SPICE and VNC, desktop shortcut and troubleshooting, each checked against the code and on a test cluster. The README is now a short overview

### Fixed
- `--debug` and `--log-level` now control all diagnostic output the same way: pepper's own lines (viewer command, VNC bridge, start/resume) go through the logger instead of the console, so `--log-level` shows them too
- Secrets are masked in diagnostic output: the VNC ticket in the WebSocket URL and the SPICE password
- `--dry-run` no longer leaves a `.vv` file with a valid ticket in the temp folder
- Errors are reported as `ERROR: …` like the other cv4pve tools, with the stack trace when `--debug` is set
- `--start-or-resume` stops with the real reason when the start or resume fails, instead of trying to open the console of a stopped VM; the result of the start task is checked
- Windows release builds are WinExe again: no console window when launched from a shortcut (the release is built on Linux, so the output type now follows the target runtime)
- `--proxy` help and README: the SPICE proxy is an IP address or host name reached on port 3128, not a URL
- Packaging license was MIT; the project is GPL-3.0-only

### Changed
- Updated Corsinvest.ProxmoxVE.Api.Console to 9.2.3
- Product icon (Lucide `monitor-play`) and Windows executable icon
- Project metadata, symbols and Source Link aligned with the other cv4pve tools

## [2.0.0] - 2026-04-14

### Added
- VNC console support via `--vnc` flag — connect to any running VM or container without SPICE display configuration
- No extra console window on Windows when launched from a desktop shortcut or file manager

### Fixed
- Password file with plaintext content was rejected — now works correctly alongside encrypted files
- VNC connection was using the wrong API endpoint for QEMU VMs and LXC containers
- Viewer was launched even when the API returned an error — now shows the error and exits cleanly
