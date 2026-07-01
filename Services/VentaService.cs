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




    }
}