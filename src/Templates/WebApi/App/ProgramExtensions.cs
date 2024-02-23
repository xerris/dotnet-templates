namespace Xerris.WebApi1;

public record OpenApiDocumentSettings(string Title);

public static class ProgramExtensions
{
    public static IServiceCollection AddCustomOpenApiDocument(this IServiceCollection services,
        OpenApiDocumentSettings settings)
    {
        services.AddEndpointsApiExplorer();

        services.AddOpenApiDocument(document =>
        {
            document.Title = settings.Title;
            document.SchemaSettings.FlattenInheritanceHierarchy = true;
            document.SchemaSettings.AllowReferencesWithProperties = true;
        });

        return services;
    }

    public static IApplicationBuilder AddSwaggerUi(this IApplicationBuilder app,
        OpenApiDocumentSettings settings)
    {
        app.UseOpenApi();
        app.UseSwaggerUi(config =>
        {
            config.DocumentTitle = settings.Title;
        });

        return app;
    }
}
