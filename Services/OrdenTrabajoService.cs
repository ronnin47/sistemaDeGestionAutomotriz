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

                                
                     string sql = @"SELECT
                                    o.id_orden,
                                    ts.categoria,
                                    ts.nombre AS tipo_servicio,
                                    CONCAT(c.apellido, ', ', c.nombre) AS cliente,
                                    c.dni,
                                    c.telefono,
                                    o.vehiculo,
                                    o.tipo_modulo,
                                    o.detalle,
                                    o.diagnostico,
                                    o.es_reparable,
                                    CONCAT(u.apellido, ', ', u.nombre) AS tecnico_asignado,
                                    o.fecha AS fecha_ingreso,
                                    o.estado,
                                    o.precio,
                                    o.garantia,
                                    o.motivo_garantia,
                                    o.observaciones
                                FROM ordenes_trabajo o
                                INNER JOIN clientes c
                                    ON o.id_cliente = c.id_cliente
                                INNER JOIN usuarios u
                                    ON o.id_usuario = u.id_usuario
                                INNER JOIN tipos_servicio ts
                                    ON o.id_tipo = ts.id_tipo
                                ORDER BY o.id_orden DESC;";

                                                    

              
                    NpgsqlCommand comando = new NpgsqlCommand(sql, conexion);

                    NpgsqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {



                        OrdenTrabajoDto orden = new OrdenTrabajoDto
                        {
                            NumeroOrden = Convert.ToInt32(reader["id_orden"]),
                            Categoria = reader["categoria"].ToString(),
                            TipoServicio = reader["tipo_servicio"].ToString(),
                            Cliente = reader["cliente"].ToString(),
                            Dni = reader["dni"].ToString(),
                            Telefono = reader["telefono"].ToString(),
                            Vehiculo = reader["vehiculo"].ToString(),
                            TipoModulo = reader["tipo_modulo"].ToString(),
                            Detalle = reader["detalle"].ToString(),
                            Diagnostico = reader["diagnostico"] != DBNull.Value ? reader["diagnostico"].ToString() : "",
                            EsReparable = reader["es_reparable"] != DBNull.Value && Convert.ToBoolean(reader["es_reparable"]),
                            TecnicoAsignado = reader["tecnico_asignado"].ToString(),
                            FechaIngreso = Convert.ToDateTime(reader["fecha_ingreso"]),
                            Estado = reader["estado"].ToString(),
                            Precio = reader["precio"] != DBNull.Value ? Convert.ToDecimal(reader["precio"]) : 0,
                            Garantia = reader["garantia"] != DBNull.Value && Convert.ToBoolean(reader["garantia"]),
                            MotivoGarantia = reader["motivo_garantia"] != DBNull.Value ? reader["motivo_garantia"].ToString() : "",
                            Observaciones = reader["observaciones"] != DBNull.Value ? reader["observaciones"].ToString() : ""
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
