#define MyAppVersion     GetFileVersion("PCLParaphernalia\bin\Release\PCLParaphernalia.exe")
#define MyAppName        "PCL Paraphernalia"
#define MyAppURL         "https://github.com/michaelknigge/pclparaphernalia"
#define MyAppUUID        "213E31F3-4806-4A88-9745-C70D5BC5E2C9"
#define MyGroupName      "PCL Paraphernalia"

[Setup]
AppId={#MyAppUUID}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppSupportURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyGroupName}
OutputDir=.
UsePreviousLanguage=no
Compression=lzma
DisableWelcomePage=no
OutputBaseFilename=PCLParaphernalia-Setup

[Tasks]
Name: desktopicon; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"

[Icons]
Name: "{commondesktop}\{#MyAppName}";   Filename: "{app}\pclparaphernalia.exe";  WorkingDir: "{app}"; Tasks: desktopicon
Name: "{group}\PCL Paraphernalia";      Filename: "{app}\pclparaphernalia.exe";  WorkingDir: "{app}"
Name: "{group}\Uninstall";              Filename: "{uninstallexe}"

[Files]
Source: "PCLParaphernalia\bin\Release\PCLParaphernalia.application";  DestDir: "{app}"; Flags: replacesameversion restartreplace uninsrestartdelete recursesubdirs
Source: "PCLParaphernalia\bin\Release\PCLParaphernalia.chm";          DestDir: "{app}"; Flags: replacesameversion restartreplace uninsrestartdelete recursesubdirs
Source: "PCLParaphernalia\bin\Release\PCLParaphernalia.exe";          DestDir: "{app}"; Flags: replacesameversion restartreplace uninsrestartdelete recursesubdirs
Source: "PCLParaphernalia\bin\Release\PCLParaphernalia.exe.config";   DestDir: "{app}"; Flags: replacesameversion restartreplace uninsrestartdelete recursesubdirs
Source: "PCLParaphernalia\bin\Release\PCLParaphernalia.exe.manifest"; DestDir: "{app}"; Flags: replacesameversion restartreplace uninsrestartdelete recursesubdirs
