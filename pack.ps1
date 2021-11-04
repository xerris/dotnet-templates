dotnet tool restore

dotnet gitversion /updateprojectfiles /output=buildserver

dotnet pack .\src\Xerris.DotNet.Templates.csproj -c Release