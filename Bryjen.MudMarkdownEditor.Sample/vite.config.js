import { defineConfig } from 'vite';

export default defineConfig({
    build: {
        outDir: './wwwroot/dist', // Output directory for the bundle
        rollupOptions: {
            input: './wwwroot/js/index.js', // Entry point for your wrapper
            output: {
                entryFileNames: 'index.js', // Specify the output filename
                assetFileNames: 'assets/[name][extname]', // Keep assets in the 'assets' folder
                chunkFileNames: 'chunks/[name].js', // Optional: Specify names for chunks
            },
        },
    },
});
