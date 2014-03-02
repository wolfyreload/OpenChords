rmdir /Q /E bin\Release 
c:\Windows\Microsoft.NET\Framework\v3.5\msbuild.exe OpenChords.sln /t:Clean,Build /p:Configuration=Release >build.log