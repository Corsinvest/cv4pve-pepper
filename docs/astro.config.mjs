// @ts-check
import { defineConfig } from 'astro/config';
import starlight from '@astrojs/starlight';
import corsinvestTheme from '@corsinvest/cv4pve-docs-theme';

export default defineConfig({
  site: 'https://corsinvest.github.io',
  base: '/cv4pve-pepper',
  integrations: [
    starlight({
      title: 'cv4pve-pepper',
      description: 'Open the SPICE or VNC console of a Proxmox VE VM or container with one command, from your desktop, a script or a shortcut.',
      // Brand, product icon, GitHub link, the Corsinvest sidebar group and
      // external links in a new tab come from the shared cv4pve theme.
      plugins: [
        corsinvestTheme({
          repo: 'cv4pve-pepper',
          // Product icon: favicon and header, dark variant for the dark theme.
          icon: { light: '/icon.svg', dark: '/icon-dark.svg' },
          // No `admin` option: cv4pve-admin opens consoles in the browser (noVNC, xterm.js), not with this engine.
          // Visits, without cookies.
          matomo: { url: 'https://matomo.corsinvest.it/', siteId: 8 },
          // Steps panel in the home hero: the same steps, in the same order and words, as Getting started.
          // The commands are in the pages: pepper needs remote-viewer, and its path differs on each system.
          steps: {
            items: [
              'Install cv4pve-pepper',
              { text: 'Install remote-viewer', href: 'getting-started/' },
              { text: 'Create an API token', href: 'permissions/#user-and-token' },
              'Open the first console',
            ],
          },
        }),
      ],
      sidebar: [
        { label: 'Start here', items: ['getting-started', 'permissions', 'connection', 'ai-agents', 'troubleshooting'] },
        { label: 'Using pepper', items: ['options', 'spice-and-vnc', 'desktop-shortcut'] },
      ],
    }),
  ],
});
