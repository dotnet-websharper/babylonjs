@ECHO OFF
setlocal
set PATH=%PATH%;%ProgramFiles(x86)%\Microsoft SDKs\F#\4.0\Framework\v4.0
set PATH=%PATH%;%ProgramFiles%\Microsoft SDKs\F#\4.0\Framework\v4.0
fsi.exe --exec tools/configure-zafir.fsx

xcopy /y /q packages\Zafir.TypeScript\tools\net40\WebSharper.Core.dll tools
xcopy /y /q packages\Zafir.TypeScript\tools\net40\WebSharper.TypeScript.dll tools
xcopy /y /q packages\Zafir.TypeScript\tools\net40\WebSharper.Core.JavaScript.dll tools
xcopy /y /q packages\Zafir.TypeScript\tools\net40\WebSharper.Compiler.dll tools
xcopy /y /q packages\Zafir.TypeScript\tools\net40\WebSharper.JQuery.dll tools
xcopy /y /q packages\Zafir.TypeScript\tools\net40\Mono.Cecil.dll tools
xcopy /y /q packages\Zafir.TypeScript\tools\net40\Mono.Cecil.Pdb.dll tools
xcopy /y /q packages\Zafir.TypeScript\tools\net40\Mono.Cecil.Mdb.dll tools
xcopy /y /q packages\Zafir.TypeScript\tools\net40\FParsec.dll tools
xcopy /y /q packages\Zafir.TypeScript\tools\net40\FParsecCS.dll tools
xcopy /y /q packages\NuGet.Core\lib\net40-client\NuGet.Core.dll tools
xcopy /y /q packages\IntelliFactory.Core\lib\net45\IntelliFactory.Core.dll tools
xcopy /y /q packages\IntelliFactory.Build\lib\net45\IntelliFactory.Build.dll tools
fsc.exe -o:tools\build-zafir.exe -r:tools\WebSharper.JQuery.dll -r:tools\WebSharper.Core.dll -r:tools\WebSharper.TypeScript.dll -r:tools\NuGet.Core.dll -r:tools\IntelliFactory.Core.dll -r:tools\IntelliFactory.Build.dll tools\utility.fsx tools\build-zafir.fsx
tools\build-zafir.exe
