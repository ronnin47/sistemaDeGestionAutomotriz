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

        public string Categoria { get; set; } = string.Empty;

        public string TipoServicio { get; set; } = string.Empty;

        public string Cliente { get; set; } = string.Empty;

        public string Dni { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public string Vehiculo { get; set; } = string.Empty;

        public string TipoModulo { get; set; } = string.Empty;

        public string Detalle { get; set; } = string.Empty;

        public string Diagnostico { get; set; } = string.Empty;

        public bool EsReparable { get; set; }

        public string TecnicoAsignado { get; set; } = string.Empty;

        public DateTime FechaIngreso { get; set; }

        public string Estado { get; set; } = string.Empty;

        public decimal Precio { get; set; }

        public bool Garantia { get; set; }

        public string MotivoGarantia { get; set; } = string.Empty;

        public string Observaciones { get; set; } = string.Empty;
    }
}
