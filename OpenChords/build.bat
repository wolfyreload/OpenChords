::Cleanup old build
rmdir /Q /S OpenChords.CrossPlatform.Wpf\Bin\Release
rmdir /Q /S OpenChords.CrossPlatform.Gtk2\Bin\Release

::build application
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" OpenChords.sln /t:Clean,Build /p:Configuration=Release >build.log

::cleanup pdb and xml files
del /S OpenChords.CrossPlatform.Gtk2\Bin\Release\*.pdb
del /S OpenChords.CrossPlatform.Gtk2\Bin\Release\*.xml
del /S OpenChords.CrossPlatform.Wpf\Bin\Release\*.pdb
del /S OpenChords.CrossPlatform.Wpf\Bin\Release\*.xml
