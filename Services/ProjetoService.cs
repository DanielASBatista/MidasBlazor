using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MidasBlazor.Models;

namespace MidasBlazor.Services
{
    public class ProjetoService
    {
        private readonly HttpClient _http;

        public ProjetoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ProjetoViewModel> InsertAsync(ProjetoViewModel projeto)
{
    var content = new StringContent(
        JsonSerializer.Serialize(projeto),
        Encoding.UTF8,
        "application/json"
    );

    // ROTA CORRETA
    var response = await _http.PostAsync("Projecoes", content);

    var responseContent = await response.Content.ReadAsStringAsync();

    if (!response.IsSuccessStatusCode)
        throw new Exception($"Erro ao inserir: {responseContent}");

    // Aqui retorna o JSON da API
    var projecaoCriada = JsonSerializer.Deserialize<ProjetoViewModel>(responseContent,
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    // Atualiza o ID retornado
    projeto.IdProjecao = projecaoCriada.IdProjecao;

    return projeto;
}

    }
}