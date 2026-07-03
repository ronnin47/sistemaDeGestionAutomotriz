using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemaDeGestionAutomotriz.Models
{
    public class OrdenTrabajoCerrajeria
    {
        public int IdOrden { get; set; }







        //ID_CLIENTE 
        public int IdCliente { get; set; }


        public string NombreCliente { get; set; }

        public string ApellidoCliente { get; set; }

        public string Dni { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }




        public string TipoServicio { get; set; }

      

        public string Marca { get; set; }

     




        public int IdUsuarioAsignado { get; set; }

        public string Observaciones { get; set; }

        public DateTime FechaIngreso { get; set; }

        public string Estado { get; set; }

        public bool Garantia { get; set; }
        public string Direccion { get; set; }


    }
}
