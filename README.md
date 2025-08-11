# cv4pve-pepper 🌶️

<div align="center">

![cv4pve-pepper Banner](https://img.shields.io/badge/cv4pve--pepper-SPICE%20Remote%20Viewer-red?style=for-the-badge&logo=proxmox)

**🖥️ Simplified SPICE Remote Viewer Launcher for Proxmox VE**

[![License](https://img.shields.io/github/license/Corsinvest/cv4pve-pepper.svg?style=flat-square)](LICENSE.md)
[![Release](https://img.shields.io/github/release/Corsinvest/cv4pve-pepper.svg?style=flat-square)](https://github.com/Corsinvest/cv4pve-pepper/releases/latest)
[![Downloads](https://img.shields.io/github/downloads/Corsinvest/cv4pve-pepper/total.svg?style=flat-square&logo=download)](https://github.com/Corsinvest/cv4pve-pepper/releases)
[![.NET](https://img.shields.io/badge/.NET-C%23-blue?style=flat-square&logo=dotnet)](https://docs.microsoft.com/en-us/dotnet/csharp/)

⭐ **We appreciate your star, it helps!** ⭐

</div>

---

## 🚀 Quick Start

```bash
# Download and extract (replace VERSION with actual version)
wget https://github.com/Corsinvest/cv4pve-pepper/releases/download/VERSION/cv4pve-pepper-linux-x64.zip
unzip cv4pve-pepper-linux-x64.zip

# Launch SPICE viewer for VM 100
./cv4pve-pepper --host=YOUR_HOST --username=root@pam --password=YOUR_PASSWORD \
                --vmid=100 --viewer=/usr/bin/remote-viewer

# With API token (recommended)
./cv4pve-pepper --host=YOUR_HOST --api-token=user@realm!token=uuid \
                --vmid=100 --viewer=/usr/bin/remote-viewer
```

---

## 📋 Table of Contents

<details>
<summary>🗂️ <strong>Click to expand navigation</strong></summary>

- [🌟 Features](#-features)
- [📦 Installation](#-installation)
- [⚙️ Configuration](#-configuration)
- [🔧 Usage Examples](#-usage-examples)
- [🖥️ SPICE Client Setup](#-spice-client-setup)
- [🔐 Security & Authentication](#-security--authentication)
- [🛠️ Troubleshooting](#-troubleshooting)
- [📊 Command Reference](#-command-reference)

</details>

---

## 🌟 Features

<div align="center">

```
     ______                _                      __
    / ____/___  __________(_)___ _   _____  _____/ /_
   / /   / __ \/ ___/ ___/ / __ \ | / / _ \/ ___/ __/
  / /___/ /_/ / /  (__  ) / / / / |/ /  __(__  ) /_
  \____/\____/_/  /____/_/_/ /_/|___/\___/____/\__/

  🌶️ Launching SPICE remote-viewer for Proxmox VE (Made in Italy)
```

</div>

### 🏆 Core Capabilities

<table>
<tr>
<td width="50%">

#### ⚡ **Simplified SPICE Access**
- ✅ **No GUI required** - Pure command-line operation
- ✅ **No .vv file downloads** - Direct SPICE connection
- ✅ **Automatic ticket management** - Handles Proxmox VE tickets
- ✅ **Cross-platform** - Windows, Linux, macOS support
- ✅ **Native C#** implementation for reliability

#### 🔧 **Advanced Features**
- ✅ **Auto start/resume** VMs on connection
- ✅ **High availability** support with multiple hosts
- ✅ **Custom viewer options** pass-through
- ✅ **File-based configuration** for automation
- ✅ **SSL validation** options

</td>
<td width="50%">

#### 🛡️ **Enterprise Ready**
- ✅ **API token** authentication (Proxmox VE 6.2+)
- ✅ **No root access** required on Proxmox hosts
- ✅ **External execution** - runs outside Proxmox VE
- ✅ **Multiple host** support for cluster setups
- ✅ **Secure authentication** methods

#### 🚀 **User Experience**
- ✅ **Simple installation** - single binary extraction
- ✅ **Minimal configuration** required
- ✅ **Debug mode** for troubleshooting
- ✅ **Flexible proxy** support for reverse proxies
- ✅ **Wait for startup** - configurable VM boot time

</td>
</tr>
</table>

---

## 📦 Installation

<div align="center">
  <img src="https://img.shields.io/badge/INSTALLATION-GUIDE-green?style=for-the-badge&logo=download" alt="Installation Guide">
</div>

### 🐧 Linux Installation

```bash
# Check available releases at: https://github.com/Corsinvest/cv4pve-pepper/releases
# Download specific version (replace VERSION with actual version)
wget https://github.com/Corsinvest/cv4pve-pepper/releases/download/VERSION/cv4pve-pepper-linux-x64.zip

# Extract
unzip cv4pve-pepper-linux-x64.zip

# Make executable
chmod +x cv4pve-pepper

# Optional: Move to system path
sudo mv cv4pve-pepper /usr/local/bin/

# Verify installation
cv4pve-pepper --version
```

### 🪟 Windows Installation

```powershell
# Download from GitHub releases (replace VERSION with actual version)
Invoke-WebRequest -Uri "https://github.com/Corsinvest/cv4pve-pepper/releases/download/VERSION/cv4pve-pepper-win-x64.zip" -OutFile "cv4pve-pepper.zip"

# Extract
Expand-Archive cv4pve-pepper.zip -DestinationPath "C:\Tools\cv4pve-pepper"

# Add to PATH (optional)
$env:PATH += ";C:\Tools\cv4pve-pepper"

# Verify installation
cv4pve-pepper.exe --version
```

### 🍎 macOS Installation

```bash
# Download and extract (replace VERSION with actual version)
wget https://github.com/Corsinvest/cv4pve-pepper/releases/download/VERSION/cv4pve-pepper-osx-x64.zip
unzip cv4pve-pepper-osx-x64.zip
chmod +x cv4pve-pepper

# Move to applications
sudo mv cv4pve-pepper /usr/local/bin/

# Verify installation
cv4pve-pepper --version
```

---

## 🖥️ SPICE Client Setup

<div align="center">
  <img src="https://img.shields.io/badge/SPICE-CLIENT-blue?style=for-the-badge&logo=desktop" alt="SPICE Client">
</div>

### 📋 Requirements

cv4pve-pepper requires a SPICE client (virt-viewer) to be installed on your system.

### 🐧 Linux SPICE Client

```bash
# Ubuntu/Debian
sudo apt update && sudo apt install virt-viewer

# CentOS/RHEL/Fedora
sudo dnf install virt-viewer

# Verify installation
which remote-viewer
# Should output: /usr/bin/remote-viewer
```

### 🪟 Windows SPICE Client

1. **Download** virt-viewer 0.5.6 or higher from [SPICE official site](http://www.spice-space.org/download.html)
2. **Install** the package as Administrator
3. **Note the path** - typically: `C:\Program Files\VirtViewer v?.?-???\bin\remote-viewer.exe`

### 🍎 macOS SPICE Client

```bash
# Using Homebrew
brew install virt-viewer

# Or download from official site
# Note: macOS client may have limitations
```

### 📍 Common SPICE Client Paths

| OS | Typical Path |
|---|---|
| **Linux** | `/usr/bin/remote-viewer` |
| **Windows** | `C:\Program Files\VirtViewer v?.?-???\bin\remote-viewer.exe` |
| **macOS** | `/usr/local/bin/remote-viewer` |

---

## ⚙️ Configuration

<div align="center">
  <img src="https://img.shields.io/badge/CONFIGURATION-SETUP-blue?style=for-the-badge&logo=settings" alt="Configuration Setup">
</div>

### 🔐 Authentication Methods

<table>
<tr>
<td width="50%">

#### 🔑 **Username/Password**
```bash
cv4pve-pepper \
  --host=pve.local \
  --username=user@pam \
  --password=your_password \
  --vmid=100 \
  --viewer=/usr/bin/remote-viewer
```

#### 🎫 **API Token (Recommended)**
```bash
cv4pve-pepper \
  --host=pve.local \
  --api-token=user@realm!token=uuid \
  --vmid=100 \
  --viewer=/usr/bin/remote-viewer
```

</td>
<td width="50%">

#### 📁 **Password from File**
```bash
# Use interactive password prompt
cv4pve-pepper \
  --host=pve.local \
  --username=user@pam \
  --password=file:/etc/cv4pve/password \
  --vmid=100 \
  --viewer=/usr/bin/remote-viewer

# First run: prompts and saves password
# Later runs: reads from file
```

</td>
</tr>
</table>

### 📄 Configuration Files

<details>
<summary><strong>📋 Using Parameter Files</strong></summary>

Create a configuration file for repeated use:

#### Create Configuration File
```bash
# /etc/cv4pve/pepper.conf
--host=pve.cluster.local
--api-token=spice@pve!viewer=uuid-here
--viewer=/usr/bin/remote-viewer
--viewer-options=-f --full-screen
--start-or-resume
--wait-for-startup=10
```

#### Use Configuration File
```bash
cv4pve-pepper @/etc/cv4pve/pepper.conf --vmid=100
```

</details>

---

## 🔧 Usage Examples

<div align="center">
  <img src="https://img.shields.io/badge/USAGE-EXAMPLES-orange?style=for-the-badge&logo=terminal" alt="Usage Examples">
</div>

### 🖥️ Basic SPICE Connection

<details>
<summary><strong>🔄 Simple VM Connection</strong></summary>

#### Connect to VM by ID
```bash
cv4pve-pepper --host=pve.local --username=root@pam --password=secret \
              --vmid=100 --viewer=/usr/bin/remote-viewer
```

#### Connect to VM by Name
```bash
cv4pve-pepper --host=pve.local --api-token=user@pam!token=uuid \
              --vmid=web-server --viewer=/usr/bin/remote-viewer
```

#### Auto-start Stopped VM
```bash
cv4pve-pepper --host=pve.local --api-token=user@pam!token=uuid \
              --vmid=100 --viewer=/usr/bin/remote-viewer \
              --start-or-resume --wait-for-startup=15
```

</details>

### 🎛️ Advanced Options

<details>
<summary><strong>⚙️ Viewer Customization</strong></summary>

#### Full Screen Mode
```bash
cv4pve-pepper --host=pve.local --api-token=token \
              --vmid=100 --viewer=/usr/bin/remote-viewer \
              --viewer-options="-f"
```

#### Multiple Viewer Options
```bash
cv4pve-pepper --host=pve.local --api-token=token \
              --vmid=100 --viewer=/usr/bin/remote-viewer \
              --viewer-options="--full-screen --zoom=200"
```

#### Custom Proxy Configuration
```bash
cv4pve-pepper --host=pve.local --api-token=token \
              --vmid=100 --viewer=/usr/bin/remote-viewer \
              --proxy=https://spice-proxy.company.com:3128
```

</details>

### 🏢 High Availability Setup

<details>
<summary><strong>🔄 Cluster Configuration</strong></summary>

#### Multiple Proxmox Hosts
```bash
cv4pve-pepper --host=pve1.local:8006,pve2.local:8006,pve3.local:8006 \
              --api-token=cluster@pve!spice=uuid \
              --vmid=100 --viewer=/usr/bin/remote-viewer
```

#### SSL Certificate Validation
```bash
cv4pve-pepper --host=pve.company.com --validate-certificate \
              --api-token=secure@pve!token=uuid \
              --vmid=100 --viewer=/usr/bin/remote-viewer
```

</details>

### 🔧 Debug & Troubleshooting

<details>
<summary><strong>🐛 Debug Mode</strong></summary>

#### Enable Debug Output
```bash
cv4pve-pepper --host=pve.local --api-token=token \
              --vmid=100 --viewer=/usr/bin/remote-viewer \
              --debug
```

#### Test Connection Only
```bash
# Use --help to verify all parameters
cv4pve-pepper --host=pve.local --api-token=token --help
```

</details>

---

## 🔐 Security & Authentication

<div align="center">
  <img src="https://img.shields.io/badge/SECURITY-AUTHENTICATION-red?style=for-the-badge&logo=shield" alt="Security & Authentication">
</div>

### 🛡️ Required Permissions

For SPICE access, the user/token needs minimal permissions:

| Permission | Purpose | Scope |
|------------|---------|-------|
| **VM.Console** | Access VM console | Virtual machines |
| **VM.Audit** | Read VM information | Virtual machines |

### 🎫 API Token Setup

<details>
<summary><strong>🔧 Creating SPICE Access Token</strong></summary>

#### 1. Create API Token
Follow Proxmox VE documentation for creating API tokens with the required permissions listed above.

#### 2. Use Token in Commands
```bash
cv4pve-pepper --host=pve.local --api-token=spice@pve!viewer=uuid-from-creation \
              --vmid=100 --viewer=/usr/bin/remote-viewer
```

</details>

### 🔒 Security Best Practices

<table>
<tr>
<td width="50%">

#### ✅ **Recommended**
- ✅ Use API tokens instead of passwords
- ✅ Enable SSL certificate validation in production
- ✅ Use dedicated accounts for SPICE access
- ✅ Store credentials in secure configuration files
- ✅ Limit token permissions to minimum required

</td>
<td width="50%">

#### ❌ **Avoid**
- ❌ Using root credentials for SPICE access
- ❌ Storing passwords in plain text scripts
- ❌ Disabling SSL validation without good reason
- ❌ Sharing tokens between different applications
- ❌ Using overly broad permissions

</td>
</tr>
</table>

---

## 🛠️ Troubleshooting

<div align="center">
  <img src="https://img.shields.io/badge/TROUBLESHOOTING-HELP-red?style=for-the-badge&logo=tools" alt="Troubleshooting">
</div>

### ⚠️ Common Issues & Solutions

<details>
<summary><strong>🔌 "No SPICE Port" Error</strong></summary>

#### Problem
```
Error: no spice port
```

#### Solution
This error appears when the VM's display hardware is not configured for SPICE:

1. **Go to Proxmox web interface**
2. **Select your VM** → Hardware
3. **Edit Display** device
4. **Set Type** to "SPICE (qxl)"
5. **Start/restart** the VM

</details>

<details>
<summary><strong>🔐 Authentication Issues</strong></summary>

#### Problem: "Authentication failed"
```bash
# Check credentials and permissions
cv4pve-pepper --host=pve.local --username=test@pam --password=wrong \
              --vmid=100 --viewer=/usr/bin/remote-viewer --debug
```

#### Solution
1. **Verify credentials** in Proxmox web interface
2. **Check user permissions** (VM.Console, VM.Audit)
3. **Test API token** format: `user@realm!tokenid=uuid`

</details>

<details>
<summary><strong>🖥️ SPICE Client Issues</strong></summary>

#### Problem: "Viewer not found"
```bash
# Test if remote-viewer is available
which remote-viewer
# or
/usr/bin/remote-viewer --version
```

#### Solution
1. **Install virt-viewer** package
2. **Verify correct path** to remote-viewer
3. **Use full path** in --viewer parameter

</details>

<details>
<summary><strong>🌐 Network Issues</strong></summary>

#### Problem: Connection timeouts
```bash
# Test with debug mode
cv4pve-pepper --host=pve.local --api-token=token \
              --vmid=100 --viewer=/usr/bin/remote-viewer --debug
```

#### Solution
1. **Check network connectivity** to Proxmox host
2. **Verify firewall rules** (port 8006 for API, SPICE ports)
3. **Test with IP address** instead of hostname

</details>

### 🔍 Debug Mode

<details>
<summary><strong>🐛 Enabling Debug Output</strong></summary>

```bash
# Enable detailed logging
cv4pve-pepper --host=pve.local --api-token=token \
              --vmid=100 --viewer=/usr/bin/remote-viewer \
              --debug

# Debug output will show:
# - API connection details
# - Authentication process
# - SPICE ticket generation
# - Viewer launch commands
```

</details>

---

## 📊 Command Reference

<div align="center">
  <img src="https://img.shields.io/badge/COMMAND-REFERENCE-navy?style=for-the-badge&logo=terminal" alt="Command Reference">
</div>

### 🔧 Global Options

<details>
<summary><strong>⚙️ Complete Parameter List</strong></summary>

```bash
cv4pve-pepper [options]
```

#### Connection Options
| Parameter | Description | Example |
|-----------|-------------|---------|
| `--host` | Proxmox host(s) | `--host=pve.local:8006` |
| `--username` | Username@realm | `--username=spice@pve` |
| `--password` | Password or file | `--password=secret` |
| `--api-token` | API token | `--api-token=user@realm!token=uuid` |
| `--validate-certificate` | Validate SSL certificate | `--validate-certificate` |

#### VM & Viewer Options
| Parameter | Description | Required |
|-----------|-------------|----------|
| `--vmid` | VM ID or name | ❌ Optional |
| `--viewer` | Path to remote-viewer | ✅ Required |
| `--viewer-options` | Options for SPICE viewer | ❌ Optional |

#### Advanced Options
| Parameter | Description | Default |
|-----------|-------------|---------|
| `--proxy` | SPICE proxy server | None |
| `--start-or-resume` | Auto-start stopped/paused VM | `false` |
| `--wait-for-startup` | Wait seconds for VM startup | `5` |
| `--debug` | Enable debug output | `false` |

</details>

### 📖 Usage Examples by Category

<details>
<summary><strong>🎯 Common Use Cases</strong></summary>

#### Basic Connection
```bash
# Simple connection
cv4pve-pepper --host=pve.local --username=root@pam --password=secret \
              --vmid=100 --viewer=/usr/bin/remote-viewer

# With API token
cv4pve-pepper --host=pve.local --api-token=spice@pve!token=uuid \
              --vmid=web-server --viewer=/usr/bin/remote-viewer
```

#### Production Setup
```bash
# HA cluster with SSL validation
cv4pve-pepper --host=pve1.local,pve2.local,pve3.local --validate-certificate \
              --api-token=production@pve!spice=uuid \
              --vmid=100 --viewer=/usr/bin/remote-viewer

# Auto-start with custom timeout
cv4pve-pepper --host=pve.local --api-token=token \
              --vmid=100 --viewer=/usr/bin/remote-viewer \
              --start-or-resume --wait-for-startup=30
```

#### Customized Viewer
```bash
# Full screen with high quality
cv4pve-pepper --host=pve.local --api-token=token \
              --vmid=100 --viewer=/usr/bin/remote-viewer \
              --viewer-options="--full-screen --zoom=150"

# Through reverse proxy
cv4pve-pepper --host=internal-pve.local --api-token=token \
              --vmid=100 --viewer=/usr/bin/remote-viewer \
              --proxy=https://proxy.company.com:3128
```

</details>

---

## 📞 Support & Community

<div align="center">
  <img src="https://img.shields.io/badge/SUPPORT-COMMUNITY-blue?style=for-the-badge&logo=community" alt="Support & Community">
</div>

### 🏢 Commercial Support

<table>
<tr>
<td width="50%" align="center">

#### 💼 **Enterprise Support**
[![cv4pve-tools](https://img.shields.io/badge/cv4pve--tools-Commercial%20Support-blue?style=for-the-badge)](https://www.corsinvest.it/cv4pve)

Professional support and services

</td>
<td width="50%" align="center">

#### 🌐 **Official Website**
[![Corsinvest](https://img.shields.io/badge/Corsinvest-Official%20Site-green?style=for-the-badge)](https://www.corsinvest.it)

Complete information and documentation

</td>
</tr>
</table>

### 🤝 Community Resources

| Resource | Description |
|----------|-------------|
| **[GitHub Issues](https://github.com/Corsinvest/cv4pve-pepper/issues)** | Bug reports and feature requests |
| **[GitHub Discussions](https://github.com/Corsinvest/cv4pve-pepper/discussions)** | Community Q&A and discussions |
| **[Proxmox Forum](https://forum.proxmox.com/)** | General Proxmox VE community |
| **[cv4pve Documentation](https://www.corsinvest.it/cv4pve)** | Complete documentation suite |

### 🔄 Contributing

We welcome contributions! Here's how you can help:

- 🐛 **Report bugs** via GitHub Issues
- 💡 **Suggest features** through GitHub Discussions  
- 📖 **Improve documentation** with pull requests
- 🧪 **Test releases** and provide feedback
- 🌟 **Star the repository** to show support

---

## 📄 License & Copyright

<div align="center">

**Copyright © Corsinvest Srl**

[![License](https://img.shields.io/badge/License-MIT-blue?style=for-the-badge)](LICENSE.md)

This software is part of the **cv4pve-tools** suite.

**[📜 View Full License](LICENSE.md)** • **[🛡️ Commercial Support](https://www.corsinvest.it/cv4pve)**

---

<sub>Made with ❤️ in Italy 🇮🇹 by [Corsinvest](https://www.corsinvest.it)</sub>

</div>
