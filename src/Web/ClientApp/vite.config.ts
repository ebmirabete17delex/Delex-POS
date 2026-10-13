import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import tailwindcss from '@tailwindcss/vite';
import { fileURLToPath } from 'node:url';

const clientRoot = fileURLToPath(new URL('.', import.meta.url));
const srcRoot = fileURLToPath(new URL('./src', import.meta.url));

const target =
  process.env['services__webapi__https__0'] ||
  process.env['services__webapi__http__0'];

const proxyOptions = target
  ? { target, secure: false, changeOrigin: true }
  : undefined;

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    react(),
    tailwindcss(),
  ],
  resolve: {
    alias: [
      { find: '@/components', replacement: `${srcRoot}/components` },
      { find: '@/hooks', replacement: `${srcRoot}/hooks` },
      { find: '@', replacement: clientRoot },
    ],
  },
  server: {
    port: parseInt(process.env.PORT!),
    proxy: proxyOptions
      ? {
          '/api': proxyOptions,
          '/openapi': proxyOptions,
          '/scalar': proxyOptions,
          '/weatherforecast': proxyOptions,
          '/WeatherForecast': proxyOptions,
        }
      : undefined,
  },
  build: {
    outDir: 'build',
  },
});
