rmdir Staging /Q /S
rmdir Release /Q /S

call build.bat

:: copy all needed files to run the application
xcopy  /Y /E .\OpenChords.CrossPlatform.Executable\bin\Release\*    .\Staging\OpenChords\App\

::copy the settings files
xcopy /Y .\settings.xml .\Staging\OpenChords\App

::Make 7zip files for portable version
7za.exe a -tzip .\Release\OpenChords.Portable.zip .\Staging\* -mx9
7za.exe a -t7z .\Release\OpenChords.Portable.7z .\Staging\* -mx9 -ms=on -m0=lzma

::Copy readme file for sourceforge
copy /Y .\ChangeLog\changelog.txt .\Release\README.txt 

::Make Installer
SET InnoPath="packages\Tools.InnoSetup.5.5.9\tools\ISCC.exe"
%InnoPath% OpenChords.Setup\OpenChords.Setup.iss 
move OpenChords.Setup\Output\* .\Release\


