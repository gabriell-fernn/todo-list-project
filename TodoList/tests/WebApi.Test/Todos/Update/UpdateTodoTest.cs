using CommonTestUtilities.Requests;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using TodoList.Communication.Requests;

namespace WebApi.Test.Todos.Update
{
    public class UpdateTodoTest : IClassFixture<CustomWebApplicationFactory>
    {
        private const string METHOD = "api/todos";
        private readonly HttpClient _httpClient;

        public UpdateTodoTest(CustomWebApplicationFactory webApplicationFactory)
        {
            _httpClient = webApplicationFactory.CreateClient();
        }

        [Fact]
        public async Task PUT_Marca_Concluido()
        {
            var request = RequestRegisterTodoJsonBuilder.Build();

            var created = await _httpClient.PostAsJsonAsync(METHOD, request);
            created.StatusCode.Should().Be(HttpStatusCode.Created);

            var body = await created.Content.ReadAsStreamAsync();
            var json = await JsonDocument.ParseAsync(body);

            var id = json.RootElement.GetProperty("id").GetInt32();

            var result = await _httpClient.PutAsJsonAsync($"{METHOD}/{id}", new RequestUpdateTodoJson { Completed = true });

            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Error_PUT_TarefaNaoExiste()
        {
            var request = RequestRegisterTodoJsonBuilder.Build();

            var result = await _httpClient.PutAsJsonAsync($"{METHOD}/999999", request);

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
