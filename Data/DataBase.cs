using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace sistemaDeGestionAutomotriz.Services
    {
        public class Conexion
        {
            private static string cadena = ConfigurationManager
                .ConnectionStrings["ElectroLab"].ConnectionString;

            public static NpgsqlConnection ObtenerConexion()
            {
                return new NpgsqlConnection(cadena);
            }
        }
    }

