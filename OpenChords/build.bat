::Cleanup old build
rmdir /Q /S OpenChords.CrossPlatform.Executable\Bin\Release

nuget restore
"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" OpenChords.sln /property:Configuration=Release