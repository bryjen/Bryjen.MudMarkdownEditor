import { defineConfig } from 'vite';
import rawPlugin from 'vite-plugin-raw';

export default defineConfig({
    build: {
        outDir: './wwwroot/starry-night/out/js',
        rollupOptions: {
            input: './wwwroot/starry-night/in/js/wrapper.js',
            output: {
                entryFileNames: 'wrapper.out.js',
                assetFileNames: 'assets/[name][extname]',
                chunkFileNames: 'chunks/[name].js',
            },
        },
    },
    plugins: [
        rawPlugin({
            match: /\.css$/,
            exclude: null,   // Include all files, including node_modules
        }),
    ]
});
