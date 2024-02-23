using System.Net.Http.Json;
using Xerris.WebApi1.Models;

namespace Xerris.WebApi1.IntegrationTests.Todos;

[Collection(IdentityWebApplicationCollection.CollectionName)]
public class TodoIntegrationTests(WebApplicationFixture fixture)
{
    [Fact]
    public async Task Get_todo_items()
    {
        var client = fixture.WebApplicationFactory.CreateClient();

        var todos = await client.GetFromJsonAsync<TodoItem[]>("todos");

        todos.Should().NotBeEmpty();
    }
}
