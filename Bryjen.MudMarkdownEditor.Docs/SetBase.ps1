param (
    [string]$BasePath
)

Write-Output "Executing: 'SetBase.ps1'"
Write-Output "BasePath: $BasePath"
Write-Output ""

$indexTemplateFilePath = ".\wwwroot\index-template.html"
$newIndexFileContents = (Get-Content $indexTemplateFilePath) -replace '%BASE_PATH%', $BasePath

$outputFilePath = ".\wwwroot\index.html"
Set-Content -Path $outputFilePath -Value $newIndexFileContents

Write-Output "New '$outputFilePath' file contents:"
Write-Output $newIndexFileContents