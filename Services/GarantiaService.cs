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
    class GarantiaService
    {
        public List<GarantiaDto> ObtenerTodasLasGarantias()
        {
            List<GarantiaDto> listaGarantias = new List<GarantiaDto>();

            using (NpgsqlConnection conexion = new NpgsqlConnection(Database.CadenaConexion))
            {
                try
                {
                    conexion.Open();

                    string sql = @"SELECT
                                 o.id_orden,
                                  CONCAT(c.apellido, ', ', c.nombre) AS cliente,
                                    o.detalle,
                                    CONCAT(u.apellido, ', ', u.nombre) AS asignado,
                                    o.estado,
                                    o.motivo_garantia AS condicion
                                FROM ordenes_trabajo o
                                INNER JOIN clientes c
                                    ON o.id_cliente = c.id_cliente
                                INNER JOIN usuarios u
                                    ON o.id_usuario = u.id_usuario
                                WHERE o.garantia = TRUE
                                ORDER BY o.id_orden DESC;";

                    NpgsqlCommand comando = new NpgsqlCommand(sql, conexion);

                    NpgsqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        GarantiaDto garantia = new GarantiaDto
                        {
                            IdOrden = Convert.ToInt32(reader["id_orden"]),
                            Cliente = reader["cliente"].ToString(),
                            Detalle = reader["detalle"].ToString(),
                            Asignado = reader["asignado"].ToString(),
                            Estado = reader["estado"].ToString(),
                            Condicion = reader["condicion"].ToString()

                        };

                        listaGarantias.Add(garantia);
                    }

                    return listaGarantias;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de conexión: " + ex.Message);
                    return new List<GarantiaDto>();
                }
            }
        }
    }
}
