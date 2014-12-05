open System
open System.IO

#load "utility.fsx"

try Directory.Delete(Utility.loc ["packages"], true) with _ -> ()
let ok =
    Utility.nuget "install WebSharper -pre -o packages -excludeVersion -nocache"
    && Utility.nuget "install WebSharper.TypeScript -pre -o packages -excludeVersion -nocache"
    && Utility.nuget "install WebSharper.TypeScript.Lib -pre -o packages -excludeVersion -nocache"
    && Utility.nuget "install IntelliFactory.Build -pre -o packages -excludeVersion -nocache"

printfn "configure: %b" ok
if not ok then exit 1 else exit 0
