using System.Net;
using System.Net.Http.Json;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;

public class CategoriesControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CategoriesControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostCategory_ReturnsCreated()
    {
        var category = new { name = "Ensaladas", icon = "🥗" };
        var response = await _client.PostAsJsonAsync("/api/categories", category);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}

public partial class Program { }