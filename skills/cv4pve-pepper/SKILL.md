---
name: cv4pve-pepper
description: Open the SPICE or VNC console of a Proxmox VE VM or container in a window on the user's desktop with cv4pve-pepper and remote-viewer, or write the parameter file and shortcut that does it. Use it when the user asks to open, see or connect to the console or screen of a VM. It opens a window for the user; it does not read the cluster or show you the screen.
---

# cv4pve-pepper

`cv4pve-pepper` asks Proxmox VE for a console ticket and starts `remote-viewer` with it: a window opens on
the desktop of the machine where the command runs. It has no subcommands. The connection options (host and
API token) and the path of the viewer (`--viewer`) are in a file the user names: if you do not know its
path, ask.

## Rules

- Connect only with that file, passed with `@`. Do not print it or copy the token anywhere.
- The window is for the user: you cannot see or use the console. Tell the user that a window opens.
- Pass the exact id or name in `--vmid`. A pattern (`%web%`, `100:199`) takes the first match by node name
  and id, which may not be the guest the user means: if the user gave only part of a name, ask.
- `--start-or-resume` starts a stopped guest or resumes a paused one: it changes the cluster, with
  `--dry-run` too. Add it only after the user agrees to start that guest.
- `--dry-run` does everything except starting the viewer: use it to check the connection, the guest and
  the privileges without opening a window.
- SPICE is the default and pepper returns as soon as the viewer has started. With `--vnc` pepper runs until
  the user closes the viewer: start it in the background, or the command does not return.
- In PowerShell or `cmd` on Windows the terminal does not wait for pepper and gives no exit code: run it
  with `Start-Process cv4pve-pepper -ArgumentList '@<file>' -Wait -PassThru -NoNewWindow` and read
  `ExitCode`. From bash it waits as any other command.
- In an options file a value with spaces goes in quotes, as the path of the viewer on Windows:
  `--viewer="C:\Program Files\VirtViewer v11.0-256\bin\remote-viewer.exe"`.
- Exit code 0 means the viewer started (SPICE), ended normally (VNC), or the dry run prepared everything;
  1 is an error, on a line starting `ERROR:`.
- If an option is refused, check `cv4pve-pepper --help`: this skill can be newer than the tool.

## Commands

```bash
cv4pve-pepper @<options-file> --vmid=<id|name> --dry-run          # check, no window
cv4pve-pepper @<options-file> --vmid=<id|name>                    # SPICE console
cv4pve-pepper @<options-file> --vmid=<id|name> --vnc              # VNC console; runs until the viewer is closed
cv4pve-pepper @<options-file> --vmid=<id|name> --start-or-resume  # after agreement: start it, then open
```

## Errors

| Message | What to do |
|---|---|
| `ERROR: VM/CT '<x>' not found!` | Wrong id or name, or the token lacks `VM.Audit` on the guest |
| `ERROR: no spice port` | The display of the VM is not SPICE: use `--vnc` |
| `ERROR: VM <id> not running` | The guest is stopped: ask the user whether to add `--start-or-resume` |
| `ERROR: Start VM/CT <id> failed: …` | The start was refused or failed: often `VM.PowerMgmt` is missing |
| `Option '--viewer' is required.` | The options file has no `--viewer`: ask the user for the path of `remote-viewer` |

SPICE connects from the user's machine to the SPICE proxy on port 3128; VNC goes through the API port only.
If the SPICE window opens and closes, try `--vnc`.

Documentation: https://corsinvest.github.io/cv4pve-pepper/
