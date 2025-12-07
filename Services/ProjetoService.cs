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

    
    // Endpoint correto
    var response = await _http.PostAsync("Projecoes", content);

    var responseContent = await response.Content.ReadAsStringAsync();

    if (!response.IsSuccessStatusCode)
        throw new Exception($"Erro ao inserir: {responseContent}");

    // Deserializa o JSON retornado
    var projecaoCriada = JsonSerializer.Deserialize<ProjetoViewModel>(
        responseContent,
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
    );

    if (projecaoCriada == null)
        throw new Exception("A API retornou um JSON inválido.");

    // Atribui o ID retornado
    projeto.IdProjecao = projecaoCriada.IdProjecao;

    return projeto;
}
    // GET ALL
    public async Task<List<ProjetoViewModel>> GetAllAsync()
    {
        var result = await _http.GetFromJsonAsync<List<ProjetoViewModel>>("Projecoes");


        return result ?? new List<ProjetoViewModel>();
    }
    public async Task UpdateAsync(ProjetoViewModel projeto)
    {
        var content = new StringContent(
            JsonSerializer.Serialize(projeto),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _http.PutAsync($"Projecoes/{projeto.IdProjecao}", content);

        if (!response.IsSuccessStatusCode)
        {
            var erro = await response.Content.ReadAsStringAsync();
            throw new Exception($"Erro ao atualizar: {erro}");
        }
    }
    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"Projecoes/{id}");

        if (!response.IsSuccessStatusCode)
        {
            var erro = await response.Content.ReadAsStringAsync();
            throw new Exception($"Erro ao deletar: {erro}");
        }
    }


    }
}