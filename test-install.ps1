dotnet clean --configuration release
dotnet clean --configuration debug

.\pack.ps1

$localNupkgFiles = Get-ChildItem -Filter *.nupkg -Recurse

dotnet new uninstall Xerris.DotNet.Templates
dotnet new install $localNupkgFiles[0].FullName
