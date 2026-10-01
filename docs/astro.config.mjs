// @ts-check
import { defineConfig } from 'astro/config';
import starlight from '@astrojs/starlight';
import corsinvestTheme from '@corsinvest/cv4pve-docs-theme';

const releases = 'https://github.com/Corsinvest/cv4pve-pepper/releases/latest/download';
const run = ['--host=pve01', "--api-token='pepper@pve!console=…'", '--vmid=100'];

export default defineConfig({
  site: 'https://corsinvest.github.io',
  base: '/cv4pve-pepper',
  integrations: [
    starlight({
      title: 'cv4pve-pepper',
      description: 'Open the SPICE or VNC console of a Proxmox VE VM or container with one command, from your desktop, a script or a shortcut.',
      // Brand, logo, GitHub and "Edit page" links, the Corsinvest sidebar group and
      // external links in a new tab come from the shared cv4pve theme.
      plugins: [
        corsinvestTheme({
          repo: 'cv4pve-pepper',
          // Product icon: favicon and header, dark variant for the dark theme.
          icon: { light: '/icon.svg', dark: '/icon-dark.svg' },
          // No admin banner: cv4pve-admin opens consoles in the browser (noVNC, xterm.js), not with this engine.
          // Visits, without cookies.
          matomo: { url: 'https://matomo.corsinvest.it/', siteId: 8 },
          // Install-and-run panel in the home hero. pepper needs remote-viewer, and its path differs
          // on each system, so the targets are written out instead of the presets.
          install: {
            targets: [
              {
                id: 'windows',
                label: 'Windows',
                icon: 'windows',
                lines: [
                  '# install',
                  'winget install Corsinvest.cv4pve.pepper',
                  'winget install RedHat.VirtViewer',
                  '',
                  '# open the console of VM 100',
                  `cv4pve-pepper ${run.join(' ')} \``,
                  '  --viewer="C:\\Program Files\\VirtViewer v11.0-256\\bin\\remote-viewer.exe"',
                ],
              },
              {
                id: 'linux',
                label: 'Linux',
                icon: 'linux',
                lines: [
                  '# install (x64 — arm64 and arm on the Releases page)',
                  `wget ${releases}/\\\ncv4pve-pepper-linux-x64.zip`,
                  'unzip cv4pve-pepper-linux-x64.zip',
                  'chmod +x cv4pve-pepper',
                  'sudo apt install virt-viewer',
                  '',
                  '# open the console of VM 100',
                  `./cv4pve-pepper ${run.join(' ')} \\`,
                  '  --viewer=/usr/bin/remote-viewer',
                ],
              },
              {
                id: 'macos',
                label: 'macOS',
                icon: 'macos',
                lines: [
                  '# install (remote-viewer: see Getting started)',
                  'brew install corsinvest/tap/cv4pve-pepper',
                  '',
                  '# open the console of VM 100',
                  `cv4pve-pepper ${run.join(' ')} \\`,
                  '  --viewer=/path/to/remote-viewer',
                ],
              },
            ],
          },
        }),
      ],
      lastUpdated: true,
      sidebar: [
        { label: 'Start here', items: ['getting-started', 'permissions', 'connection', 'ai-agents', 'troubleshooting'] },
        { label: 'Using pepper', items: ['options', 'spice-and-vnc', 'desktop-shortcut'] },
      ],
    }),
  ],
});
