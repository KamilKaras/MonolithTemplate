import react from "@vitejs/plugin-react";
import mkcert from "vite-plugin-mkcert";
import { defineConfig } from "vitest/config";

export default defineConfig({
  plugins: [react(), mkcert()],
  server: {
    host: true,
    port: 3000,
  },
  css: {
    preprocessorOptions: {
      scss: {
        silenceDeprecations: ["color-functions", "global-builtin", "import"],
      },
    },
  },
  test: {
    environment: "jsdom",
    setupFiles: "./src/test/setup.ts",
    globals: false,
    clearMocks: true,
  },
});
