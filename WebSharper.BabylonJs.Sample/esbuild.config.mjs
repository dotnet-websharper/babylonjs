import { build } from 'esbuild'
import { readFileSync, writeFileSync, copyFileSync } from 'fs'

const name = 'WebSharper.BabylonJs.Sample'

// The WebSharper-generated module imports the npm `@babylonjs/core` package
// (and the WebSharper runtime); bundle it all into a single IIFE.
await build({
  entryPoints: [`./build/${name}.js`],
  bundle: true,
  minify: true,
  format: 'iife',
  outfile: `../dist/Scripts/${name}.min.js`,
  globalName: 'wsbundle'
})

copyFileSync('./site.css', '../dist/site.css')

// Fold the WebSharper head (linked resources) into the page.
const head = readFileSync(`./build/${name}.head.html`, 'utf8')
const html = readFileSync('./index.html', 'utf8').replace('<!-- ws-head -->', head)
writeFileSync('../dist/index.html', html)

console.log('Bundled:', name)