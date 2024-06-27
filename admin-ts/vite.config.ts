import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import ckeditor5 from '@ckeditor/vite-plugin-ckeditor5'
import tailwindcss from 'tailwindcss'

import { createRequire } from 'node:module'
import { resolve } from 'node:path'
const require = createRequire(import.meta.url)

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react(), ckeditor5({ theme: require.resolve('@ckeditor/ckeditor5-theme-lark') })],
  server: {
    port: 3000
  },
  css: {
    postcss: {
      plugins: [tailwindcss()]
    }
  },
  optimizeDeps: {
    exclude: ['js-big-decimal']
  }
})
