namespace Xerris.WebApi1.IntegrationTests;

[CollectionDefinition(CollectionName, DisableParallelization = true)]
public class IdentityWebApplicationCollection : ICollectionFixture<WebApplicationFixture>
{
    public const string CollectionName = "Web Application";

    // This class has no code, and is never created. Its purpose is simply to be the place to apply
    // [CollectionDefinition] and all the ICollectionFixture<> interfaces.
}
