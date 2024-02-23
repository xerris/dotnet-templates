using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace Xerris.WebApi1.IntegrationTests;

public class WebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public Task InitializeAsync()
    {
        // TODO: Set up any dependencies here
        return Task.CompletedTask;
    }

    public new Task DisposeAsync()
    {
        // TODO: Tear down any dependencies here
        return Task.CompletedTask;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(_ =>
        {
            // TODO: Configure test services here
        });
    }
}
