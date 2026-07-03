using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemaDeGestionAutomotriz.Models
{
     public class OrdenTrabajo
    {
        public int IdOrden { get; set; }




        //ID_CLIENTE 
        public int IdCliente { get; set; }


        public string NombreCliente { get; set; }

        public string ApellidoCliente { get; set; }

        public string Dni { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

       
        
        
        public string TipoModulo { get; set; }
        public int TipoModuloId { get; set; }// guardamos el id del tipo de Modulo

        public string Modelo { get; set; }
        public string Marca { get; set; }

        public string TipoVehiculo { get; set; }

        public string Combustible { get; set; }




        public int IdUsuarioAsignado { get; set; }

        public string Observaciones { get; set; }

        public DateTime FechaIngreso { get; set; }

        public string Estado { get; set; }

        public bool Garantia { get; set; }
        public string Direccion { get; set; }


    }
}
