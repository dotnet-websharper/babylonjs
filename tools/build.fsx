#r "../packages/WebSharper.TypeScript/tools/net40/WebSharper.Core.dll"
#r "../packages/WebSharper/lib/net40/WebSharper.JQuery.dll"
#r "../packages/WebSharper.TypeScript/tools/net40/WebSharper.TypeScript.dll"
//#r "C:/dev/websharper.typescript/build/Release/WebSharper.TypeScript.dll"
#I "../packages/NuGet.Core/lib/net40-client"
#r "NuGet.Core"
#r "../packages/IntelliFactory.Core/lib/net45/IntelliFactory.Core.dll"
#r "../packages/IntelliFactory.Build/lib/net45/IntelliFactory.Build.dll"
#load "utility.fsx"

open System
open System.IO
module C = WebSharper.TypeScript.Compiler
module U = Utility
type JQuery = WebSharper.JQuery.Resources.JQuery

open IntelliFactory.Build
let version =
    let bt = BuildTool().PackageId("WebSharper.BabylonJS", "3.0")
    let v = PackageVersion.Full.Find(bt).ToString()
    let s = match PackageVersion.Current.Find(bt).Suffix with Some s -> "-" + s | None -> ""
    v + s

let dts = U.loc ["typings/babylon.d.ts"]
let lib = U.loc ["packages/WebSharper.TypeScript.Lib/lib/net40/WebSharper.TypeScript.Lib.dll"]
let snk = U.loc [Environment.GetEnvironmentVariable("INTELLIFACTORY"); "keys/IntelliFactory.snk"]

let fsCore =
    U.loc [
        Environment.GetEnvironmentVariable("ProgramFiles(x86)")
        "Reference Assemblies/Microsoft/FSharp/.NETFramework/v4.0/4.3.0.0/FSharp.Core.dll"
    ]

let opts =
    {
        C.Options.Create("WebSharper.BabylonJs", [dts]) with
            AssemblyVersion = Some (Version "3.0.0.0")
//            Renaming = C.Renaming.RemovePrefix ""
            References = [C.ReferenceAssembly.File lib; C.ReferenceAssembly.File fsCore]
            StrongNameKeyFile = Some snk
            Verbosity = C.Level.Verbose
            EmbeddedResources =
                [
                    C.EmbeddedResource.FromFile("babylon.1.12.js")
                ]
            WebSharperResources =
                [
                    C.WebSharperResource.Create("Scripts", "babylon.1.12.js")
                ]
    }

let result =
    C.Compile opts

for msg in result.Messages do
    printfn "%O" msg

match result.CompiledAssembly with
| None -> ()
| Some asm ->
    let out = U.loc ["build/WebSharper.BabylonJs.dll"]
    let dir = DirectoryInfo(Path.GetDirectoryName(out))
    if not dir.Exists then
        dir.Create()
    printfn "Writing %s" out
    File.WriteAllBytes(out, asm.GetBytes())

let (|I|_|) (x: string) =
    match x with
    | null | "" -> None
    | n ->
        match Int32.TryParse(n) with
        | true, r -> Some r
        | _ -> None

let ok =
    match Environment.GetEnvironmentVariable("NuGetPackageOutputPath") with
    | null | "" ->
        U.nuget (sprintf "pack -out build/ -version %s BabylonJs.nuspec" version)
    | path ->
        U.nuget (sprintf "pack -out %s -version %s BabylonJs.nuspec" path version)

printfn "pack: %b" ok
if not ok then exit 1 else exit 0
