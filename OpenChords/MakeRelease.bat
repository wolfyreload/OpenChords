rmdir Staging /Q /S
rmdir Release /Q /S

call build.bat

call Manual\MakeHelpDocument.bat

:: copy all needed files to run the application
xcopy  /Y /E .\OpenChords.CrossPlatform.Wpf\bin\Release\*    .\Staging\WindowsApp\OpenChords\App\
xcopy  /Y /E .\OpenChords.CrossPlatform.Gtk2\bin\Release\*   .\Staging\LinuxApp\OpenChords\App\ 

:: copy the manual
xcopy  /Y .\Manual\Help.html .\Staging\Manual\

::Put manual into each folder
xcopy /Y /E .\Staging\Manual\* .\Staging\WindowsApp\OpenChords\App\
xcopy /Y /E .\Staging\Manual\* .\Staging\LinuxApp\OpenChords\App\ 

::copy the settings files
xcopy /Y .\settings.xml .\Staging\WindowsApp\OpenChords\App
xcopy /Y .\settings.xml .\Staging\LinuxApp\OpenChords\App 

::Make 7zip files for portable version
7za.exe a -tzip .\Release\OpenChords.Windows.Portable.zip .\Staging\WindowsApp\* -mx9
7za.exe a -tzip .\Release\OpenChords.Linux.Portable.zip .\Staging\LinuxApp\* -mx9
7za.exe a -t7z .\Release\OpenChords.Windows.Portable.7z .\Staging\WindowsApp\* -mx9 -ms=on -m0=lzma
7za.exe a -t7z .\Release\OpenChords.Linux.Portable.7z .\Staging\LinuxApp\* -mx9 -ms=on -m0=lzma

::Make Installer
"C:\Program Files (x86)\Inno Setup 5\ISCC.exe" OpenChords.Setup\OpenChords.Setup.iss 
move OpenChords.Setup\Output\* .\Release\


