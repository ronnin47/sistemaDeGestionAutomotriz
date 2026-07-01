using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemaDeGestionAutomotriz.Models
{
    

    public class CotizacionPendienteDto
    {
        public int NumeroOrden { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public string TipoServicio { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
        public string Detalle { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public decimal? Precio { get; set; }
    }
}
