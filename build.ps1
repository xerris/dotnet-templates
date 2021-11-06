dotnet tool restore

dotnet gitversion

# We build the projects individually here because only the parent project is in the solution file.
$projects = Get-ChildItem -Filter "*.csproj" -Recurse;

foreach ($project in $projects) {
    dotnet clean $project
    dotnet restore $project
    dotnet build $project --no-restore
}
