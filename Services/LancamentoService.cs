using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using MidasBlazor.Models;

namespace MidasBlazor.Services
{
    public class LancamentoService
    {
        private readonly HttpClient _http;

        public LancamentoService(HttpClient http)
        {
            _http = http;
        }
    
    public async Task<List<LancamentoViewModel>> GetAllAsync()
        {

            var response = await _http.GetAsync("Lancamentos");
            var responseContent = await response.Content.ReadAsStringAsync();
            List<LancamentoViewModel> lista = new List<LancamentoViewModel>();

            if (response.IsSuccessStatusCode)
            {
                    lista = JsonSerializer.Deserialize<List<LancamentoViewModel>>(responseContent, JsonSerializerOptions.Web);
                return lista;        
            }
            else
            {
                throw new Exception(responseContent);
            }
        }
    }
}