rmdir Staging /Q /S
rmdir Release /Q /S

call build.bat

:: copy all needed files to run the application
xcopy  /Y /E .\OpenChords.CrossPlatform.Executable\bin\Release\*    .\Staging\OpenChords\App\

::copy the settings files
xcopy /Y .\settings.xml .\Staging\OpenChords\App

::Make 7zip files for portable version
git describe --tags > version.txt
set /p gitVersion=<version.txt
7za.exe a -tzip .\Release\OpenChords.Portable.%gitVersion%.zip .\Staging\* -mx9
7za.exe a -t7z .\Release\OpenChords.Portable.%gitVersion%.7z .\Staging\* -mx9 -ms=on -m0=lzma

::Copy readme file for sourceforge
copy /Y .\ChangeLog\changelog.txt .\Release\README.txt 

::Make Installer
SET InnoPath="packages\Tools.InnoSetup.6.2.0\tools\ISCC.exe"
%InnoPath% OpenChords.Setup\OpenChords.Setup.iss 
move OpenChords.Setup\Output\* .\Release\


