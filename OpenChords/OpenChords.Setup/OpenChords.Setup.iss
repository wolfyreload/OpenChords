#define MainBinaryLocation  "..\OpenChordsLoader\bin\Release\OpenChords.exe"
#define AppVersion      GetFileVersion(MainBinaryLocation)


[Setup]
AppName=OpenChords
AppVersion={#AppVersion} Beta
DefaultDirName={pf}\OpenChords
DefaultGroupName=OpenChords
UninstallDisplayIcon={app}\OpenChords.exe
Compression=lzma2
SolidCompression=yes
OutputBaseFilename=OpenChords.Installer.{#AppVersion}

[Files]
Source: "..\OpenChordsLoader\bin\Release\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs 
Source: "..\OpenChordsLoader\bin\Release\changelog.txt"; DestDir: "{app}"; Flags: isreadme ignoreversion

[Icons]
Name: "{group}\OpenChords"; Filename: "{app}\OpenChords.exe"

[Run]
Filename: {app}\{cm:AppName}.exe; Description: {cm:LaunchProgram,{cm:AppName}}; Flags: nowait postinstall skipifsilent

[CustomMessages]
AppName=OpenChords
LaunchProgram=Start OpenChords after finishing installation