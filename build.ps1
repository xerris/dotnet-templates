dotnet tool restore

dotnet gitversion

# We build the projects individually here because only the parent project is in the solution file.
$projects = Get-ChildItem -Filter "*.csproj" -Recurse;

$configuration = "Release";

foreach ($project in $projects) {
    Write-Output "Cleaning $project..."
    dotnet clean $project --configuration $configuration

    Write-Output "Restoring $project..."
    dotnet restore $project

    Write-Output "Building $project..."
    dotnet build $project --configuration $configuration --no-restore
}
