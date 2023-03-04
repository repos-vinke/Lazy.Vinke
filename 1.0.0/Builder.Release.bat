@echo off

:: Release

echo Builder Release
echo:

echo Copying dependencies...
xcopy /e /y .\Dependencies\ ..\..\..\Release\ >> nul
del ..\..\..\Release\.gitkeep >>nul 2>>&1
echo:

cd .\Modules\Lazy.Vinke\Sources\
call .\Lazy.Vinke.Builder.Release.bat
cd ..\..\..\

cd .\Modules\Lazy.Vinke.Json\Sources\
call .\Lazy.Vinke.Json.Builder.Release.bat
cd ..\..\..\

cd .\Modules\Lazy.Vinke.Database\Sources\
call .\Lazy.Vinke.Database.Builder.Release.bat
cd ..\..\..\

cd .\Modules\Lazy.Vinke.Windows\Sources\
call .\Lazy.Vinke.Windows.Builder.Release.bat
cd ..\..\..\
