import react from "@vitejs/plugin-react";
import { defineConfig } from "vite";

export default defineConfig({
  plugins: [react()],
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
});
