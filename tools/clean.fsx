#load "utility.fsx"
open System
open System.IO

let rm path =
    let d = DirectoryInfo(Utility.loc path)
    if d.Exists then
        d.Delete(``recursive`` = true)

rm ["build"]
rm ["packages"]
rm ["tools/packages"]
rm ["node_modules"]
