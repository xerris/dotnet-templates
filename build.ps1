$solutionFile = ".\Xerris.Templates.sln"

dotnet clean $solutionFile

dotnet build $solutionFile --configuration Release