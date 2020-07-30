# cv4pve-pepper

[![License](https://img.shields.io/github/license/Corsinvest/cv4pve-pepper.svg)](LICENSE.md) [![AppVeyor branch](https://img.shields.io/appveyor/ci/franklupo/cv4pve-pepper/master.svg)](https://ci.appveyor.com/project/franklupo/cv4pve-pepper)

```text
    ______                _                      __
   / ____/___  __________(_)___ _   _____  _____/ /_
  / /   / __ \/ ___/ ___/ / __ \ | / / _ \/ ___/ __/
 / /___/ /_/ / /  (__  ) / / / / |/ /  __(__  ) /_
 \____/\____/_/  /____/_/_/ /_/|___/\___/____/\__/

Launching SPICE on Proxmox VE                  (Made in Italy)

Usage: cv4pve-pepper [options]

Options:
  -?|-h|--help  Show help information
  --version         Show version information
  --host            The host name host[:port],host1[:port],host2[:port]
  --api-token       Api token format 'USER@REALM!TOKENID=UUID'. Require Proxmox VE 6.2 or later
  --username        User name <username>@<realm>
  --password        The password. Specify 'file:path_file' to store password in file.
  --vmid            The id or name VM/CT
  --viewer          Executable SPICE client remote viewer

Commands:
  app-check-update  Check update application
  app-upgrade       Upgrade application

Run 'cv4pve-pepper [command] --help' for more information about a command.

cv4pve-pepper is a part of suite cv4pve-tools.
For more information visit https://www.cv4pve-tools.com
```

## Copyright and License

Copyright: Corsinvest Srl
For licensing details please visit [LICENSE.md](LICENSE.md)

## Commercial Support

This software is part of a suite of tools called cv4pve-tools. If you want commercial support, visit the [site](https://www.cv4pve-tools.com)

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
* Check-Update and Upgrade application
* Use Api token --api-token parameter

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

## Topical path of remove viewer

* Linux /usr/bin/remote-viewer
* Windows C:\Program Files\VirtViewer v?.?-???\bin\remote-viewer.exe
