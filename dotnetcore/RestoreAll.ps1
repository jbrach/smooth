

Clear-Host

Write-Host "You have 2 seconds" -ForegroundColor Red -BackgroundColor White

Start-Sleep -s 2


Write-Host $PSScriptRoot

Set-Location  $PSScriptRoot\src\Smooth.Library

dotnet clean

dotnet restore

dotnet msbuild

Set-Location  $PSScriptRoot\src\Smooth.Console

dotnet clean

dotnet restore

dotnet msbuild

Set-Location  $PSScriptRoot\test

dotnet clean

dotnet restore

dotnet msbuild

#dotnet test

Write-Host "Smooth - Pew Pew Pew" -ForegroundColor Red -BackgroundColor White

Start-Sleep -s 1


