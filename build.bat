rmdir build /Q /S
mkdir build
mkdir build\OpenChords
mkdir build\OpenChords\App
mkdir build\OpenChords\App\Bin
mkdir build\OpenChords\Web

cd OpenChords\
call build.bat
cd ..

xcopy  /Y .\OpenChords\bin\Release\OpenChords\App\OpenChords.exe ".\build\OpenChords\App\Bin" 

xcopy  /Y .\OpenChords\bin\Release\OpenChords\App\*.dll ".\build\OpenChords\App\Bin" 

xcopy  /Y .\OpenChords\bin\Release\OpenChords\App\*.config ".\build\OpenChords\App\Bin" 


xcopy  /Y .\OpenChords\bin\Release\OpenChords\App\UpgradeScript.bat ".\build\OpenChords\App\Bin" 

xcopy  /Y .\OpenChords\manual\manual.pdf ".\build\OpenChords\App\Bin" 

xcopy  /Y .\OpenChords\changelog\changelog.txt ".\build\OpenChords\App\Bin" 

xcopy /Y /E .\OpenChordsData\* ".\build\OpenChords\App\Data\"

:: xcopy /Y /E .\OpenChords\OpenChords.Web\* .\build\OpenChords\Web\
:: RMDIR /Q /S .\build\OpenChords\Web\App_Code
:: RMDIR /Q /S .\build\OpenChords\Web\App_Data
:: RMDIR /Q /S .\build\OpenChords\Web\obj
:: RMDIR /Q /S .\build\OpenChords\Web\Properties
:: RMDIR /Q /S .\build\OpenChords\Web\Scripts
:: RMDIR /Q /S .\build\OpenChords\Web\Bin\Scripts
:: RMDIR /Q /S .\build\OpenChords\Web\Bin\App_Themes
:: DEL /F .\build\OpenChords\Web\Web.Debug.config
:: DEL /F .\build\OpenChords\Web\Web.Release.config
:: DEL /F /S /Q .\build\OpenChords\Web\*.cs
:: DEL /F /S /Q .\build\OpenChords\Web\*.csproj
:: DEL /F /S /Q .\build\OpenChords\Web\*.user
:: DEL /F /S /Q .\build\OpenChords\Web\Bin\*.pdb
:: xcopy /Y .\build\OpenChords\Web\OpenChordsWebSettings.build.xml .\build\OpenChords\Web\OpenChordsWebSettings.xml
:: DEL /F /Q .\build\OpenChords\Web\OpenChordsWebSettings.build.xml


echo cd app\bin >> .\build\OpenChords\StartOpenChords.bat
echo start OpenChords.exe >> .\build\OpenChords\StartOpenChords.bat