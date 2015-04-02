rmdir /Q /S OpenChordsLoader\Bin\Release
"C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe" OpenChords.sln /t:Clean,Build /p:Configuration=Release >build.log