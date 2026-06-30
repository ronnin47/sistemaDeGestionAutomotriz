using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemaDeGestionAutomotriz.Models
{
    public class OrdenTrabajoDto
    {

        public int NumeroOrden { get; set; }
        public string Tipo { get; set; }
        public string Cliente { get; set; }
        public string Detalle { get; set; }
        public string Asignado { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Estado { get; set; }

        public string Telefono { get; set; }
        public double Cotizacion { get; set; }
    }
}
