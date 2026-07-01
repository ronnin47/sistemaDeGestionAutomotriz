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
    class CotizacionService
    {

        public List<CotizacionPendienteDto> ObtenerClientesPendientes()
        {
            List<CotizacionPendienteDto> listaCotizacionesPendientes = new List<CotizacionPendienteDto>();

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
                                    o.detalle,
                                    o.estado,
                                    o.precio
                                FROM ordenes_trabajo o
                                INNER JOIN clientes c
                                    ON o.id_cliente = c.id_cliente
                                INNER JOIN tipos_servicio ts
                                    ON o.id_tipo = ts.id_tipo
                                WHERE o.estado = 'Esperando aprobación'
                                ORDER BY o.id_orden DESC;";

                    NpgsqlCommand comando = new NpgsqlCommand(sql, conexion);

                    NpgsqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                      


                        CotizacionPendienteDto cotizacion = new CotizacionPendienteDto
                        {
                            NumeroOrden = Convert.ToInt32(reader["id_orden"]),
                            Categoria = reader["categoria"].ToString(),
                            TipoServicio = reader["tipo_servicio"].ToString(),
                            Cliente = reader["cliente"].ToString(),
                            Detalle = reader["detalle"].ToString(),
                            Estado = reader["estado"].ToString(),
      
                             Precio = reader["precio"] != DBNull.Value ? Convert.ToDecimal(reader["precio"]) : 0,
                        };

                        listaCotizacionesPendientes.Add(cotizacion);
                    }

                    return listaCotizacionesPendientes;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de conexión: " + ex.Message);
                    return new List<CotizacionPendienteDto>();
                }
            }
        }



       
    }
}
