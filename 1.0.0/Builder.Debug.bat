@echo off

:: Debug

echo Builder Debug
echo:

echo Copying Defaults...
xcopy /d /e /y .\Defaults\ ..\..\..\Debug\ >> nul
del ..\..\..\Debug\.gitkeep >>nul 2>>&1
del ..\..\..\Debug\Both\.gitkeep >>nul 2>>&1
del ..\..\..\Debug\Client\.gitkeep >>nul 2>>&1
del ..\..\..\Debug\Server\.gitkeep >>nul 2>>&1
echo:

cd .\Modules\Lazy.Vinke\Sources\
call .\Lazy.Vinke.Builder.Debug.bat
cd ..\..\..\

cd .\Modules\Lazy.Vinke.Json\Sources\
call .\Lazy.Vinke.Json.Builder.Debug.bat
cd ..\..\..\

cd .\Modules\Lazy.Vinke.Database\Sources\
call .\Lazy.Vinke.Database.Builder.Debug.bat
cd ..\..\..\

cd .\Modules\Lazy.Vinke.Windows\Sources\
call .\Lazy.Vinke.Windows.Builder.Debug.bat
cd ..\..\..\
