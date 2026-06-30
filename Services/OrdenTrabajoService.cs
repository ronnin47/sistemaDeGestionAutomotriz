using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Npgsql;
using sistemaDeGestionAutomotriz.Data;
using sistemaDeGestionAutomotriz.Models;




namespace sistemaDeGestionAutomotriz.Services
{

    public class OrdenTrabajoService
    {

        public List<OrdenTrabajoDto> ObtenerOrdenesTrabajo()
        {
            List<OrdenTrabajoDto> listaOrdenesTrabajo = new List<OrdenTrabajoDto>();

            using (NpgsqlConnection conexion = new NpgsqlConnection(Database.CadenaConexion))
            {
                try
                {
                    conexion.Open();

                    string sql = @"
                            SELECT 
                                orden_trabajo.id_orden,
                                tipo_servicio.nombre AS tipo_servicio,
                                CONCAT(clientes.nombre, ' ', clientes.apellido) AS cliente_nombre,
                                COALESCE(modulo.marca_modelo, orden_trabajo.observaciones, 'Sin detalle') AS detalle,
                                usuarios.nombre AS usuario_asignado,
                                clientes.telefono,
                                cotizacion.precio AS cotizacion,
                                orden_trabajo.fecha_ingreso,
                                orden_trabajo.estado
                            FROM orden_trabajo
                            LEFT JOIN tipo_servicio ON orden_trabajo.id_tipo_servicio = tipo_servicio.id_tipo_servicio
                            LEFT JOIN clientes ON orden_trabajo.id_cliente = clientes.id_cliente
                            LEFT JOIN usuarios ON orden_trabajo.id_usuario_asig = usuarios.id_usuario
                            LEFT JOIN modulo ON orden_trabajo.id_orden = modulo.id_orden
                            LEFT JOIN cotizacion ON orden_trabajo.id_orden = cotizacion.id_orden;";

                    NpgsqlCommand comando = new NpgsqlCommand(sql, conexion);

                    NpgsqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        OrdenTrabajoDto orden = new OrdenTrabajoDto
                        {
                            NumeroOrden = Convert.ToInt32(reader["id_orden"]),
                            Tipo = reader["tipo_servicio"].ToString(),
                            Cliente = reader["cliente_nombre"].ToString(), // Nombre y apellido juntos desde SQL
                            Detalle = reader["detalle"].ToString(),
                            Asignado = reader["usuario_asignado"] != DBNull.Value ? reader["usuario_asignado"].ToString() : "Sin asignar",
                            FechaIngreso = Convert.ToDateTime(reader["fecha_ingreso"]),
                            Estado = reader["estado"].ToString(),
                            Telefono = reader["telefono"].ToString(),

                            Cotizacion = reader["cotizacion"] != DBNull.Value ? Convert.ToDouble(reader["cotizacion"]) : 0,

                       

                        };



                        listaOrdenesTrabajo.Add(orden);
                    }

                    return listaOrdenesTrabajo;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de conexión: " + ex.Message);
                    return new List<OrdenTrabajoDto>();
                }
            }
        }

    }


}
