::Cleanup old build
rmdir /Q /S OpenChords.CrossPlatform.Executable\Bin\Release
rmdir /Q /S OpenChords.CrossPlatform.Executable\Bin\Release

::build application
powershell -File build.ps1

::cleanup pdb and xml files
del /S OpenChords.CrossPlatform.Executable\Bin\Release\*.pdb
del /S OpenChords.CrossPlatform.Executable\Bin\Release\*.xml
