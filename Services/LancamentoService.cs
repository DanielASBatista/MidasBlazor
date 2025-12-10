using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
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

        // CREATE
        public async Task<LancamentoViewModel> InsertAsync(LancamentoViewModel lancamento)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(lancamento),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _http.PostAsync("Lancamentos/new", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Erro ao inserir: {responseContent}");

            var criado = JsonSerializer.Deserialize<LancamentoViewModel>(
                responseContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            if (criado == null)
                throw new Exception("A API retornou um JSON inválido.");

            lancamento.IdLancamento = criado.IdLancamento;
            return lancamento;
        }

        // READ - GET ALL
        public async Task<List<LancamentoViewModel>> GetAllAsync()
        {
            var result = await _http.GetFromJsonAsync<List<LancamentoViewModel>>("Lancamentos/GetAll");
            return result ?? new List<LancamentoViewModel>();
        }

        // READ - GET BY ID
        public async Task<LancamentoViewModel?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<LancamentoViewModel>($"Lancamentos/{id}");
        }

        // UPDATE
        public async Task UpdateAsync(LancamentoViewModel lancamento)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(lancamento),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _http.PutAsync($"Lancamentos/{lancamento.IdLancamento}", content);

            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao atualizar: {erro}");
            }
        }

        // DELETE
        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"Lancamentos/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao deletar: {erro}");
            }
        }
        // FILTROS

        // Por data (Data)
        public async Task<List<LancamentoViewModel>> GetByDataAsync(DateTime data)
        {
            var url = $"Lancamentos/DataReferencia/{data:yyyy-MM-dd}";
            return await _http.GetFromJsonAsync<List<LancamentoViewModel>>(url) ?? new();
        }

        // Por Data de Criação
        public async Task<List<LancamentoViewModel>> GetByDataCriacaoAsync(DateTime data)
        {
            var url = $"Lancamentos/DataCriacao/{data:yyyy-MM-dd}";
            return await _http.GetFromJsonAsync<List<LancamentoViewModel>>(url) ?? new();
        }

        // Por valor
        public async Task<List<LancamentoViewModel>> GetByValorAsync(decimal valor)
        {
            return await _http.GetFromJsonAsync<List<LancamentoViewModel>>($"Lancamentos/valor/{valor}") ?? new();
        }

        // Por ano
        public async Task<List<LancamentoViewModel>> GetByAnoAsync(int ano)
        {
            return await _http.GetFromJsonAsync<List<LancamentoViewModel>>($"Lancamentos/ano/{ano}") ?? new();
        }

        // Por ano e mês
        public async Task<List<LancamentoViewModel>> GetByMesAsync(int ano, int mes)
        {
            return await _http.GetFromJsonAsync<List<LancamentoViewModel>>($"Lancamentos/mes/{ano}/{mes}") ?? new();
        }

        // Por ano/mês/dia
        public async Task<List<LancamentoViewModel>> GetByDiaAsync(int ano, int mes, int dia)
        {
            return await _http.GetFromJsonAsync<List<LancamentoViewModel>>($"Lancamentos/dia/{ano}/{mes}/{dia}") ?? new();
        }

        // Somatória de valores
        public async Task<decimal> GetSomatoriaAsync()
        {
            return await _http.GetFromJsonAsync<decimal>("Lancamentos/somatoria");
        }

        // Comparação (maior que X)
        public async Task<List<LancamentoViewModel>> GetComparacaoAsync(decimal valor)
        {
            return await _http.GetFromJsonAsync<List<LancamentoViewModel>>($"Lancamentos/comparacao/{valor}") ?? new();
        }
    }
}
