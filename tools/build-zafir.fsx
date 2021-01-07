#if INTERACTIVE
#r "../packages/WebSharper.TypeScript/tools/net40/WebSharper.Core.dll"
#r "../packages/WebSharper/lib/net40/WebSharper.JQuery.dll"
#r "../packages/WebSharper.TypeScript/tools/net40/WebSharper.TypeScript.dll"
//#r "C:/dev/websharper.typescript/build/Release/WebSharper.TypeScript.dll"
#I "../packages/NuGet.Core/lib/net40-client"
#r "NuGet.Core"
#r "../packages/IntelliFactory.Core/lib/net45/IntelliFactory.Core.dll"
#r "../packages/IntelliFactory.Build/lib/net45/IntelliFactory.Build.dll"
#load "utility.fsx"
#endif

open System
open System.IO
module C = WebSharper.TypeScript.Compiler
module U = Utility
type JQuery = WebSharper.JQuery.Resources.JQuery

open IntelliFactory.Build

let version = File.ReadAllText(__SOURCE_DIRECTORY__ + "/version.txt")
let v = Version.Parse version

let bt =
    BuildTool().PackageId("WebSharper.BabylonJS", version).VersionFrom("WebSharper", "", "[4.0,5.0)")
    |> PackageVersion.Full.Custom v

let asmVersion =
    sprintf "%i.%i.0.0" v.Major v.Minor

let dts = U.loc ["typings/babylon.2.2.d.ts"]
let lib = U.loc ["packages/WebSharper.TypeScript.Lib/lib/net40/WebSharper.TypeScript.Lib.dll"]
let snk = U.loc [Environment.GetEnvironmentVariable("INTELLIFACTORY"); "keys/IntelliFactory.snk"]
let fsCore = U.loc ["packages/FSharp.Core/lib/net45/FSharp.Core.dll"]

let opts =
    {
        C.Options.Create("WebSharper.BabylonJs", [dts]) with
            AssemblyVersion = Some (Version asmVersion)
//            Renaming = C.Renaming.RemovePrefix ""
            References = [C.ReferenceAssembly.File lib; C.ReferenceAssembly.File fsCore]
            StrongNameKeyFile = Some snk
            Verbosity = C.Level.Verbose
            EmbeddedResources =
                [
                    C.EmbeddedResource.FromFile("babylon.2.2.js")
                ]
            WebSharperResources =
                [
                    C.WebSharperResource.Create("Scripts", "babylon.2.2.js")
                ]
    }

let result =
    C.Compile opts

for msg in result.Messages do
    printfn "%O" msg

let tlibVersion = File.ReadAllText(__SOURCE_DIRECTORY__ + "/tlib-version.txt")

match result.CompiledAssembly with
| None -> ()
| Some asm ->
    let out = U.loc ["build/WebSharper.BabylonJs.dll"]
    let dir = DirectoryInfo(Path.GetDirectoryName(out))
    if not dir.Exists then
        dir.Create()
    printfn "Writing %s" out
    File.WriteAllBytes(out, asm.GetBytes())

    bt.Solution [
        bt.NuGet.CreatePackage()
            .Configure(fun c ->
                { c with
                    Authors = ["IntelliFactory"]
                    Title = Some "WebSharper.BabylonJs 2.2"
                    LicenseUrl = Some "http://websharper.com/licensing"
                    ProjectUrl = Some "http://websharper.com"
                    Description = "WebSharper bindings to Babylon JS (2.2)"
                    RequiresLicenseAcceptance = true })
            .AddDependency("WebSharper.TypeScript.Lib", tlibVersion, forceFoundVersion = true)
            .AddFile("build/WebSharper.BabylonJs.dll", "lib/net40/WebSharper.BabylonJs.dll")
            .AddFile("README.md", "docs/README.md")
    ]
    |> bt.Dispatch
