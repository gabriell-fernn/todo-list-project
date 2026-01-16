using CommonTestUtilities.Requests;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace WebApi.Test.Todos.GetById
{
    public class GetByIdTodoTest : IClassFixture<CustomWebApplicationFactory>
    {
        private const string METHOD = "api/todos";
        private readonly HttpClient _httpClient;

        public GetByIdTodoTest(CustomWebApplicationFactory webApplicationFactory)
        {
            _httpClient = webApplicationFactory.CreateClient();
        }

        [Fact]
        public async Task Sucesso_GET_By_Id()
        {
            var request = RequestRegisterTodoJsonBuilder.Build();

            var created = await _httpClient.PostAsJsonAsync(METHOD, request);
            created.StatusCode.Should().Be(HttpStatusCode.Created);

            var body = await created.Content.ReadAsStreamAsync();
            var json = await JsonDocument.ParseAsync(body);

            var id = json.RootElement.GetProperty("id").GetInt32();

            var result = await _httpClient.GetAsync($"{METHOD}/{id}");

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Error_GET_TarefaNaoExiste()
        {
            var request = RequestRegisterTodoJsonBuilder.Build();

            var result = await _httpClient.PutAsJsonAsync($"{METHOD}/999999", request);

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
