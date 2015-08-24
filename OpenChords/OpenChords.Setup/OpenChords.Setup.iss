#define MainBinaryLocation  "..\OpenChords.CrossPlatform.Wpf\bin\Release\OpenChords.Windows.exe"
#define AppVersion      GetFileVersion(MainBinaryLocation)

[Setup]
AppName=OpenChords
UninstallDisplayName=OpenChords
AppVersion={#AppVersion} Beta
DefaultDirName={pf}\OpenChords
DefaultGroupName=OpenChords
UninstallDisplayIcon={app}\OpenChords.Windows.exe
Compression=lzma2
SolidCompression=yes
OutputBaseFilename=OpenChords.Installer.{#AppVersion}

[Files]
Source: "..\OpenChords.CrossPlatform.Wpf\bin\Release\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs 
Source: "..\OpenChords.CrossPlatform.Wpf\bin\Release\changelog.txt"; DestDir: "{app}"; Flags: isreadme ignoreversion

[Icons]
Name: "{group}\OpenChords"; Filename: "{app}\OpenChords.Windows.exe"

[CustomMessages]
LaunchProgram=Launch OpenChords

[Run]
Filename: {app}\OpenChords.Windows.exe; Description: {cm:LaunchProgram}; Flags: nowait postinstall skipifsilent