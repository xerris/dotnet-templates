dotnet tool restore

dotnet gitversion

$solutionFile = ".\Xerris.Templates.sln"
$configuration = "Release";

dotnet clean $solutionFile --configuration $configuration

dotnet restore $solutionFile

dotnet build $solutionFile --configuration $configuration
