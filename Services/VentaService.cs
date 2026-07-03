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
    class VentaService
    {

        public List<VentaInsumosDto> ObtenerVentaInsumos()
        {
            List<VentaInsumosDto> listaVentaInsumos = new List<VentaInsumosDto>();

            using (NpgsqlConnection conexion = new NpgsqlConnection(Database.CadenaConexion))
            {
                try
                {
                    conexion.Open();

                    string sql = @"SELECT
                                v.id_venta,
                                CONCAT(c.apellido, ', ', c.nombre) AS cliente,
                                k.nombre AS insumo,
                                v.cantidad,
                                v.total,
                                v.fecha
                            FROM ventas v
                            INNER JOIN clientes c
                                ON v.id_cliente = c.id_cliente
                            INNER JOIN kits_insumos k
                                ON v.id_kit = k.id_kit
                            ORDER BY v.id_venta DESC;";

                    NpgsqlCommand comando = new NpgsqlCommand(sql, conexion);

                    NpgsqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {



                      


                        VentaInsumosDto ventaInsumos = new VentaInsumosDto
                        {
                            NumeroVenta = Convert.ToInt32(reader["id_venta"]),
                            Cliente = reader["cliente"].ToString(),
                            Insumo = reader["insumo"].ToString(),
                            Cantidad = Convert.ToInt32(reader["cantidad"]),
                            Total = Convert.ToDecimal(reader["total"]),
                            Fecha = Convert.ToDateTime(reader["fecha"])
                        };

                        listaVentaInsumos.Add(ventaInsumos);
                    }

                    return listaVentaInsumos;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de conexión: " + ex.Message);
                    return new List<VentaInsumosDto>();
                }
            }
        }

        public bool RegistrarNuevaVenta(Venta venta)
        {
            using (NpgsqlConnection conexion = new NpgsqlConnection(Database.CadenaConexion))
            {
                try
                {
                    conexion.Open();

                    // Iniciamos una transacción para asegurar la integridad de los datos
                    using (NpgsqlTransaction transaccion = conexion.BeginTransaction())
                    {
                        try
                        {
                            // 1. Insertar la venta en la tabla 'ventas'
                            string queryVenta = @"INSERT INTO ventas (id_cliente, id_kit, cantidad, total, fecha) 
                                         VALUES (@id_cliente, @id_kit, @cantidad, @total, @fecha);";

                            using (NpgsqlCommand cmdVenta = new NpgsqlCommand(queryVenta, conexion, transaccion))
                            {
                                cmdVenta.Parameters.AddWithValue("@id_cliente", venta.IdCliente);
                                cmdVenta.Parameters.AddWithValue("@id_kit", venta.IdKit);
                                cmdVenta.Parameters.AddWithValue("@cantidad", venta.Cantidad);
                                cmdVenta.Parameters.AddWithValue("@total", venta.Total);
                                cmdVenta.Parameters.AddWithValue("@fecha", DateTime.Now);

                                cmdVenta.ExecuteNonQuery();
                            }

                            // 2. Descontar el stock en la tabla 'kits_insumos'
                            string queryStock = @"UPDATE kits_insumos 
                                         SET stock = stock - @cantidad 
                                         WHERE id_kit = @id_kit;";

                            using (NpgsqlCommand cmdStock = new NpgsqlCommand(queryStock, conexion, transaccion))
                            {
                                cmdStock.Parameters.AddWithValue("@cantidad", venta.Cantidad);
                                cmdStock.Parameters.AddWithValue("@id_kit", venta.IdKit);

                                cmdStock.ExecuteNonQuery();
                            }

                            // Si ambas operaciones fueron exitosas, confirmamos los cambios
                            transaccion.Commit();
                            return true;
                        }
                        catch (Exception)
                        {
                            // Si algo falla aquí adentro, se deshacen los cambios de forma segura
                            transaccion.Rollback();
                            throw; // Volvemos a lanzar la excepción para que la capture el bloque catch externo
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar la venta en la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }










        }
}