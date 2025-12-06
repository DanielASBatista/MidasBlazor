using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MidasBlazor.Models
{
    public class ProjetoViewModel
    {
         [Key]
        public int IdProjecao { get; set; }

        [Required, MaxLength(200)]
        public string Titulo { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorPrevisto { get; set; }
        public DateTime DataReferencia { get; set; } = DateTime.UtcNow;
    }
}