
"Download Neptune"
& "$PSScriptRoot\DatabaseDownload.ps1" -iniFile "./build.ini" -secretsIniFile "./secrets.ini"

"Restore Neptune"
& "$PSScriptRoot\DatabaseRestore.ps1" -iniFile "./build.ini"

"Build Neptune"
& "$PSScriptRoot\DatabaseBuild.ps1" -iniFile "./build.ini"
