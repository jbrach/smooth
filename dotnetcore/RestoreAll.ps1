

Clear-Host

Write-Host "You have 2 seconds" -ForegroundColor Red -BackgroundColor White

Start-Sleep -s 2


Write-Host $PSScriptRoot

Set-Location  $PSScriptRoot\src\Smooth.Library


dotnet restore

dotnet build

Set-Location  $PSScriptRoot\src\Smooth.Console

dotnet restore

dotnet build

Set-Location  $PSScriptRoot\test

dotnet restore

dotnet build

dotnet test

Write-Host "Smooth - Pew Pew Pew" -ForegroundColor Red -BackgroundColor White

Start-Sleep -s 1


