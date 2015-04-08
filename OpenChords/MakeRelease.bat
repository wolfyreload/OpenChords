rmdir Staging /Q /S
rmdir Release /Q /S

mkdir Release

call build.bat

xcopy  /Y /E .\OpenChordsLoader\bin\Release\* .\Staging\LegacyApp\ 
xcopy  /Y /E .\OpenChords.CrossPlatform.WinForms\bin\Release\* .\Staging\WinformsApp\ 
xcopy  /Y /E .\OpenChords.CrossPlatform.Wpf\bin\Release\* .\Staging\WpfApp\
xcopy  /Y /E .\OpenChords.CrossPlatform.Gtk2\bin\Release\* .\Staging\Gtk2App\ 

xcopy  /Y .\OpenChords\manual\manual.pdf .\Staging\LegacyApp\
copy /Y .\portable.xml .\Staging\LegacyApp\settings.xml

echo cd app >> .\staging\StartOpenChords.bat
echo start OpenChords.exe >> .\staging\StartOpenChords.bat

7za.exe a -tzip .\Release\OpenChords.zip .\Staging\* 
7za.exe a .\Release\OpenChords.7z .\Staging\* 

"C:\Program Files (x86)\Inno Setup 5\ISCC.exe" /cc OpenChords.Setup\OpenChords.Setup.iss 
move OpenChords.Setup\Output\* .\Release\


