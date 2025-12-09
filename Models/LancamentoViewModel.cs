using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MidasBlazor.Models
{
    public class LancamentoViewModel
    {
    public int IdLancamento { get; set; }

    public string Descricao { get; set; } = string.Empty;

    public decimal Valor { get; set; }

    public DateTime? Data { get; set; }
    }
}