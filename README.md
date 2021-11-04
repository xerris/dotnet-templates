# Xerris.DotNet.Templates

Project templates for use with `dotnet new`.

## Installation and Use

Templates can be installed as a nuget package with the command:

```powershell
dotnet new --install Xerris.DotNet.Templates
```

Read more about `dotnet new` and project templates
[here](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new).

Once installed, the templates can be used just like any other `dotnet new`
template. For example, to create a new service project, run this command:

```powershell
dotnet new xerris-service --name Company.Project.ServiceName
```

## Adding New Templates

Any new templates added under the `src/Templates` folder will be included in the
`Xerris.DotNet.Templates` package automatically. For more information on
creating custom templates, see the [official docs](https://docs.microsoft.com/en-us/dotnet/core/tools/custom-templates).
