using System;
using System.Windows.Forms;
using Npgsql;
using sistemaDeGestionAutomotriz.Data;
using sistemaDeGestionAutomotriz.Models;







namespace sistemaDeGestionAutomotriz.Services
{
    public class UsuarioService
    {


        //cambios
        public Usuario Login(string email, string pass)
        {
            using (NpgsqlConnection conexion = new NpgsqlConnection(Database.CadenaConexion))
            {
                try
                {
                    conexion.Open();

                    //MessageBox.Show("Conexión exitosa a la base de datos");

                    string sql = @"SELECT
                            usuarios.id_usuario,
                            usuarios.nombre,
                            usuarios.email,
                            usuarios.password,
                            rol.nombre_rol
                            
                            FROM usuarios
                            INNER JOIN rol
                                ON usuarios.id_rol = rol.id_rol

                            WHERE usuarios.email = @email;";


                    NpgsqlCommand comando = new NpgsqlCommand(sql, conexion);
                    comando.Parameters.AddWithValue("@email", email);

                    NpgsqlDataReader reader = comando.ExecuteReader();

                    if (!reader.Read())
                    {
                        MessageBox.Show("No encontró usuario");
                        return null;
                    }


                    Usuario usuario = new Usuario
                    {
                        UsuarioId = Convert.ToInt32(reader["id_usuario"]),
                        Nombre = reader["nombre"].ToString(),
                        Email = reader["email"].ToString(),
                        Pass = reader["password"].ToString(),
                        Rol = reader["nombre_rol"].ToString()
                    };

                    /*
                    MessageBox.Show(
                        $"ID: {usuario.UsuarioId}\n" +
                        $"Nombre: {usuario.Nombre}\n" +
                        $"Email: {usuario.Email}\n" +
                        $"Rol: {usuario.Rol}"
                    );
                    */

                    if (usuario.Pass != pass)
                    {
                        MessageBox.Show("Contraseña incorrecta");
                        return null;
                    }

                    return usuario;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de conexión: " + ex.Message);
                    return null;
                }
            }
        }
    }
}



