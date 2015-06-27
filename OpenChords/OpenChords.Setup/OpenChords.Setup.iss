#define MainBinaryLocation  "..\Staging\WpfApp\OpenChords.CrossPlatform.Wpf.exe"
#define AppVersion      GetFileVersion(MainBinaryLocation)

[Setup]
AppName=OpenChords
AppVersion={#AppVersion}
DefaultDirName={pf}\OpenChords
DefaultGroupName=OpenChords
UninstallDisplayIcon={app}\OpenChords.CrossPlatform.Wpf.exe
Compression=lzma2
SolidCompression=yes
OutputBaseFilename=OpenChords.Installer.{#AppVersion}

[Files]
Source: "..\Staging\WpfApp\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs 
Source: "..\Staging\WpfApp\changelog.txt"; DestDir: "{app}"; Flags: isreadme ignoreversion

[Icons]
Name: "{group}\OpenChords"; Filename: "{app}\OpenChords.CrossPlatform.Wpf.exe"

;[Run]
;Filename: {app}\{cm:AppName}.exe; Description: {cm:LaunchProgram,{cm:AppName}}; Flags: nowait postinstall skipifsilent

;[CustomMessages]
;AppName=OpenChords
;LaunchProgram=Start OpenChords after finishing installation