rmdir Staging /Q /S
rmdir Release /Q /S

mkdir Release

call build.bat

call Manual\MakeHelpDocument.bat

:: copy all needed files to run the application
xcopy  /Y /E .\OpenChordsLoader\bin\Release\*                .\Staging\LegacyApp\ 
xcopy  /Y /E .\OpenChords.CrossPlatform.Wpf\bin\Release\*    .\Staging\WpfApp\
xcopy  /Y /E .\OpenChords.CrossPlatform.Gtk2\bin\Release\*   .\Staging\Gtk2App\ 

:: copy the manual
xcopy  /Y /E .\Manual\Help.html .\Staging\Manual\
xcopy  /Y /E .\Manual\HelpImages\* .\Staging\Manual\HelpImages

:: make settings file for legacy version
copy /Y .\portable.xml .\Staging\LegacyApp\settings.xml

::Put manual into each folder
xcopy /Y /E .\Staging\Manual\* .\Staging\LegacyApp\
xcopy /Y /E .\Staging\Manual\* .\Staging\WpfApp\
xcopy /Y /E .\Staging\Manual\* .\Staging\Gtk2App\ 

::cleanup files in staging
pushd Staging
del /s *.pdb
del /s *Eto.xml
del /s *log4net.xml
del /s *.vshost.exe*
popd

echo cd app >> .\staging\StartOpenChords.bat
echo start OpenChords.exe >> .\staging\StartOpenChords.bat

7za.exe a -tzip .\Release\OpenChords.zip .\Staging\* 
7za.exe a .\Release\OpenChords.7z .\Staging\* 

"C:\Program Files (x86)\Inno Setup 5\ISCC.exe" /cc OpenChords.Setup\OpenChords.Setup.iss 
move OpenChords.Setup\Output\* .\Release\


