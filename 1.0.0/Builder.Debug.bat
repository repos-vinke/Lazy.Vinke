@echo off

cd .\Modules\Lazy.Vinke\Sources\
call .\Lazy.Vinke.Builder.Debug.bat
cd ..\..\..\

cd .\Modules\Lazy.Vinke.Json\Sources\
call .\Lazy.Vinke.Json.Builder.Debug.bat
cd ..\..\..\
