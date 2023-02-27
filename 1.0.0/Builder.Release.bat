@echo off

cd .\Modules\Lazy.Vinke\Sources\
call .\Lazy.Vinke.Builder.Release.bat
cd ..\..\..\

cd .\Modules\Lazy.Vinke.Json\Sources\
call .\Lazy.Vinke.Json.Builder.Release.bat
cd ..\..\..\

cd .\Modules\Lazy.Vinke.Database\Sources\
call .\Lazy.Vinke.Database.Builder.Release.bat
cd ..\..\..\
