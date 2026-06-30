using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using sistemaDeGestionAutomotriz.Data;
using sistemaDeGestionAutomotriz.Models;

namespace sistemaDeGestionAutomotriz.Services
{
    
        public class ClienteService
        {

        


        //obtener clientes activos
        public List<Cliente> ObtenerClientesActivos()
            {
                List<Cliente> listaClientes = new List<Cliente>();

                using (NpgsqlConnection conexion = new NpgsqlConnection(Database.CadenaConexion))
                {
                    try
                    {
                        conexion.Open();

                        string sql = @"SELECT
                            id_cliente,
                            nombre,
                            apellido,
                            dni,               
                            telefono,
                            email,
                            direccion,
                            activo
                           
                           FROM clientes
                           WHERE activo = TRUE;";

                        NpgsqlCommand comando = new NpgsqlCommand(sql, conexion);

                        NpgsqlDataReader reader = comando.ExecuteReader();

                        while (reader.Read())
                        {
                            Cliente cliente = new Cliente
                            {
                                ClienteId = Convert.ToInt32(reader["id_cliente"]),
                                Nombre = reader["nombre"].ToString(),   
                                Apellido = reader["apellido"].ToString(),
                                Dni = reader["dni"].ToString(),
                                Telefono = reader["telefono"].ToString(),
                                Email = reader["email"].ToString(),
                                Direccion= reader["direccion"].ToString(),
                                Activo= Convert.ToBoolean(reader["activo"])
                            };

                            listaClientes.Add(cliente);
                        }

                        /*
                        //MOSTRAR
                        StringBuilder mensaje = new StringBuilder();

                        foreach (Cliente cliente in listaClientes)
                        {
                            mensaje.AppendLine(
                                $"ID: {cliente.ClienteId} | " +
                                $"Nombre: {cliente.Nombre} {cliente.Apellido} | " +
                                $"DNI: {cliente.Dni} | " +
                                $"Teléfono: {cliente.Telefono} | " +
                                $"Email: {cliente.Email} | " +
                                $"Dirección: {cliente.Direccion}"
                            );
                        }

                        MessageBox.Show(mensaje.ToString(), "Lista de Clientes");
                        */
                        return listaClientes;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error de conexión: " + ex.Message);
                        return new List<Cliente>();
                    }
                }


          


            























            /*
            public Cliente ObtenerClientePorId(int id)
            {

            }

            

            public void ModificarCliente(Cliente cliente)
            {

            }

            public void EliminarCliente(int id)
            {

            }
            */
        }

        //obtener clientes inactivos
        public List<Cliente> ObtenerClientesInactivos()
        {
            List<Cliente> listaClientes = new List<Cliente>();

            using (NpgsqlConnection conexion = new NpgsqlConnection(Database.CadenaConexion))
            {
                try
                {
                    conexion.Open();

                    string sql = @"SELECT
                            id_cliente,
                            nombre,
                            apellido,
                            dni,
                            telefono,
                            email,
                            direccion,
                            activo
                           FROM clientes
                           WHERE activo = FALSE;";

                    NpgsqlCommand comando = new NpgsqlCommand(sql, conexion);

                    NpgsqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Cliente cliente = new Cliente
                        {
                            ClienteId = Convert.ToInt32(reader["id_cliente"]),
                            Nombre = reader["nombre"].ToString(),
                            Apellido = reader["apellido"].ToString(),
                            Dni = reader["dni"].ToString(),
                            Telefono = reader["telefono"].ToString(),
                            Email = reader["email"].ToString(),
                            Direccion = reader["direccion"].ToString(),
                            Activo = Convert.ToBoolean(reader["activo"])
                        };

                        listaClientes.Add(cliente);
                    }

                    return listaClientes;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de conexión: " + ex.Message);
                    return new List<Cliente>();
                }
            }
        }

