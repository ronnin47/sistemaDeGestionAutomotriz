using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemaDeGestionAutomotriz.Models
{
    public class VentaInsumosDto
    {
        public int NumeroVenta { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public string Insumo { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
    }
}
