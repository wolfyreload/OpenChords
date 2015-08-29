rmdir Staging /Q /S
rmdir Release /Q /S

call build.bat

call Manual\MakeHelpDocument.bat

:: copy all needed files to run the application
xcopy  /Y /E .\OpenChords.CrossPlatform.Wpf\bin\Release\*    .\Staging\WindowsApp\OpenChords\App\
xcopy  /Y /E .\OpenChords.CrossPlatform.Gtk2\bin\Release\*   .\Staging\LinuxApp\OpenChords\App\ 

:: copy the manual
xcopy  /Y /E .\Manual\Help.html .\Staging\Manual\
xcopy  /Y /E .\Manual\HelpImages\* .\Staging\Manual\HelpImages

::Put manual into each folder
xcopy /Y /E .\Staging\Manual\* .\Staging\WindowsApp\OpenChords\App\
xcopy /Y /E .\Staging\Manual\* .\Staging\LinuxApp\OpenChords\App\ 

::copy the settings files
xcopy /Y .\settings.xml .\Staging\WindowsApp\OpenChords\App
xcopy /Y .\settings.xml .\Staging\LinuxApp\OpenChords\App 

::Make zip files for portable version
7za.exe a -tzip .\Release\OpenChords.Windows.zip .\Staging\WindowsApp\* 
7za.exe a -tzip .\Release\OpenChords.Linux.zip .\Staging\LinuxApp\* 

::Make Installer
"C:\Program Files (x86)\Inno Setup 5\ISCC.exe" OpenChords.Setup\OpenChords.Setup.iss 
move OpenChords.Setup\Output\* .\Release\


