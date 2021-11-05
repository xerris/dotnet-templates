$solutionFile = ".\Xerris.Templates.sln"

dotnet tool restore

dotnet gitversion /updateprojectfiles /output buildserver

$artifactsDirectory = ".\artifacts"

Remove-Item  $artifactsDirectory -Force -Recurse -ErrorAction Ignore

dotnet pack $solutionFile `
    --configuration Release `
    --output ".\artifacts"