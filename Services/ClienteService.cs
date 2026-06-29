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

            //siempre hacemos primero el GET
        public List<Cliente> ObtenerClientes()
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
                            direccion
                           
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
                                Direccion= reader["direccion"].ToString(),
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
                               direccion
                           )
                           VALUES
                           (
                               @nombre,
                               @apellido,
                               @dni,
                               @telefono,
                               @email,
                               @direccion
                           );";

                    NpgsqlCommand comando = new NpgsqlCommand(sql, conexion);

                    comando.Parameters.AddWithValue("@nombre", cliente.Nombre);
                    comando.Parameters.AddWithValue("@apellido", cliente.Apellido);
                    comando.Parameters.AddWithValue("@dni", cliente.Dni);
                    comando.Parameters.AddWithValue("@telefono", cliente.Telefono);
                    comando.Parameters.AddWithValue("@email", cliente.Email);
                    comando.Parameters.AddWithValue("@direccion", cliente.Direccion);

                    comando.ExecuteNonQuery();

                    MessageBox.Show("Cliente agregado correctamente.");



                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar el cliente: " + ex.Message);
                }
            }
        }



    }
}

