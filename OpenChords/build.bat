::Cleanup old build
rmdir /Q /S OpenChords.CrossPlatform.Executable\Bin\Release
rmdir /Q /S OpenChords.CrossPlatform.Executable\Bin\Release

::build application
nuget restore OpenChords.sln
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\MSBuild.exe" OpenChords.sln /t:Clean,Build /p:Configuration=Release /verbosity:minimal

::cleanup pdb and xml files
del /S OpenChords.CrossPlatform.Executable\Bin\Release\*.pdb
del /S OpenChords.CrossPlatform.Executable\Bin\Release\*.xml
