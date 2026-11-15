#!/bin/bash
set -e

dotnet tool restore
dotnet paket update -g wsbuild --no-install

if [ "$#" -eq 0 ]; then
    dotnet fsi ./build.fsx
else
    dotnet fsi ./build.fsx -t "$@"
fi