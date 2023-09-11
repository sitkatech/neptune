Param(
    [Parameter (Mandatory = $false)]
    [string] $iniFile = ".\build.ini",
    [Parameter (Mandatory = $false)]
    [string] $secretsIniFile = ".\secrets.ini"
)

Import-Module .\Get-Config.psm1

$config = Get-Config -iniFile $iniFile
$secrets = Get-Config -iniFile $secretsIniFile

$blobStorageLocalConnectionString = $secrets.AzureBlobStorageLocalConnectionString
$blobStorageProdConnectionString = $secrets.AzureBlobStorageProdConnectionString

if ("" -ne $blobStorageLocalConnectionString -and "" -ne $blobStorageProdConnectionString)
{
    Write-Output "Begin: Building AzureFileRestore Project"
    dotnet build .\Tools\AzureFileRestore
    Write-Output "Finish: Building AzureFileRestore Project"
    Write-Output "Begin: Blob File Transfer"
    dotnet run --project ".\Tools\AzureFileRestore" -m SyncCopy --Containers file-resource -s $blobStorageProdConnectionString -d $blobStorageLocalConnectionString 
    Write-Output "Finish: Blob File Transfer"
}
else
{
    Write-Output "Null values, no Connection String(s)"
}