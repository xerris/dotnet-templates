dotnet tool restore

dotnet gitversion /updateprojectfiles /output buildserver

# Only the parent project gets packaged, so only the parent project should get versioned.
# See https://github.com/xerris/Xerris.DotNet.Templates/issues/5
git checkout .\src\Templates\**\*.csproj -v

$solutionFile = ".\Xerris.Templates.sln"
$artifactsDirectory = "artifacts"

Remove-Item  $artifactsDirectory -Force -Recurse -ErrorAction Ignore -Verbose

dotnet pack $solutionFile `
    --configuration Release `
    --output "artifacts"