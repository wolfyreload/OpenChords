rmdir Staging /Q /S
rmdir Release /Q /S

mkdir Staging\App
mkdir Release

call build.bat

xcopy  /Y /E .\OpenChordsLoader\bin\Release\* .\Staging\App 
xcopy  /Y .\OpenChords\manual\manual.pdf .\Staging\App
copy /Y .\portable.xml .\Staging\App\settings.xml

echo blank >> .\staging\App\settings.xml
echo cd app >> .\staging\StartOpenChords.bat
echo start OpenChords.exe >> .\staging\StartOpenChords.bat

7za.exe a -tzip .\Release\OpenChords.zip .\Staging\* 
7za.exe a .\Release\OpenChords.7z .\Staging\* 

"C:\Program Files (x86)\Inno Setup 5\ISCC.exe" /cc OpenChords.Setup\OpenChords.Setup.iss 
move OpenChords.Setup\Output\* .\Release\


