using System.Configuration;
using Npgsql;

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