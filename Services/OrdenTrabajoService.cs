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


        //get 
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
                                    o.id_usuario, 
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
                            IdUsuario = Convert.ToInt32(reader["id_usuario"]),
                            Categoria = reader["categoria"].ToString(),
                            Cliente = reader["cliente"].ToString(),
                            TipoServicio = reader["tipo_servicio"].ToString(),
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









      



        //insert nueva orden Modulo
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
                    comando.Parameters.AddWithValue("@id_tipo", orden.TipoModuloId);
                    comando.Parameters.AddWithValue("@tipo_modulo", orden.TipoModulo);


                    //detalle
                    comando.Parameters.AddWithValue("@modelo", orden.Modelo);
                    comando.Parameters.AddWithValue("@tipo_vehiculo", orden.TipoVehiculo);
                    comando.Parameters.AddWithValue("@combustible", orden.Combustible);
                    comando.Parameters.AddWithValue("@vehiculo", orden.TipoVehiculo);


                    //MessageBox.Show($" lo que tiene marca {orden.Marca}");
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

        //insert nueva orden cerrajeria
        public bool CrearNuevaOrdenCerrajeria(OrdenTrabajoCerrajeria orden)
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
                    comando.Parameters.AddWithValue("@id_tipo", orden.TipoServicioId);//aca tiene que entrar en uno de los tres casos de jerarquia
                                                                                    //segun el id tipo

                    comando.Parameters.AddWithValue("@tipo_modulo", orden.TipoServicio);

                  

             
                    comando.Parameters.AddWithValue("@vehiculo", orden.Marca);

                    //detalle
                    string detalle = $"Servicio: { orden.TipoServicio} | Tipo: {orden.Marca}";
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

        //insert nueva orden Instalacion
        public bool CrearNuevaOrdenInstalacion(OrdenTrabajoInstalacion orden)
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
                    comando.Parameters.AddWithValue("@id_tipo", orden.TipoServicioId);


                    comando.Parameters.AddWithValue("@tipo_modulo", orden.TipoServicio);




                    comando.Parameters.AddWithValue("@vehiculo", orden.Marca);

                    //detalle
                    string detalle = $"Servicio: { orden.TipoServicio} | Tipo: {orden.Marca}";
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






        //anular una orden
        public bool AnularOrden(int idOrden)
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
                            string sqlOrden = @"UPDATE ordenes_trabajo 
                                        SET estado = 'Anulada' 
                                        WHERE id_orden = @idOrden;";

                            using (NpgsqlCommand cmdOrden = new NpgsqlCommand(sqlOrden, conexion, transaccion))
                            {
                                cmdOrden.Parameters.AddWithValue("@idOrden", idOrden);
                                cmdOrden.ExecuteNonQuery();
                            }
                            /*
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
                            */
                            // Confirmamos la operación en PostgreSQL
                            transaccion.Commit();

                            MessageBox.Show("La órden ha sido anulada correctamente.",
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

        public bool ActualizarOrden(OrdenTrabajoDto orden)
        {
            using (NpgsqlConnection conexion = new NpgsqlConnection(Database.CadenaConexion))
            {
                try
                {
                    conexion.Open();

                    // Usamos una transacción para asegurar que se actualice la orden y el teléfono juntos
                    using (NpgsqlTransaction transaccion = conexion.BeginTransaction())
                    {
                        try
                        {
                            // PASO 1: Actualizar los campos operativos en la tabla ordenes_trabajo (ID DIRECTO)
                            string sqlOrden = @"UPDATE ordenes_trabajo 
                                        SET id_usuario = @id_usuario,
                                            diagnostico = @diagnostico,
                                            es_reparable = @es_reparable,
                                            estado = @estado,
                                            precio = @precio,
                                            garantia = @garantia,
                                            motivo_garantia = @motivo_garantia,
                                            observaciones = @observaciones
                                        WHERE id_orden = @id_orden;";

                            using (NpgsqlCommand comando = new NpgsqlCommand(sqlOrden, conexion, transaccion))
                            {
                                // Le pasamos directamente el IdUsuario numérico que viaja de forma segura
                                comando.Parameters.AddWithValue("@id_usuario", orden.IdUsuario);
                                comando.Parameters.AddWithValue("@diagnostico", orden.Diagnostico);
                                comando.Parameters.AddWithValue("@es_reparable", orden.EsReparable);
                                comando.Parameters.AddWithValue("@estado", orden.Estado);
                                comando.Parameters.AddWithValue("@precio", orden.Precio);
                                comando.Parameters.AddWithValue("@garantia", orden.Garantia);
                                comando.Parameters.AddWithValue("@motivo_garantia", orden.MotivoGarantia);
                                comando.Parameters.AddWithValue("@observaciones", orden.Observaciones);
                                comando.Parameters.AddWithValue("@id_orden", orden.NumeroOrden);

                                comando.ExecuteNonQuery();
                            }

                            // PASO 2: Actualizar el campo telefono en la tabla clientes usando el id_cliente de esta orden
                            string sqlTelefono = @"UPDATE clientes 
                                          SET telefono = @telefono 
                                          WHERE id_cliente = (SELECT id_cliente FROM ordenes_trabajo WHERE id_orden = @id_orden);";

                            using (NpgsqlCommand cmdTel = new NpgsqlCommand(sqlTelefono, conexion, transaccion))
                            {
                                cmdTel.Parameters.AddWithValue("@telefono", orden.Telefono);
                                cmdTel.Parameters.AddWithValue("@id_orden", orden.NumeroOrden);

                                cmdTel.ExecuteNonQuery();
                            }

                            // Confirmamos de forma definitiva todos los cambios en Supabase
                            transaccion.Commit();
                            return true;
                        }
                        catch (Exception exInner)
                        {
                            // Si algo falla dentro del proceso, revertimos todo para no corromper la información
                            transaccion.Rollback();
                            throw new Exception("Error en la ejecución de los comandos de actualización: " + exInner.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar la orden de trabajo: " + ex.Message, "Error de Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }


    }


}
