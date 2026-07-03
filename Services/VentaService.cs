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

                    // 1. SUMAMOS v.estado A TU CONSULTA SELECT
                    string sql = @"SELECT
                        v.id_venta,
                        CONCAT(c.apellido, ', ', c.nombre) AS cliente,
                        k.nombre AS insumo,
                        c.id_cliente,
                        v.id_kit,
                        v.cantidad,
                        v.total,
                        v.fecha,
                        v.estado -- <--- AGREGADO
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
                            idCliente = Convert.ToInt32(reader["id_cliente"]),
                            idKit = Convert.ToInt32(reader["id_kit"]),
                            Cliente = reader["cliente"].ToString(),
                            Insumo = reader["insumo"].ToString(),
                            Cantidad = Convert.ToInt32(reader["cantidad"]),
                            Total = Convert.ToDecimal(reader["total"]),
                            Fecha = Convert.ToDateTime(reader["fecha"]),

                            // 2. LEEMOS EL ESTADO DE MANERA SEGURA CONTRA VALORES VACÍOS
                            Estado = reader["estado"] != DBNull.Value ? reader["estado"].ToString() : "Completada"
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
                            string queryVenta = @"INSERT INTO ventas (id_cliente, id_kit, cantidad, total, fecha, estado) 
                                         VALUES (@id_cliente, @id_kit, @cantidad, @total, @fecha, @estado);";

                            using (NpgsqlCommand cmdVenta = new NpgsqlCommand(queryVenta, conexion, transaccion))
                            {
                                cmdVenta.Parameters.AddWithValue("@id_cliente", venta.IdCliente);
                                cmdVenta.Parameters.AddWithValue("@id_kit", venta.IdKit);
                                cmdVenta.Parameters.AddWithValue("@cantidad", venta.Cantidad);
                                cmdVenta.Parameters.AddWithValue("@total", venta.Total);
                                cmdVenta.Parameters.AddWithValue("@fecha", DateTime.Now);
                                // Aseguramos que la venta nazca con estado activo 'Completada'
                                cmdVenta.Parameters.AddWithValue("@estado", "Completada");
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


        /*
        public void ActualizarVenta(VentaInsumosDto venta)
        {
          
            using (NpgsqlConnection conexion = new NpgsqlConnection(Database.CadenaConexion))
            {
                try
                {
                    conexion.Open();

                    string sql = @"";

                    NpgsqlCommand comando = new NpgsqlCommand(sql, conexion);

                    comando.Parameters.AddWithValue("@id", cliente.ClienteId);
                   

                    comando.ExecuteNonQuery();

                    MessageBox.Show("Cliente actualizado correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el cliente: " + ex.Message);
                }
            }
            
        }
        */
        public bool AnularVenta(int idVenta, int idKit, int cantidadAReponer)
        {
            using (NpgsqlConnection conexion = new NpgsqlConnection(Database.CadenaConexion))
            {
                try
                {
                    conexion.Open();

                    // Usamos una transacción para resguardar que se anule la venta y se devuelva el stock juntos
                    using (NpgsqlTransaction transaccion = conexion.BeginTransaction())
                    {
                        try
                        {
                            // PASO 1: Cambiar el estado de la venta a 'Anulada'
                            string sqlVenta = @"UPDATE ventas 
                                        SET estado = 'Anulada' 
                                        WHERE id_venta = @idVenta;";

                            using (NpgsqlCommand cmdVenta = new NpgsqlCommand(sqlVenta, conexion, transaccion))
                            {
                                cmdVenta.Parameters.AddWithValue("@idVenta", idVenta);
                                cmdVenta.ExecuteNonQuery();
                            }

                            // PASO 2: Devolver el stock sumando la cantidad en kits_insumos (usando tus nombres de campos exactos)
                            string sqlStock = @"UPDATE kits_insumos 
                                        SET stock = stock + @cantidad 
                                        WHERE id_kit = @idKit;";

                            using (NpgsqlCommand cmdStock = new NpgsqlCommand(sqlStock, conexion, transaccion))
                            {
                                cmdStock.Parameters.AddWithValue("@cantidad", cantidadAReponer);
                                cmdStock.Parameters.AddWithValue("@idKit", idKit);
                                cmdStock.ExecuteNonQuery();
                            }

                            // Confirmamos la operación en PostgreSQL
                            transaccion.Commit();

                            MessageBox.Show("La venta ha sido anulada correctamente y las unidades regresaron al inventario.",
                                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        catch (Exception exInner)
                        {
                            // Deshacemos todo si algo falla para no romper los números del negocio
                            transaccion.Rollback();
                            throw new Exception("Error en los comandos de anulación: " + exInner.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de conexión al procesar la baja: " + ex.Message,
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }


    }
}