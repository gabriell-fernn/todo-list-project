using CommonTestUtilities.Requests;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace WebApi.Test.Todos.Register
{
    public class RegisterTodoTest : IClassFixture<CustomWebApplicationFactory>
    {
        private const string METHOD = "api/todos";
        private readonly HttpClient _httpClient;

        public RegisterTodoTest(CustomWebApplicationFactory webApplicationFactory)
        {
            _httpClient = webApplicationFactory.CreateClient();
        }

        [Fact]
        public async Task Success_POST()
        {
            var request = RequestRegisterTodoJsonBuilder.Build();

            var result = await _httpClient.PostAsJsonAsync(METHOD, request);

            result.StatusCode.Should().Be(HttpStatusCode.Created);

            var body = await result.Content.ReadAsStreamAsync();
            var response = await JsonDocument.ParseAsync(body);

            response.RootElement.GetProperty("title").GetString().Should().Be(request.Title);
            response.RootElement.GetProperty("userId").GetInt32().Should().Be(request.UserId);
        }

        [Fact]
        public async Task Error_EmptyTitle()
        {
            var request = RequestRegisterTodoJsonBuilder.Build();
            request.Title = "";

            var result = await _httpClient.PostAsJsonAsync(METHOD, request);

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();
            var response = await JsonDocument.ParseAsync(body);

            var errorMessages = response.RootElement.GetProperty("errorMessages").EnumerateArray();
            errorMessages.Should().Contain(error =>
                error.GetString()!.Contains("Título é obrigatório"));
        }

        [Fact]
        public async Task Error_InvalidUserId()
        {
            var request = RequestRegisterTodoJsonBuilder.Build();
            request.UserId = 0;

            var result = await _httpClient.PostAsJsonAsync(METHOD, request);

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();
            var response = await JsonDocument.ParseAsync(body);

            var errorMessages = response.RootElement.GetProperty("errorMessages").EnumerateArray();
            errorMessages.Should().Contain(error =>
                error.GetString()!.Contains("UserId deve ser maior que 0"));
        }
    }
}
