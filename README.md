# cv4pve-pepper

[![License](https://img.shields.io/github/license/Corsinvest/cv4pve-pepper.svg)](LICENSE.md)

```text
     ______                _                      __
    / ____/___  __________(_)___ _   _____  _____/ /_
   / /   / __ \/ ___/ ___/ / __ \ | / / _ \/ ___/ __/
  / /___/ /_/ / /  (__  ) / / / / |/ /  __(__  ) /_
  \____/\____/_/  /____/_/_/ /_/|___/\___/____/\__/


  Launching SPICE remote-viewer for Proxmox VE   (Made in Italy)

  cv4pve-pepper is a part of suite cv4pve.
  For more information visit https://www.corsinvest.it/cv4pve

Usage:
  cv4pve-pepper [options]

Options:
  --api-token <api-token>                Api token format 'USER@REALM!TOKENID=UUID'. Require Proxmox VE 6.2 or later
  --username <username>                  User name <username>@<realm>
  --password <password>                  The password. Specify 'file:path_file' to store password in file.
  --validate-certificate                 Validate SSL Certificate Proxmox VE node.
  --host <host> (REQUIRED)               The host name host[:port],host1[:port],host2[:port]
  --vmid <vmid>                          The id or name VM/CT
  --proxy <proxy>                        SPICE proxy server. This can be used by the client to specify the proxy server. All nodes in a cluster runs
                                         'spiceproxy', so it is up to the client to choose one. By default, we return the node to connect. If specify
                                         http(s)://[host]:[port] then replace proxy option in file .vv. E.g. for reverse proxy.
  --viewer <viewer> (REQUIRED)           Executable SPICE client remote viewer (remote-viewer executable)
  --viewer-options <viewer-options>      Send options directly SPICE Viewer (quote value).
  --start-or-resume                      Run stopped or paused VM
  --wait-for-startup <wait-for-startup>  Wait sec. for startup VM [default: 5]
  --version                              Show version information
  --debug                                Show debug information
  -?, -h, --help                         Show help and usage information
```

## Copyright and License

Copyright: Corsinvest Srl
For licensing details please visit [LICENSE.md](LICENSE.md)

## Commercial Support

This software is part of a suite of tools called cv4pve-tools. If you want commercial support, visit the [site](https://www.corisnvest.it/cv4pve)

## Introduction

Launching SPICE remote-viewer having access VM running on Proxmox VE.

this software aims to simplify run SPICE client from Proxmox VE using command line. The reasons are:

* Proxmox VE uses tickets that expire
* do not use graphical interface (GUI)
* no download .vv file to run remove viewer
* use a simple client

## Main features

* Completely written in C#
* Use native api REST Proxmox VE (library C#)
* Independent os (Windows, Linux, Macosx)
* Installation unzip file extract binary
* Not require installation in Proxmox VE
* Execute out side Proxmox VE
* Not require Web login
* Support multiple host for HA in --host parameter es. host[:port],host1[:port],host2[:port]
* Start or Resume VM on connection
* Check-Update and Upgrade application
* Use Api token --api-token parameter
* Send options directly to viewer
* Execution with file parameter e.g. @FileParameter.parm
* Validate certificate SSL, default not validate

## Api token

From version 6.2 of Proxmox VE is possible to use [Api token](https://pve.proxmox.com/pve-docs/pveum-plain.html).
This feature permit execute Api without using user and password.
If using **Privilege Separation** when create api token remember specify in permission.

## Configuration and use

E.g. install on linux 64

Download last package e.g. Debian cv4pve-pepper-linux-x64.zip, on your os and install:

```sh
root@debian:~# unzip cv4pve-pepper-linux-x64.zip
```

This tool need basically no configuration.

## Run

```sh
root@debian:~# cv4pve-pepper --host=192.168.0.100 --username=root@pam --password=fagiano --vmid 100 --viewer path-spice-viewer
```

## SPICE client

* [Windows: virt-viewer 0.5.6 or higher,](http://www.spice-space.org/download.html)

* Linux: virt-viewer 0.5.6 or higher

* [OS X (not yet working as expected): virt-viewer 0.5.7 or higher](https://www.spice-space.org/osx-client.html)

## Topical path of remote viewer

* Linux /usr/bin/remote-viewer
* Windows C:\Program Files\VirtViewer v?.?-???\bin\remote-viewer.exe

## Options of remote viewer

Use --viewer-options to send options to viewer.
E.g. --viewer-options "-f" for full screen.

## Error

* **no spice port**: This error appears when you have not configured the display hardware on SPICE.

## Execution with file parameter

Is possible execute with file parameter

```sh
root@debian:~# cv4pve-pepper @FileParameter.parm
```

File **FileParameter.parm**

```txt
--host=192.168.0.100
--username=root@pam
--password=fagiano
--vmid 100
--viewer path-spice-viewer
```
