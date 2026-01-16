using System.Text.Json;
using TodoList.Application.Interfaces;
using TodoList.Communication.ExternalApis;

namespace TodoList.Infrastructure.Http
{
    public class ExternalTodoService : IExternalTodoService
    {
        private readonly HttpClient _httpClient;
        private const string ExternalApiUrl = "https://jsonplaceholder.typicode.com/todos";

        public ExternalTodoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TodoExternalJson>> FetchTodosFromExternalApi()
        {
            try
            {
                var response = await _httpClient.GetAsync(ExternalApiUrl);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var todos = JsonSerializer.Deserialize<List<TodoExternalJson>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return todos ?? new List<TodoExternalJson>();
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException("Erro ao conectar com API externa", ex);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Erro ao desserializar JSON da API externa", ex);
            }
        }
    }
}
