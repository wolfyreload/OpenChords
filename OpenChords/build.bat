::Cleanup old build
rmdir /Q /S OpenChords.CrossPlatform.Executable\Bin\Release
rmdir /Q /S OpenChords.CrossPlatform.Executable\Bin\Release

::build application
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" OpenChords.sln /t:Clean,Build /p:Configuration=Release

::cleanup pdb and xml files
del /S OpenChords.CrossPlatform.Executable\Bin\Release\*.pdb
del /S OpenChords.CrossPlatform.Executable\Bin\Release\*.xml
