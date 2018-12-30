#define MyAppVersion     GetFileVersion("PCLParaphernalia.exe")
#define MyAppName        "PCL Paraphernalia"
#define MyAppURL         "https://github.com/michaelknigge/pclparaphernalia"
#define MyAppUUID        "213E31F3-4806-4A88-9745-C70D5BC5E2C9"
#define MyGroupName      "PCL Paraphernalia"

[Setup]
AppId={#MyAppUUID}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppSupportURL={#MyAppURL}
DefaultDirName={pf}
DefaultGroupName={#MyGroupName}
OutputDir=.
PrivilegesRequired=lowest
UsePreviousLanguage=no
Compression=lzma
DisableWelcomePage=no

[Tasks]
Name: desktopicon; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"

[Icons]
Name: "{commondesktop}\{#MyAppName}";   Filename: "{app}\pclparaphernalia.exe";  WorkingDir: "{app}"; Tasks: desktopicon
Name: "{group}\PCL Paraphernalia";      Filename: "{app}\pclparaphernalia.exe";  WorkingDir: "{app}"
Name: "{group}\Uninstall";              Filename: "{uninstallexe}"

[Files]
Source: "pclparaphernalia.exe"; DestDir: "{app}"; Flags: replacesameversion restartreplace uninsrestartdelete recursesubdirs
Source: "pclparaphernalia.chw"; DestDir: "{app}"; Flags: replacesameversion restartreplace uninsrestartdelete recursesubdirs
Source: "pclparaphernalia.chm"; DestDir: "{app}"; Flags: replacesameversion restartreplace uninsrestartdelete recursesubdirs
