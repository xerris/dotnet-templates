# Rebuilds template package, packs new templates, uninstalls previous versions, then installs them from the nupkg files
# directly. This is useful for testing modifications to templates.

dotnet clean --configuration release
dotnet clean --configuration debug

.\pack.ps1

$localNupkgFiles = Get-ChildItem -Filter *.nupkg -Recurse

dotnet new uninstall Xerris.DotNet.Templates
dotnet new install $localNupkgFiles[0].FullName
