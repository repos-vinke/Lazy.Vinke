@echo off

:: Release

echo Builder Release
echo:

echo Copying Defaults...
xcopy /d /e /y .\Defaults\ ..\..\..\Release\ >> nul
del ..\..\..\Release\.gitkeep >>nul 2>>&1
del ..\..\..\Release\Both\.gitkeep >>nul 2>>&1
del ..\..\..\Release\Client\.gitkeep >>nul 2>>&1
del ..\..\..\Release\Server\.gitkeep >>nul 2>>&1
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
