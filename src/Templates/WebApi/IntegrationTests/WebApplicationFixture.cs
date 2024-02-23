namespace Xerris.WebApi1.IntegrationTests;

public class WebApplicationFixture : IAsyncLifetime
{
    public WebApplicationFactory WebApplicationFactory { get; } = new();

    public async Task InitializeAsync()
    {
        await WebApplicationFactory.InitializeAsync();
    }

    public async Task DisposeAsync()
    {
        await WebApplicationFactory.DisposeAsync();
    }
}
