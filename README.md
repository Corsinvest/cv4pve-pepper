# cv4pve-pepper

```
   ______                _                      __
  / ____/___  __________(_)___ _   _____  _____/ /_
 / /   / __ \/ ___/ ___/ / __ \ | / / _ \/ ___/ __/
/ /___/ /_/ / /  (__  ) / / / / |/ /  __(__  ) /_
\____/\____/_/  /____/_/_/ /_/|___/\___/____/\__/

Launching SPICE Remote Viewer for Proxmox VE (Made in Italy)
```

[![License](https://img.shields.io/github/license/Corsinvest/cv4pve-pepper.svg?style=flat-square)](LICENSE.md)
[![Release](https://img.shields.io/github/release/Corsinvest/cv4pve-pepper.svg?style=flat-square)](https://github.com/Corsinvest/cv4pve-pepper/releases/latest)
[![Downloads](https://img.shields.io/github/downloads/Corsinvest/cv4pve-pepper/total.svg?style=flat-square&logo=download)](https://github.com/Corsinvest/cv4pve-pepper/releases)

---

## Quick Start

```bash
# Download latest release from: https://github.com/Corsinvest/cv4pve-pepper/releases
wget https://github.com/Corsinvest/cv4pve-pepper/releases/download/VERSION/cv4pve-pepper-linux-x64.zip
unzip cv4pve-pepper-linux-x64.zip
chmod +x cv4pve-pepper

# Launch SPICE viewer for VM
./cv4pve-pepper --host=YOUR_HOST --username=root@pam --password=YOUR_PASSWORD --vmid=100 --viewer=/usr/bin/remote-viewer
```

---

## Features

### Core Capabilities

#### **Performance & Reliability**
- **Native C#** implementation
- **Cross-platform** (Windows, Linux, macOS)
- **API-based** operation (no root access required)
- **Cluster support** with automatic failover
- **Ticket management** (automatic handling of expiring tickets)

#### **Advanced Management**
- **API token** support (Proxmox VE 6.2+)
- **Auto start/resume** stopped or paused VMs
- **Proxy support** for reverse proxy configurations
- **SSL validation** options
- **Direct viewer options** pass-through

### Why cv4pve-pepper?

This tool simplifies SPICE client connections to Proxmox VE by:

- **No ticket expiration hassle** - Handles Proxmox VE tickets automatically
- **No GUI required** - Command-line execution
- **No manual .vv file downloads** - Generates and launches automatically
- **Simple workflow** - Single command to connect

---

## Installation

 
### Permission

| Permission | Purpose | Scope |
|------------|---------|-------|
| **VM.Console** | Access VM console | Virtual machines |
| **VM.Audit** | Read VM information | Virtual machines |

### Linux Installation

```bash
# Check available releases at: https://github.com/Corsinvest/cv4pve-pepper/releases
# Download specific version (replace VERSION with actual version like v1.9.0)
wget https://github.com/Corsinvest/cv4pve-pepper/releases/download/VERSION/cv4pve-pepper-linux-x64.zip

# Extract and make executable
unzip cv4pve-pepper-linux-x64.zip
chmod +x cv4pve-pepper

# Optional: Move to system path
sudo mv cv4pve-pepper /usr/local/bin/
```

### Windows Installation

```powershell
# Check available releases at: https://github.com/Corsinvest/cv4pve-pepper/releases
# Download specific version (replace VERSION with actual version)
Invoke-WebRequest -Uri "https://github.com/Corsinvest/cv4pve-pepper/releases/download/VERSION/cv4pve-pepper-win-x64.zip" -OutFile "cv4pve-pepper.zip"

# Extract
Expand-Archive cv4pve-pepper.zip -DestinationPath "C:\Tools\cv4pve-pepper"

# Add to PATH (optional)
$env:PATH += ";C:\Tools\cv4pve-pepper"
```

### macOS Installation

```bash
# Check available releases at: https://github.com/Corsinvest/cv4pve-pepper/releases
# Download specific version (replace VERSION with actual version)
wget https://github.com/Corsinvest/cv4pve-pepper/releases/download/VERSION/cv4pve-pepper-osx-x64.zip
unzip cv4pve-pepper-osx-x64.zip
chmod +x cv4pve-pepper

# Move to applications
sudo mv cv4pve-pepper /usr/local/bin/
```

---

## Usage

### Basic Connection

```bash
# Connect to VM using username/password
cv4pve-pepper --host=192.168.1.100 --username=root@pam --password=secret --vmid=100 --viewer=/usr/bin/remote-viewer

# Connect using API token (recommended)
cv4pve-pepper --host=192.168.1.100 --api-token=user@pve!token1=uuid-here --vmid=100 --viewer=/usr/bin/remote-viewer
```

### Advanced Options

```bash
# Auto-start stopped VM and connect
cv4pve-pepper --host=pve.local --username=root@pam --password=secret --vmid=100 --viewer=/usr/bin/remote-viewer --start-or-resume

# Connect with fullscreen viewer
cv4pve-pepper --host=pve.local --api-token=user@pve!token=uuid --vmid=100 --viewer=/usr/bin/remote-viewer --viewer-options="-f"

# Connect using VM name instead of ID
cv4pve-pepper --host=pve.local --username=root@pam --password=secret --vmid=webserver --viewer=/usr/bin/remote-viewer

# Use with reverse proxy
cv4pve-pepper --host=pve.local --username=root@pam --password=secret --vmid=100 --viewer=/usr/bin/remote-viewer --proxy=https://spice.company.com:3128
```

### High Availability Cluster

```bash
# Multiple hosts for HA failover
cv4pve-pepper --host=pve1.local:8006,pve2.local:8006,pve3.local:8006 --api-token=user@pve!token=uuid --vmid=100 --viewer=/usr/bin/remote-viewer
```

### Using Parameter Files

```bash
# Create parameter file
cat > vm-connection.conf <<EOF
--host=192.168.1.100
--api-token=user@pve!token=uuid-here
--vmid=100
--viewer=/usr/bin/remote-viewer
--viewer-options=-f
--start-or-resume
EOF

# Execute with parameter file
cv4pve-pepper @vm-connection.conf
```

---

## SPICE Client Setup

### Installation

<details>
<summary><strong>Linux (Debian/Ubuntu)</strong></summary>

```bash
sudo apt-get update
sudo apt-get install virt-viewer
```

**Path**: `/usr/bin/remote-viewer`

</details>

<details>
<summary><strong>Linux (RHEL/CentOS/Fedora)</strong></summary>

```bash
sudo dnf install virt-viewer
# or
sudo yum install virt-viewer
```

**Path**: `/usr/bin/remote-viewer`

</details>

<details>
<summary><strong>Windows</strong></summary>

Download from [SPICE Space](http://www.spice-space.org/download.html)

**Typical Path**: `C:\Program Files\VirtViewer v?.?-???\bin\remote-viewer.exe`

**Minimum Version**: virt-viewer 0.5.6 or higher

</details>

<details>
<summary><strong>macOS</strong></summary>

Download from [SPICE Space macOS Client](https://www.spice-space.org/osx-client.html)

**Minimum Version**: virt-viewer 0.5.7 or higher

**Note**: macOS support may have limitations

</details>

### Viewer Options

Pass options directly to the SPICE viewer using `--viewer-options`:

| Option | Description | Example |
|--------|-------------|---------|
| `-f` | Fullscreen mode | `--viewer-options="-f"` |
| `-t` | Set window title | `--viewer-options="-t 'My VM'"` |
| `--debug` | Enable viewer debug | `--viewer-options="--debug"` |

---

## Command Reference

### Authentication Options

| Parameter | Description | Example |
|-----------|-------------|---------|
| `--host` | Proxmox host(s) (required) | `--host=pve.local:8006` |
| `--username` | Username@realm | `--username=root@pam` |
| `--password` | Password or file | `--password=secret` or `--password=file:/etc/cv4pve/password` |
| `--api-token` | API token | `--api-token=user@realm!token=uuid` |

### VM Selection

| Parameter | Description | Example |
|-----------|-------------|---------|
| `--vmid` | VM/CT ID or name | `--vmid=100` or `--vmid=webserver` |

### SPICE Options

| Parameter | Description | Required |
|-----------|-------------|----------|
| `--viewer` | Path to remote-viewer executable | Yes |
| `--viewer-options` | Options to pass to viewer | No |
| `--proxy` | SPICE proxy server URL | No |

### VM Control

| Parameter | Description | Default |
|-----------|-------------|---------|
| `--start-or-resume` | Start stopped or resume paused VM | `false` |
| `--wait-for-startup` | Seconds to wait for VM startup | `5` |

### Security Options

| Parameter | Description | Default |
|-----------|-------------|---------|
| `--validate-certificate` | Validate SSL certificate | `false` |

### Output Options

| Parameter | Description |
|-----------|-------------|
| `--debug` | Enable debug output |
| `--version` | Show version information |
| `-h, --help` | Show help and usage |

---

## Troubleshooting

### Common Issues

<details>
<summary><strong>Error: "no spice port"</strong></summary>

**Cause**: Display hardware not configured for SPICE

**Solution**:
1. Open VM hardware settings in Proxmox VE
2. Change Display to "SPICE (qxl)"
3. Restart the VM
4. Try connecting again

</details>

<details>
<summary><strong>Error: "Authentication failed"</strong></summary>

**Solution**:
```bash
# Verify credentials
cv4pve-pepper --host=pve.local --username=root@pam --password=test --vmid=100 --viewer=/usr/bin/remote-viewer --debug

# Check API token format
cv4pve-pepper --host=pve.local --api-token=user@realm!tokenid=uuid --vmid=100 --viewer=/usr/bin/remote-viewer --debug
```

</details>

<details>
<summary><strong>Error: "Connection timeout"</strong></summary>

**Check connectivity**:
```bash
# Test network connectivity
ping pve.local
telnet pve.local 8006

# Try with IP address instead
cv4pve-pepper --host=192.168.1.100 --username=root@pam --password=secret --vmid=100 --viewer=/usr/bin/remote-viewer

# Enable SSL validation if needed
cv4pve-pepper --host=pve.local --username=root@pam --password=secret --validate-certificate --vmid=100 --viewer=/usr/bin/remote-viewer
```

</details>

<details>
<summary><strong>Viewer not found</strong></summary>

**Find viewer path**:
```bash
# Linux
which remote-viewer
# Common path: /usr/bin/remote-viewer

# Windows (PowerShell)
Get-Command remote-viewer
# Common path: C:\Program Files\VirtViewer v*\bin\remote-viewer.exe
```

</details>

### Debug Mode

Enable detailed logging:

```bash
cv4pve-pepper --host=pve.local --username=root@pam --password=secret --vmid=100 --viewer=/usr/bin/remote-viewer --debug
```

---

## Support

Professional support and consulting available through [Corsinvest](https://www.corsinvest.it/cv4pve).

---

Part of [cv4pve](https://www.corsinvest.it/cv4pve) suite | Made with ❤️ in Italy by [Corsinvest](https://www.corsinvest.it)

Copyright © Corsinvest Srl