using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using MidasBlazor.Models;

namespace MidasBlazor.Services
{
    public class ProjecoesService
    {
        private readonly HttpClient _http;

        public ProjecoesService(HttpClient http)
        {
            _http = http;
        }
    
    public async Task<List<ProjecaoViewModel>> GetAllAsync()
        {

            var response = await _http.GetAsync("Projecoes");
            var responseContent = await response.Content.ReadAsStringAsync();
            List<ProjecaoViewModel> lista = new List<ProjecaoViewModel>();

            if (response.IsSuccessStatusCode)
            {
                    lista = JsonSerializer.Deserialize<List<ProjecaoViewModel>>(responseContent, JsonSerializerOptions.Web);
                return lista;        
            }
            else
            {
                throw new Exception(responseContent);
            }
        }
    }
}