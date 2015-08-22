rmdir Staging /Q /S
rmdir Release /Q /S

call build.bat

call Manual\MakeHelpDocument.bat

:: copy all needed files to run the application
xcopy  /Y /E .\OpenChords.CrossPlatform.Wpf\bin\Release\*    .\Staging\WindowsApp\App\
xcopy  /Y /E .\OpenChords.CrossPlatform.Gtk2\bin\Release\*   .\Staging\LinuxApp\App\ 

:: copy the manual
xcopy  /Y /E .\Manual\Help.html .\Staging\Manual\
xcopy  /Y /E .\Manual\HelpImages\* .\Staging\Manual\HelpImages

::Put manual into each folder
xcopy /Y /E .\Staging\Manual\* .\Staging\WindowsApp\App\
xcopy /Y /E .\Staging\Manual\* .\Staging\LinuxApp\App\ 

::cleanup files in staging
pushd Staging
del /s *.pdb
del /s *Eto.xml
del /s *log4net.xml
del /s *.vshost.exe*
popd

7za.exe a -tzip .\Release\OpenChords.Windows.zip .\Staging\WindowsApp\* 
7za.exe a -tzip .\Release\OpenChords.Linux.zip .\Staging\LinuxApp\* 

"C:\Program Files (x86)\Inno Setup 5\ISCC.exe" OpenChords.Setup\OpenChords.Setup.iss 
move OpenChords.Setup\Output\* .\Release\


