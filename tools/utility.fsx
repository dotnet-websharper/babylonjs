open System
open System.Diagnostics
open System.Collections
open System.Collections.Specialized
open System.IO

let env =
    ["EnableNuGetPackageRestore", "true"]

let exec (fileName: string) (arguments: string) =
    let psi = ProcessStartInfo()
    psi.FileName <- fileName
    psi.Arguments <- arguments
    psi.CreateNoWindow <- true
    psi.UseShellExecute <- false
    psi.RedirectStandardError <- true
    psi.RedirectStandardOutput <- true
    for (k, v) in env do
        psi.EnvironmentVariables.[k] <- v
    let proc = Process.Start(psi)
    proc.WaitForExit()
    proc.StandardError.ReadToEnd() |> stderr.Write
    proc.StandardOutput.ReadToEnd() |> stdout.Write
    proc.ExitCode = 0

let loc args =
    Path.Combine(__SOURCE_DIRECTORY__, "..", Path.Combine(Array.ofList args))

let nuget cmd =
    exec (loc ["tools/NuGet/NuGet.exe"]) cmd
