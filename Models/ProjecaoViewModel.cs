using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MidasBlazor.Models
{
    public class ProjecaoViewModel
    {
        public int IdProjecao { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public decimal ValorPrevisto { get; set; }
        public DateTime DataReferencia { get; set; }
    }
}