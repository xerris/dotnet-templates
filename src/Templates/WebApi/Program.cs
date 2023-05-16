using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using Xerris.WebApi1;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddRouting(opts =>
{
    opts.LowercaseUrls = true;
    opts.LowercaseQueryStrings = true;
});

builder.Services.AddHealthChecks();

builder.Services
    .AddFluentValidationAutoValidation(fv => fv.DisableDataAnnotationsValidation = true)
    .AddValidatorsFromAssemblyContaining<Program>();

var openApiDocumentSettings = new OpenApiDocumentSettings("Todo API");

builder.Services.AddCustomOpenApiDocument(openApiDocumentSettings);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see
    // https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();

app.UseHttpLogging();

app.MapHealthChecks("/");

app.AddSwaggerUi(openApiDocumentSettings);

await app.RunAsync();
