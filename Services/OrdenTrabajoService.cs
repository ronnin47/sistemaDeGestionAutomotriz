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









        //inser de nueva orden
        //CraarNuevaOrden  INSERT
        //tiene que conseguir primero el id_cliente y si no existe tienen que insertar el cliente y depsues 
        //insertar la orden




        public bool CrearNuevaOrden(OrdenTrabajo orden)
        {
            using (NpgsqlConnection conexion = new NpgsqlConnection(Database.CadenaConexion))
            {
                try
                {
                    conexion.Open();

                    int idCliente;

                    // Buscar cliente por DNI
                    string sqlBuscar = @"SELECT id_cliente
                                 FROM clientes
                                 WHERE dni = @dni;";

                    NpgsqlCommand cmdBuscar = new NpgsqlCommand(sqlBuscar, conexion);
                    cmdBuscar.Parameters.AddWithValue("@dni", orden.Dni);

                    object resultado = cmdBuscar.ExecuteScalar();

                    if (resultado != null)
                    {
                        idCliente = Convert.ToInt32(resultado);
                    }
                    else
                    {
                        // Insertar cliente
                        string sqlCliente = @"INSERT INTO clientes
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
                                        TRUE
                                    )
                                    RETURNING id_cliente;";

                        NpgsqlCommand cmdCliente = new NpgsqlCommand(sqlCliente, conexion);

                        cmdCliente.Parameters.AddWithValue("@nombre", orden.NombreCliente);
                        cmdCliente.Parameters.AddWithValue("@apellido", orden.ApellidoCliente);
                        cmdCliente.Parameters.AddWithValue("@dni", orden.Dni);
                        cmdCliente.Parameters.AddWithValue("@telefono", orden.Telefono);
                        cmdCliente.Parameters.AddWithValue("@email", orden.Email);
                        cmdCliente.Parameters.AddWithValue("@direccion", orden.Direccion);

                        idCliente = Convert.ToInt32(cmdCliente.ExecuteScalar());
                    }

                    // Insertar orden
                    string sqlOrden = @"INSERT INTO ordenes_trabajo
                                (
                                    id_cliente,
                                    id_usuario,
                                    id_tipo,



                                    tipo_modulo,
                                    modelo,
                                    tipo_vehiculo,
                                    combustible,
                                    
                                    vehiculo,


                                    detalle,
                                    diagnostico,
                                    
                                    observaciones,
                                    estado,
                                    fecha,
                                    garantia
                                )
                                VALUES
                                (
                                    @id_cliente,
                                    @id_usuario,
                                    @id_tipo,

                                    @tipo_modulo,
                                    @modelo,
                                    @tipo_vehiculo,
                                    @combustible,

                                    @vehiculo,


                                    @detalle,
                                    @diagnostico,


                                    @observaciones,
                                    'Pendiente',
                                    CURRENT_DATE,
                                    @garantia
                                    
                                );";

                    NpgsqlCommand comando = new NpgsqlCommand(sqlOrden, conexion);


                    //cliente
                    comando.Parameters.AddWithValue("@id_cliente", idCliente);
                    comando.Parameters.AddWithValue("@id_usuario", orden.IdUsuarioAsignado);
                    comando.Parameters.AddWithValue("@id_tipo", 1);
                    comando.Parameters.AddWithValue("@tipo_modulo", orden.TipoModulo);


                    //detalle
                    comando.Parameters.AddWithValue("@modelo", orden.Modelo);
                    comando.Parameters.AddWithValue("@tipo_vehiculo", orden.TipoVehiculo);
                    comando.Parameters.AddWithValue("@combustible", orden.Combustible);
                    comando.Parameters.AddWithValue("@vehiculo", orden.TipoVehiculo);


                    MessageBox.Show($" lo que tiene marca {orden.Marca}");
                    comando.Parameters.AddWithValue("@vehiculo", orden.Marca);


                    string detalle = $"Modelo: {orden.Modelo} | Vehículo: {orden.TipoVehiculo} | Combustible: {orden.Combustible} | Tipo: {orden.Marca}";
                    comando.Parameters.AddWithValue("@detalle", detalle);


                   comando.Parameters.AddWithValue("@diagnostico", "Sin diagnostico");


                    comando.Parameters.AddWithValue("@observaciones", orden.Observaciones);
                    comando.Parameters.AddWithValue("@garantia", orden.Garantia);

                    int filasAfectadas = comando.ExecuteNonQuery();

                    return filasAfectadas > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al crear la orden: " + ex.Message);
                    return false;
                }
            }
        }

    

    }


}
