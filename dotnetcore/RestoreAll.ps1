

Clear-Host

Write-Host Get-Location 

Write-Host "You have 2 seconds" -ForegroundColor Red -BackgroundColor White

Start-Sleep -s 2

Write-Host "Setting location" 

Set-Location  C:\source\smooth\dotnetcore\src\Smooth.Library

Write-Host $PSCommandPath

dotnet restore

dotnet build

Set-Location  C:\source\smooth\dotnetcore\src\Smooth.Console

Write-Host $PSCommandPath

dotnet restore

dotnet build

Set-Location  C:\source\smooth\dotnetcore\test

Write-Host $PSCommandPath

dotnet restore

dotnet build

dotnet test

Write-Host "You have 5 seconds" -ForegroundColor Red -BackgroundColor White

Start-Sleep -s 5


