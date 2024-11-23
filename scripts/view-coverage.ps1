[CmdletBinding()]
param(
    [switch]$SkipTarget
)

if ($SkipTarget | Out-Null) {
    Write-Host 'sldkfsd'
}

$rootDirectoryPath = Join-Path -Path $PSScriptRoot -ChildPath '..'
$devScriptPath = Join-Path -Path $rootDirectoryPath -ChildPath 'dev.ps1'
$coveragePath = Join-Path -Path $rootDirectoryPath -ChildPath 'artifacts' -AdditionalChildPath 'tests','coverage','index.html'

$targetSucceeded = $true
if (-not $SkipTarget) {
    & $devScriptPath 'coverage'
    $targetSucceeded = $LASTEXITCODE -eq 0
}

if ($targetSucceeded) {
    Invoke-Item -Path $coveragePath
}
