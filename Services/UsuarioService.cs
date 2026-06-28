using System;
using Npgsql;
using sistemaDeGestionAutomotriz.Models;
using sistemaDeGestionAutomotriz.Data;

namespace sistemaDeGestionAutomotriz.Services
{
    public class UsuarioService
    {
        public Usuario Login(string email, string pass)
        {
            using (var conexion = DataBase.ObtenerConexion())
            {
                conexion.Open();

                string sql = @"SELECT id_usuario AS UsuarioId, 
                                      nombre     AS Nombre, 
                                      usuario    AS Email, 
                                      password   AS Pass, 
                                      id_rol     AS Rol
                               FROM usuario
                               WHERE usuario = @Email";

                using (var comando = new NpgsqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@Email", email);

                    using (var reader = comando.ExecuteReader())
                    {
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
    }
}