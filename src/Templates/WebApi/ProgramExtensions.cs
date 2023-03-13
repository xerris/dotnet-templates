namespace Xerris.WebApi1;

public record OpenApiDocumentSettings(string Title);

public static class ProgramExtensions
{
    public static IServiceCollection AddCustomOpenApiDocument(this IServiceCollection services,
        OpenApiDocumentSettings settings)
    {
        services.AddOpenApiDocument(document =>
        {
            document.Title = settings.Title;
            document.FlattenInheritanceHierarchy = true;
            document.AllowReferencesWithProperties = true;
        });

        return services;
    }

    public static IApplicationBuilder AddSwaggerUi(this IApplicationBuilder app,
        OpenApiDocumentSettings settings)
    {
        app.UseOpenApi();
        app.UseSwaggerUi3(config =>
        {
            config.DocumentTitle = settings.Title;
        });

        return app;
    }
}
