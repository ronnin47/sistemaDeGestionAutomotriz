using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemaDeGestionAutomotriz.Models
{
    public class VentaInsumosDto
    {
        public int NumeroVenta { get; set; } // este es el id
        public string Cliente { get; set; } = string.Empty;
        public int idCliente { get; set; }
        public int idKit { get; set; }
        public string Insumo { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}
