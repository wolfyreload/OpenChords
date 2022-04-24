::Cleanup old build
rmdir /Q /S OpenChords.CrossPlatform.Executable\Bin\Release
rmdir /Q /S OpenChords.CrossPlatform.Executable\Bin\Release

::build application
dotnet tool restore
dotnet cake

::cleanup pdb and xml files
del /S OpenChords.CrossPlatform.Executable\Bin\Release\*.pdb
del /S OpenChords.CrossPlatform.Executable\Bin\Release\*.xml
