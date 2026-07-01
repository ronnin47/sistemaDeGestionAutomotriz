using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemaDeGestionAutomotriz.Models
{
    public class GarantiaDto
    {
        public int IdOrden { get; set; }
        public string Cliente { get; set; }
        public string Detalle { get; set; }
        public string Asignado { get; set; }
        public string Estado { get; set; }
        public string Condicion { get; set; }
    }
}