        //obtener todos los clientes activos y inactivos
        public List<Cliente> ObtenerTodosLosClientes()
        {
            List<Cliente> listaClientes = new List<Cliente>();

            using (NpgsqlConnection conexion = new NpgsqlConnection(Database.CadenaConexion))
            {
                try
                {
                    conexion.Open();

                    string sql = @"SELECT
                            id_cliente,
                            nombre,
                            apellido,
                            dni,
                            telefono,
                            email,
                            direccion,
                            activo
                           FROM clientes;";

                    NpgsqlCommand comando = new NpgsqlCommand(sql, conexion);

                    NpgsqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Cliente cliente = new Cliente
                        {
                            ClienteId = Convert.ToInt32(reader["id_cliente"]),
                            Nombre = reader["nombre"].ToString(),
                            Apellido = reader["apellido"].ToString(),
                            Dni = reader["dni"].ToString(),
                            Telefono = reader["telefono"].ToString(),
                            Email = reader["email"].ToString(),
                            Direccion = reader["direccion"].ToString(),
                            Activo = Convert.ToBoolean(reader["activo"])
                        };

                        listaClientes.Add(cliente);
                    }

                    return listaClientes;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de conexión: " + ex.Message);
                    return new List<Cliente>();
                }
            }
        }







        public void AgregarCliente(Cliente cliente)
        {
            using (NpgsqlConnection conexion = new NpgsqlConnection(Database.CadenaConexion))
            {
                try
                {
                    conexion.Open();

                    string sql = @"INSERT INTO clientes
                           (
                               nombre,
                               apellido,
                               dni,
                               telefono,
                               email,
                               direccion,
                               activo
                           )
                           VALUES
                           (
                               @nombre,
                               @apellido,
                               @dni,
                               @telefono,
                               @email,
                               @direccion,
                               @activo
                           );";

                    NpgsqlCommand comando = new NpgsqlCommand(sql, conexion);

                    comando.Parameters.AddWithValue("@nombre", cliente.Nombre);
                    comando.Parameters.AddWithValue("@apellido", cliente.Apellido);
                    comando.Parameters.AddWithValue("@dni", cliente.Dni);
                    comando.Parameters.AddWithValue("@telefono", cliente.Telefono);
                    comando.Parameters.AddWithValue("@email", cliente.Email);
                    comando.Parameters.AddWithValue("@direccion", cliente.Direccion);
                    comando.Parameters.AddWithValue("@activo", cliente.Activo);

                    comando.ExecuteNonQuery();

                    MessageBox.Show("Cliente agregado correctamente.");



                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar el cliente: " + ex.Message);
                }
            }
        }

        public void ActualizarCliente(Cliente cliente)
        {

            using (NpgsqlConnection conexion = new NpgsqlConnection(Database.CadenaConexion))
            {
                try
                {
                    conexion.Open();

                    string sql = @"UPDATE clientes
                           SET
                               nombre = @nombre,
                               apellido = @apellido,
                               dni = @dni,
                               telefono = @telefono,
                               email = @email,
                               direccion = @direccion
                           WHERE id_cliente = @id;";

                    NpgsqlCommand comando = new NpgsqlCommand(sql, conexion);

                    comando.Parameters.AddWithValue("@id", cliente.ClienteId);
                    comando.Parameters.AddWithValue("@nombre", cliente.Nombre);
                    comando.Parameters.AddWithValue("@apellido", cliente.Apellido);
                    comando.Parameters.AddWithValue("@dni", cliente.Dni);
                    comando.Parameters.AddWithValue("@telefono", cliente.Telefono);
                    comando.Parameters.AddWithValue("@email", cliente.Email);
                    comando.Parameters.AddWithValue("@direccion", cliente.Direccion);

                    comando.ExecuteNonQuery();

                    MessageBox.Show("Cliente actualizado correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el cliente: " + ex.Message);
                }
            }
        }



        public void DarDeBajaCliente(int clienteId)
        {
            using (NpgsqlConnection conexion = new NpgsqlConnection(Database.CadenaConexion))
            {
                try
                {
                    conexion.Open();

                    string sql = @"UPDATE clientes
                           SET activo = FALSE
                           WHERE id_cliente = @id;";

                    NpgsqlCommand comando = new NpgsqlCommand(sql, conexion);

                    comando.Parameters.AddWithValue("@id", clienteId);

                    comando.ExecuteNonQuery();

                    MessageBox.Show("Cliente dado de baja correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al dar de baja el cliente: " + ex.Message);
                }
            }
        }

    }
}

