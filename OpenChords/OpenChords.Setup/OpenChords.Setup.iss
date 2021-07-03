#define MainBinaryLocation  "..\OpenChords.CrossPlatform.Executable\bin\Release\OpenChords.exe"
#define AppVersion      GetFileVersion(MainBinaryLocation)

[Setup]
AppName=OpenChords
UninstallDisplayName=OpenChords
AppVersion={#AppVersion}
DefaultDirName={commonpf}\OpenChords
DefaultGroupName=OpenChords
UninstallDisplayIcon={app}\OpenChords.exe
Compression=lzma2
SolidCompression=yes
OutputBaseFilename=OpenChords.Installer.{#AppVersion}

[Files]
Source: "..\OpenChords.CrossPlatform.Executable\bin\Release\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs 
Source: "..\OpenChords.CrossPlatform.Executable\bin\Release\changelog.txt"; DestDir: "{app}"; Flags: isreadme ignoreversion

[Icons]
Name: "{group}\OpenChords"; Filename: "{app}\OpenChords.exe"

[CustomMessages]
LaunchProgram=Launch OpenChords

[Run]
Filename: {app}\OpenChords.exe; Description: {cm:LaunchProgram}; Flags: nowait postinstall skipifsilent