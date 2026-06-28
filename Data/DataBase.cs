using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Npgsql;


namespace sistemaDeGestionAutomotriz.Data
    {
        public class DataBase
        {
            private static string cadena = ConfigurationManager
                .ConnectionStrings["ElectroLab"].ConnectionString;

            public static NpgsqlConnection ObtenerConexion()
            {
                return new NpgsqlConnection(cadena);
            }
        }
    }

