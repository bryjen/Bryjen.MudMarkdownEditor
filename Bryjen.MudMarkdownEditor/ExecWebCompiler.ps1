param (
    [string]$ProjectDirectory
)

# restore for 'excubo.webcompiler'
Invoke-Expression "dotnet tool restore"

$jsScriptsPath = Join-Path -Path $ProjectDirectory -ChildPath "Scripts"
Get-ChildItem -Path $jsScriptsPath -File | ForEach-Object {
    Get-Content -Path $_.FullName -Raw
}