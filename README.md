# cv4pve-pepper

```
   ______                _                      __
  / ____/___  __________(_)___ _   _____  _____/ /_
 / /   / __ \/ ___/ ___/ / __ \ | / / _ \/ ___/ __/
/ /___/ /_/ / /  (__  ) / / / / |/ /  __(__  ) /_
\____/\____/_/  /____/_/_/ /_/|___/\___/____/\__/

Launching SPICE/VNC Remote Viewer for Proxmox VE (Made in Italy)
```

[![License](https://img.shields.io/github/license/Corsinvest/cv4pve-pepper.svg?style=flat-square)](LICENSE.md)
[![Release](https://img.shields.io/github/release/Corsinvest/cv4pve-pepper.svg?style=flat-square)](https://github.com/Corsinvest/cv4pve-pepper/releases/latest)
[![Downloads](https://img.shields.io/github/downloads/Corsinvest/cv4pve-pepper/total.svg?style=flat-square&logo=download)](https://github.com/Corsinvest/cv4pve-pepper/releases)
[![WinGet](https://img.shields.io/winget/v/Corsinvest.cv4pve.pepper?style=flat-square&logo=windows)](https://winstall.app/apps/Corsinvest.cv4pve.pepper)
[![AUR](https://img.shields.io/aur/version/cv4pve-pepper?style=flat-square&logo=archlinux)](https://aur.archlinux.org/packages/cv4pve-pepper)

> **SPICE and VNC console launcher for Proxmox VE** — connect to any VM or container with a single command.

---

## Desktop Experience

If you prefer a graphical interface to browse and connect to your VMs, check out **[cv4pve-vdi](https://github.com/Corsinvest/cv4pve-vdi)** — the desktop VDI client for Proxmox VE with card/list view, filters, power control, SPICE, VNC and custom service launchers (RDP, SSH, and more).

---

## Quick Start

```bash
wget https://github.com/Corsinvest/cv4pve-pepper/releases/download/VERSION/cv4pve-pepper-linux-x64.zip
unzip cv4pve-pepper-linux-x64.zip

# SPICE
./cv4pve-pepper --host=YOUR_HOST --api-token=user@realm!token=uuid --vmid=100 --viewer=/usr/bin/remote-viewer

# VNC
./cv4pve-pepper --host=YOUR_HOST --api-token=user@realm!token=uuid --vmid=100 --viewer=/usr/bin/remote-viewer --vnc
```

---

## Installation

| Platform           | Command                                                                                                         |
| ------------------ | --------------------------------------------------------------------------------------------------------------- |
| **Linux**          | `wget .../cv4pve-pepper-linux-x64.zip && unzip cv4pve-pepper-linux-x64.zip && chmod +x cv4pve-pepper`          |
| **Windows WinGet** | `winget install Corsinvest.cv4pve.pepper`                                                                       |
| **Windows manual** | Download `cv4pve-pepper-win-x64.zip` from [Releases](https://github.com/Corsinvest/cv4pve-pepper/releases)     |
| **Arch Linux**     | `yay -S cv4pve-pepper`                                                                                          |
| **macOS**          | `wget .../cv4pve-pepper-osx-x64.zip && unzip cv4pve-pepper-osx-x64.zip && chmod +x cv4pve-pepper`              |

All binaries on the [Releases page](https://github.com/Corsinvest/cv4pve-pepper/releases).

---

## Features

- **SPICE** console launch via `remote-viewer`
- **VNC** console via WebSocket bridge — no firewall rules or node-side configuration required (see [VNC Console](#vnc-console))
- **Cross-platform** — Windows, Linux, macOS
- **Self-contained binary** — no runtime to install, copy and run
- **API token** support (Proxmox VE 6.2+)
- **Auto start/resume** stopped or paused VMs
- **Cluster support** with automatic failover
- **Proxy support** for reverse proxy configurations
- **No ticket expiration hassle** — handles Proxmox VE tickets automatically
- **No manual `.vv` file downloads** — generates and launches automatically

---

<details>
<summary><strong>Security &amp; Permissions</strong></summary>

### Required Permissions

| Permission | Purpose |
|------------|---------|
| `VM.Console` | Access VM SPICE/VNC console |
| `VM.Audit` | Read VM information |

</details>

---

## Usage

```bash
# Connect using username/password (SPICE)
cv4pve-pepper --host=192.168.1.100 --username=root@pam --password=secret --vmid=100 --viewer=/usr/bin/remote-viewer

# Connect using API token (recommended)
cv4pve-pepper --host=192.168.1.100 --api-token=user@pve!token1=uuid-here --vmid=100 --viewer=/usr/bin/remote-viewer

# Connect via VNC (works on any running VM/CT without SPICE display configuration)
cv4pve-pepper --host=pve.local --api-token=user@pve!token=uuid --vmid=100 --viewer=/usr/bin/remote-viewer --vnc

# Auto-start stopped VM and connect
cv4pve-pepper --host=pve.local --api-token=user@pve!token=uuid --vmid=100 --viewer=/usr/bin/remote-viewer --start-or-resume

# Connect with fullscreen viewer
cv4pve-pepper --host=pve.local --api-token=user@pve!token=uuid --vmid=100 --viewer=/usr/bin/remote-viewer --viewer-options="-f"

# Connect using VM name instead of ID
cv4pve-pepper --host=pve.local --api-token=user@pve!token=uuid --vmid=webserver --viewer=/usr/bin/remote-viewer

# Use with reverse proxy (SPICE only)
cv4pve-pepper --host=pve.local --api-token=user@pve!token=uuid --vmid=100 --viewer=/usr/bin/remote-viewer --proxy=https://spice.company.com:3128

# Multiple hosts for HA failover
cv4pve-pepper --host=pve1.local:8006,pve2.local:8006,pve3.local:8006 --api-token=user@pve!token=uuid --vmid=100 --viewer=/usr/bin/remote-viewer

# Parameter file (recommended for complex setups)
cv4pve-pepper @/etc/cv4pve/pepper.conf
```

---

## VNC Console

VNC is available on all running QEMU VMs and LXC containers — **no additional configuration required** on the Proxmox VE side.

cv4pve-pepper uses the Proxmox VE API to open a **WebSocket VNC tunnel**, bridges it to a local port, and launches `remote-viewer` to display the session. This means:

- No direct network access to the VNC port on the node is needed
- No firewall rules to open on the Proxmox VE host
- The same `remote-viewer` used for SPICE is reused — no extra software required
- Works transparently through the existing Proxmox VE API connection
- Available on every running VM and CT regardless of display hardware configuration

---

## SPICE/VNC Client Setup

A SPICE/VNC viewer (`remote-viewer`) must be installed.

<details>
<summary><strong>Linux (Debian/Ubuntu)</strong></summary>

```bash
sudo apt-get install virt-viewer
```

**Path**: `/usr/bin/remote-viewer`

</details>

<details>
<summary><strong>Linux (RHEL/CentOS/Fedora)</strong></summary>

```bash
sudo dnf install virt-viewer
```

**Path**: `/usr/bin/remote-viewer`

</details>

<details>
<summary><strong>Windows</strong></summary>

Download from [SPICE Space](https://www.spice-space.org/download.html)

**Typical path**: `C:\Program Files\VirtViewer v?-???\bin\remote-viewer.exe`

</details>

<details>
<summary><strong>macOS</strong></summary>

Download from [SPICE Space macOS Client](https://www.spice-space.org/osx-client.html)

</details>

---

## Support

Professional support and consulting available through [Corsinvest](https://www.corsinvest.it/cv4pve).

---

Part of [cv4pve](https://www.corsinvest.it/cv4pve) suite | Made with ❤️ in Italy by [Corsinvest](https://www.corsinvest.it)

Copyright © Corsinvest Srl
