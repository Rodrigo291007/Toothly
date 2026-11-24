using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Sistema_Toothly.Clases;

namespace Sistema_Toothly.Datos
{
    // Clase para administrar las ordenes de compra y sus detalles en SQL Server
    public static class OrdenCompraDatos
    {
        // Propiedades de compatibilidad que consultan directo a SQL
        public static List<OrdenCompra> ordenes => Listar();
        public static List<OrdenCompraDetalle> detalles => ListarTodosDetalles();

        // Obtiene todas las ordenes de compra registradas
        public static List<OrdenCompra> Listar()
        {
            List<OrdenCompra> lista = new List<OrdenCompra>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "SELECT id_orden, id_proveedor, fecha, total, estado FROM dbo.OrdenCompra ORDER BY id_orden DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearOrden(dr));
                        }
                    }
                }
            }
            return lista;
        }

        // Inserta una nueva orden de compra y obtiene su ID generado
        public static void Agregar(OrdenCompra o)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"INSERT INTO dbo.OrdenCompra (id_proveedor, fecha, total, estado)
                                 VALUES (@idProv, @fecha, @total, @estado);
                                 SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idProv", o.IdProveedor);
                    cmd.Parameters.AddWithValue("@fecha", o.Fecha);
                    cmd.Parameters.AddWithValue("@total", o.Total);
                    cmd.Parameters.AddWithValue("@estado", string.IsNullOrEmpty(o.Estado) ? "Pendiente" : o.Estado);

                    object idGen = cmd.ExecuteScalar();
                    if (idGen != null && idGen != DBNull.Value)
                    {
                        o.IdOrden = Convert.ToInt32(idGen);
                    }
                }
            }
        }

        // Modifica los datos de una orden de compra
        public static void Modificar(OrdenCompra o)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"UPDATE dbo.OrdenCompra 
                                 SET id_proveedor = @idProv, fecha = @fecha, total = @total, estado = @estado 
                                 WHERE id_orden = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", o.IdOrden);
                    cmd.Parameters.AddWithValue("@idProv", o.IdProveedor);
                    cmd.Parameters.AddWithValue("@fecha", o.Fecha);
                    cmd.Parameters.AddWithValue("@total", o.Total);
                    cmd.Parameters.AddWithValue("@estado", o.Estado);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Agrega un producto al detalle de la orden de compra
        public static void AgregarDetalle(OrdenCompraDetalle d)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"INSERT INTO dbo.OrdenCompraDetalle (id_orden, id_producto, cantidad, precio_unitario, subtotal)
                                 VALUES (@idOrd, @idProd, @cant, @precio, @subt);
                                 SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idOrd", d.IdOrden);
                    cmd.Parameters.AddWithValue("@idProd", d.IdProducto);
                    cmd.Parameters.AddWithValue("@cant", d.Cantidad);
                    cmd.Parameters.AddWithValue("@precio", d.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@subt", d.Subtotal);

                    object idGen = cmd.ExecuteScalar();
                    if (idGen != null && idGen != DBNull.Value)
                    {
                        d.IdDetalle = Convert.ToInt32(idGen);
                    }
                }
            }
        }

        // Obtiene todos los productos de una orden de compra especifica
        public static List<OrdenCompraDetalle> ObtenerDetalles(int idOrden)
        {
            List<OrdenCompraDetalle> lista = new List<OrdenCompraDetalle>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT id_detalle, id_orden, id_producto, cantidad, precio_unitario, subtotal 
                                 FROM dbo.OrdenCompraDetalle 
                                 WHERE id_orden = @idOrd";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idOrd", idOrden);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new OrdenCompraDetalle
                            {
                                IdDetalle = Convert.ToInt32(dr["id_detalle"]),
                                IdOrden = Convert.ToInt32(dr["id_orden"]),
                                IdProducto = Convert.ToInt32(dr["id_producto"]),
                                Cantidad = Convert.ToInt32(dr["cantidad"]),
                                PrecioUnitario = Convert.ToDecimal(dr["precio_unitario"]),
                                Subtotal = Convert.ToDecimal(dr["subtotal"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // Elimina los detalles de una orden
        public static void EliminarDetalles(int idOrden)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "DELETE FROM dbo.OrdenCompraDetalle WHERE id_orden = @idOrd";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idOrd", idOrden);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Marca la orden como 'Recibida' y suma el stock a los productos recibidos.
        // Todo ocurre en una transacción: o se actualiza el estado y todo el inventario, o nada.
        public static void RecibirOrden(int idOrden)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            using (SqlTransaction tx = con.BeginTransaction())
            {
                try
                {
                    // 1. Cambiamos estado de la orden a 'Recibida' (solo si sigue Pendiente)
                    string queryEstado = "UPDATE dbo.OrdenCompra SET estado = 'Recibida' WHERE id_orden = @idOrd AND estado = 'Pendiente'";
                    using (SqlCommand cmdEstado = new SqlCommand(queryEstado, con, tx))
                    {
                        cmdEstado.Parameters.AddWithValue("@idOrd", idOrden);
                        if (cmdEstado.ExecuteNonQuery() == 0)
                        {
                            tx.Rollback();
                            return;
                        }
                    }

                    // 2. Obtenemos los detalles de la orden
                    List<OrdenCompraDetalle> listaDetalles = new List<OrdenCompraDetalle>();
                    using (SqlCommand cmdDet = new SqlCommand("SELECT id_producto, cantidad FROM dbo.OrdenCompraDetalle WHERE id_orden = @idOrd", con, tx))
                    {
                        cmdDet.Parameters.AddWithValue("@idOrd", idOrden);
                        using (SqlDataReader dr = cmdDet.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                listaDetalles.Add(new OrdenCompraDetalle
                                {
                                    IdProducto = Convert.ToInt32(dr["id_producto"]),
                                    Cantidad = Convert.ToInt32(dr["cantidad"])
                                });
                            }
                        }
                    }

                    // 3. Incrementamos el stock en inventario y registramos la entrada
                    foreach (var d in listaDetalles)
                    {
                        string queryStock = "UPDATE dbo.Producto SET stock_actual = stock_actual + (@cant * unidades_por_caja) WHERE id_producto = @idProd";
                        using (SqlCommand cmdStock = new SqlCommand(queryStock, con, tx))
                        {
                            cmdStock.Parameters.AddWithValue("@cant", d.Cantidad);
                            cmdStock.Parameters.AddWithValue("@idProd", d.IdProducto);
                            cmdStock.ExecuteNonQuery();
                        }

                        string queryMov = @"INSERT INTO dbo.MovimientoInventario (id_producto, tipo, cantidad, fecha, descripcion)
                                            SELECT @idProd, 'Entrada', @cant * unidades_por_caja, GETDATE(), @desc
                                            FROM dbo.Producto WHERE id_producto = @idProd";
                        using (SqlCommand cmdMov = new SqlCommand(queryMov, con, tx))
                        {
                            cmdMov.Parameters.AddWithValue("@idProd", d.IdProducto);
                            cmdMov.Parameters.AddWithValue("@cant", d.Cantidad);
                            cmdMov.Parameters.AddWithValue("@desc", "Entrada por recepción de Orden de Compra #" + idOrden + " (" + d.Cantidad + " caja(s))");
                            cmdMov.ExecuteNonQuery();
                        }
                    }

                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        // Guarda la orden y sus productos en una sola transacción (maestro-detalle)
        public static void AgregarConDetalles(OrdenCompra o, List<OrdenCompraDetalle> detalles)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            using (SqlTransaction tx = con.BeginTransaction())
            {
                try
                {
                    string query = @"INSERT INTO dbo.OrdenCompra (id_proveedor, fecha, total, estado)
                                     VALUES (@idProv, @fecha, @total, @estado);
                                     SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(query, con, tx))
                    {
                        cmd.Parameters.AddWithValue("@idProv", o.IdProveedor);
                        cmd.Parameters.AddWithValue("@fecha", o.Fecha);
                        cmd.Parameters.AddWithValue("@total", o.Total);
                        cmd.Parameters.AddWithValue("@estado", string.IsNullOrEmpty(o.Estado) ? "Pendiente" : o.Estado);
                        o.IdOrden = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    InsertarDetalles(con, tx, o.IdOrden, detalles);
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        // Modifica una orden PENDIENTE (proveedor, fecha, total y productos) en una transacción.
        // Una orden Recibida ya sumó stock al inventario, por eso no se puede cambiar.
        public static void ModificarConDetalles(OrdenCompra o, List<OrdenCompraDetalle> detalles)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            using (SqlTransaction tx = con.BeginTransaction())
            {
                try
                {
                    string query = @"UPDATE dbo.OrdenCompra
                                     SET id_proveedor = @idProv, fecha = @fecha, total = @total
                                     WHERE id_orden = @id AND estado = 'Pendiente'";
                    using (SqlCommand cmd = new SqlCommand(query, con, tx))
                    {
                        cmd.Parameters.AddWithValue("@id", o.IdOrden);
                        cmd.Parameters.AddWithValue("@idProv", o.IdProveedor);
                        cmd.Parameters.AddWithValue("@fecha", o.Fecha);
                        cmd.Parameters.AddWithValue("@total", o.Total);
                        if (cmd.ExecuteNonQuery() == 0)
                            throw new InvalidOperationException("Solo se pueden modificar órdenes en estado Pendiente.");
                    }

                    using (SqlCommand cmdDel = new SqlCommand("DELETE FROM dbo.OrdenCompraDetalle WHERE id_orden = @id", con, tx))
                    {
                        cmdDel.Parameters.AddWithValue("@id", o.IdOrden);
                        cmdDel.ExecuteNonQuery();
                    }

                    InsertarDetalles(con, tx, o.IdOrden, detalles);
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        // Elimina una orden PENDIENTE junto con sus productos
        public static void Eliminar(int idOrden)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            using (SqlTransaction tx = con.BeginTransaction())
            {
                try
                {
                    string estado = "";
                    using (SqlCommand cmdEst = new SqlCommand("SELECT estado FROM dbo.OrdenCompra WITH (UPDLOCK) WHERE id_orden = @id", con, tx))
                    {
                        cmdEst.Parameters.AddWithValue("@id", idOrden);
                        estado = Convert.ToString(cmdEst.ExecuteScalar()) ?? "";
                    }
                    if (estado != "Pendiente")
                        throw new InvalidOperationException("Solo se pueden eliminar órdenes en estado Pendiente (la orden está " + (estado == "" ? "inexistente" : estado) + ").");

                    using (SqlCommand cmdDet = new SqlCommand("DELETE FROM dbo.OrdenCompraDetalle WHERE id_orden = @id", con, tx))
                    {
                        cmdDet.Parameters.AddWithValue("@id", idOrden);
                        cmdDet.ExecuteNonQuery();
                    }
                    using (SqlCommand cmdOrd = new SqlCommand("DELETE FROM dbo.OrdenCompra WHERE id_orden = @id", con, tx))
                    {
                        cmdOrd.Parameters.AddWithValue("@id", idOrden);
                        cmdOrd.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        private static void InsertarDetalles(SqlConnection con, SqlTransaction tx, int idOrden, List<OrdenCompraDetalle> detalles)
        {
            string query = @"INSERT INTO dbo.OrdenCompraDetalle (id_orden, id_producto, cantidad, precio_unitario, subtotal)
                             VALUES (@idOrd, @idProd, @cant, @precio, @subt);
                             SELECT SCOPE_IDENTITY();";
            foreach (var d in detalles)
            {
                d.IdOrden = idOrden;
                using (SqlCommand cmd = new SqlCommand(query, con, tx))
                {
                    cmd.Parameters.AddWithValue("@idOrd", idOrden);
                    cmd.Parameters.AddWithValue("@idProd", d.IdProducto);
                    cmd.Parameters.AddWithValue("@cant", d.Cantidad);
                    cmd.Parameters.AddWithValue("@precio", d.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@subt", d.Subtotal);
                    d.IdDetalle = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // Busca ordenes por nombre del proveedor o por estado
        public static List<OrdenCompra> Buscar(string texto)
        {
            List<OrdenCompra> lista = new List<OrdenCompra>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT o.id_orden, o.id_proveedor, o.fecha, o.total, o.estado 
                                 FROM dbo.OrdenCompra o
                                 INNER JOIN dbo.Proveedor p ON o.id_proveedor = p.id_proveedor
                                 WHERE p.nombre LIKE @buscar OR o.estado LIKE @buscar
                                 ORDER BY o.id_orden DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@buscar", "%" + texto + "%");
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearOrden(dr));
                        }
                    }
                }
            }
            return lista;
        }

        // Metodo de ayuda para mapear fila SQL a objeto OrdenCompra
        private static OrdenCompra MapearOrden(SqlDataReader dr)
        {
            return new OrdenCompra
            {
                IdOrden = Convert.ToInt32(dr["id_orden"]),
                IdProveedor = Convert.ToInt32(dr["id_proveedor"]),
                Fecha = Convert.ToDateTime(dr["fecha"]),
                Total = Convert.ToDecimal(dr["total"]),
                Estado = dr["estado"].ToString()!
            };
        }

        // Metodo de ayuda para listar todos los detalles si algun reporte general lo necesita
        private static List<OrdenCompraDetalle> ListarTodosDetalles()
        {
            List<OrdenCompraDetalle> lista = new List<OrdenCompraDetalle>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "SELECT id_detalle, id_orden, id_producto, cantidad, precio_unitario, subtotal FROM dbo.OrdenCompraDetalle";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new OrdenCompraDetalle
                            {
                                IdDetalle = Convert.ToInt32(dr["id_detalle"]),
                                IdOrden = Convert.ToInt32(dr["id_orden"]),
                                IdProducto = Convert.ToInt32(dr["id_producto"]),
                                Cantidad = Convert.ToInt32(dr["cantidad"]),
                                PrecioUnitario = Convert.ToDecimal(dr["precio_unitario"]),
                                Subtotal = Convert.ToDecimal(dr["subtotal"])
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}
