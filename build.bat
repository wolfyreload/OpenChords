rmdir build /Q /S
mkdir build
mkdir build\OpenChords
mkdir build\OpenChords\App
mkdir build\OpenChords\App\Bin

cd OpenChords\
call build.bat
cd ..

xcopy  /Y .\OpenChords\bin\OpenChords\App\OpenChords.exe ".\build\OpenChords\App\Bin" 

xcopy  /Y .\OpenChords\bin\OpenChords\App\*.dll ".\build\OpenChords\App\Bin" 

xcopy  /Y .\OpenChords\bin\OpenChords\App\*.config ".\build\OpenChords\App\Bin" 


xcopy  /Y .\OpenChords\bin\OpenChords\App\UpgradeScript.bat ".\build\OpenChords\App\Bin" 

xcopy  /Y .\OpenChords\manual\manual.pdf ".\build\OpenChords\App\Bin" 

xcopy  /Y .\OpenChords\changelog\changelog.txt ".\build\OpenChords\App\Bin" 

xcopy /Y /E .\OpenChordsData\* ".\build\OpenChords\App\Data\"

echo cd app\bin >> .\build\OpenChords\StartOpenChords.bat
echo start OpenChords.exe >> .\build\OpenChords\StartOpenChords.bat