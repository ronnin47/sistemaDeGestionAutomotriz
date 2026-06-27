using System;
using MySql.Data.MySqlClient;
using sistemaDeGestionAutomotriz.Models;
using sistemaDeGestionAutomotriz.Data;



namespace sistemaDeGestionAutomotriz.Services
{
    public class UsuarioService
    {
        //private readonly string cadenaConexion = "server=localhost;database=automotrizdb;uid=root;pwd=;";
        //


        public Usuario Login(string email, string pass)
        {
            using (MySqlConnection conexion = new MySqlConnection(Database.CadenaConexion))
            {
                conexion.Open();



                //query=consulta
                string sql = @"SELECT *
                               FROM Usuarios
                               WHERE Email = @Email";

                MySqlCommand comando = new MySqlCommand(sql, conexion);
                comando.Parameters.AddWithValue("@Email", email);

                MySqlDataReader reader = comando.ExecuteReader();

                if (!reader.Read())
                    return null;

                Usuario usuario = new Usuario
                {
                    UsuarioId = Convert.ToInt32(reader["UsuarioId"]),
                    Nombre = reader["Nombre"].ToString(),
                    Email = reader["Email"].ToString(),
                    Pass = reader["Pass"].ToString(),
                    Rol = reader["Rol"].ToString()
                };

                if (usuario.Pass != pass)
                    return null;

                return usuario;
            }
        }
    }
}
