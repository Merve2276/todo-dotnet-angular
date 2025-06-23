using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using todo_dotnet_api.DTOs;
using Xunit;

namespace todo_dotnet_api.Tests
{
    public class TodoControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public TodoControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task PostTodo_InvalidTitle_ReturnsBadRequest()
        {
            // Arrange
            var invalidTodo = new TodoDto { Title = "" };

            // Act
            var response = await _client.PostAsJsonAsync("/api/todo", invalidTodo);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var errors = await response.Content.ReadFromJsonAsync<ValidationErrorResponse>();
            errors!.Errors.Should().ContainKey("Title");
            errors.Errors["Title"].Should().Contain("Başlık boş olamaz.");
        }
    }

    public class ValidationErrorResponse
    {
        public Dictionary<string, string[]> Errors { get; set; } = new();
    }
}
