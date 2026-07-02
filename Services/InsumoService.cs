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
    class InsumoService
    {




        public List<Insumo> ObtenerTodosLosInsumos()
        {
            List<Insumo> listaInsumos = new List<Insumo>();

            using (NpgsqlConnection conexion = new NpgsqlConnection(Database.CadenaConexion))
            {
                try
                {
                    conexion.Open();

                    string sql = @"SELECT
                                k.id_kit,
                                k.nombre,
                                k.precio,
                                k.stock,
                                p.nombre AS proveedor
                            FROM kits_insumos k
                            INNER JOIN proveedores p
                                ON k.id_proveedor = p.id_proveedor
                            ORDER BY k.nombre;";

                    NpgsqlCommand comando = new NpgsqlCommand(sql, conexion);

                    NpgsqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {



                        Insumo insumo = new Insumo
                        {
                            IdKit = Convert.ToInt32(reader["id_kit"]),
                            Nombre = reader["nombre"].ToString(),
                            Precio = Convert.ToDecimal(reader["precio"]),
                            Stock = Convert.ToInt32(reader["stock"]),
                            Proveedor = reader["proveedor"].ToString()
                        };

                        listaInsumos.Add(insumo);
                    }

                    return listaInsumos;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de conexión: " + ex.Message);
                    return new List<Insumo>();
                }
            }
        }
    }


   
}
