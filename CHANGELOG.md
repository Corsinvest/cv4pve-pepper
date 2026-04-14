# Changelog

## [2.0.0] - 2026-04-14

### Added
- VNC console support via `--vnc` flag — connect to any running VM or container without SPICE display configuration
- No extra console window on Windows when launched from a desktop shortcut or file manager

### Fixed
- Password file with plaintext content was rejected — now works correctly alongside encrypted files
- VNC connection was using the wrong API endpoint for QEMU VMs and LXC containers
- Viewer was launched even when the API returned an error — now shows the error and exits cleanly
