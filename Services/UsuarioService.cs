using System;
using System.Windows.Forms;
using Npgsql;
using sistemaDeGestionAutomotriz.Data;
using sistemaDeGestionAutomotriz.Models;
using System.Collections.Generic;







namespace sistemaDeGestionAutomotriz.Services
{
    public class UsuarioService
    {


        //aca estamos 
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
                            usuarios.apellido,

                            usuarios.email,
                            usuarios.password,
                            usuarios.rol  
                            FROM usuarios
                          

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
                        Apellido = reader["apellido"].ToString(),
                        Email = reader["email"].ToString(),
                        Pass = reader["password"].ToString(),
                        Rol = reader["rol"].ToString()
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




        public List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (NpgsqlConnection conexion = new NpgsqlConnection(Database.CadenaConexion))
            {
                try
                {
                    conexion.Open();

                    string sql = @"SELECT
                                id_usuario,
                                nombre,
                                apellido,
                                email,
                                password,
                                rol
                           FROM usuarios;";

                    NpgsqlCommand comando = new NpgsqlCommand(sql, conexion);

                    NpgsqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Usuario usuario = new Usuario
                        {
                            UsuarioId = Convert.ToInt32(reader["id_usuario"]),
                            Nombre = reader["nombre"].ToString(),
                            Apellido = reader["apellido"].ToString(),
                            Email = reader["email"].ToString(),
                            Pass = reader["password"].ToString(),
                            Rol = reader["rol"].ToString()
                        };

                        usuarios.Add(usuario);
                    }

                    return usuarios;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener usuarios: " + ex.Message);
                    return new List<Usuario>();
                }
            }
        }

        public Usuario ObtenerPorId(int id)
        {
            using (NpgsqlConnection conexion = new NpgsqlConnection(Database.CadenaConexion))
            {
                try
                {
                    conexion.Open();

                    string sql = @"SELECT
                            id_usuario,
                            nombre,
                            apellido,
                            email,
                            password,
                            rol
                           FROM usuarios
                           WHERE id_usuario = @id;";

                    NpgsqlCommand comando = new NpgsqlCommand(sql, conexion);
                    comando.Parameters.AddWithValue("@id", id);

                    NpgsqlDataReader reader = comando.ExecuteReader();

                    if (!reader.Read())
                    {
                        return null;
                    }

                    Usuario usuario = new Usuario
                    {
                        UsuarioId = Convert.ToInt32(reader["id_usuario"]),
                        Nombre = reader["nombre"].ToString(),
                        Apellido = reader["apellido"].ToString(),
                        Email = reader["email"].ToString(),
                        Pass = reader["password"].ToString(),
                        Rol = reader["rol"].ToString()
                    };

                    return usuario;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener usuario: " + ex.Message);
                    return null;
                }
            }
        }


    }
}



