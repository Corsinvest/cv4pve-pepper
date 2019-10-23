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
  --host        The host name host[:port]
  --username    User name <username>@<realm>
  --password    The password. Specify 'file:path_file' to store password in file.
  --version     Show version information
  --vmid        The id or name VM/CT
  --viewer      Executable SPICE client remote viewer```
```

## Copyright and License

Copyright: Corsinvest Srl
For licensing details please visit [LICENSE.md](LICENSE.md)

## Commercial Support

This software is part of a suite of tools called cv4pve-tools. If you want commercial support, visit the [site](https://www.corsinvest.it/cv4pve-tools)

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
