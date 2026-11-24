using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Sistema_Toothly.Clases;

namespace Sistema_Toothly.Datos
{
    // Clase para administrar el inventario, productos y categorias en SQL Server
    public static class ProductoDatos
    {
        // Propiedades de compatibilidad que consultan directo a SQL
        public static List<CategoriaProducto> categorias => ListarCategorias();
        public static List<Producto> productos => Listar();
        public static List<MovimientoInventario> movimientos => ListarMovimientos();

        // Obtiene todos los productos registrados
        public static List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT id_producto, codigo, nombre, id_categoria, stock_actual, 
                                        stock_minimo, precio_compra, fecha_vencimiento, unidades_por_caja, precio_venta
                                 FROM dbo.Producto 
                                 ORDER BY nombre ASC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearProducto(dr));
                        }
                    }
                }
            }
            return lista;
        }

        // Verifica si ya existe otro producto con el mismo código (columna UNIQUE en dbo.Producto)
        public static bool ExisteCodigo(string codigo, int idExcluir = 0)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "SELECT COUNT(*) FROM dbo.Producto WHERE codigo = @codigo AND id_producto <> @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@codigo", codigo.Trim());
                    cmd.Parameters.AddWithValue("@id", idExcluir);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        // Inserta un nuevo producto en el catalogo
        public static void Agregar(Producto p)
        {
            if (ExisteCodigo(p.Codigo))
                throw new InvalidOperationException("Ya existe un producto con el código " + p.Codigo + ".");

            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"INSERT INTO dbo.Producto (codigo, nombre, id_categoria, stock_actual, stock_minimo, precio_compra, fecha_vencimiento, unidades_por_caja, precio_venta)
                                 VALUES (@codigo, @nombre, @idCat, @stockAct, @stockMin, @precio, @fechaVenc, @unidades, @precioVenta);
                                 SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@codigo", p.Codigo);
                    cmd.Parameters.AddWithValue("@nombre", p.Nombre);
                    cmd.Parameters.AddWithValue("@idCat", p.IdCategoria);
                    cmd.Parameters.AddWithValue("@stockAct", p.StockActual);
                    cmd.Parameters.AddWithValue("@stockMin", p.StockMinimo);
                    cmd.Parameters.AddWithValue("@precio", p.PrecioCompra);
                    cmd.Parameters.AddWithValue("@fechaVenc", p.FechaVencimiento.Date);
                    cmd.Parameters.AddWithValue("@unidades", p.UnidadesPorCaja);
                    cmd.Parameters.AddWithValue("@precioVenta", p.PrecioVenta);

                    object idGen = cmd.ExecuteScalar();
                    if (idGen != null && idGen != DBNull.Value)
                    {
                        p.IdProducto = Convert.ToInt32(idGen);
                    }
                }
            }
        }

        // Modifica un producto existente
        public static void Modificar(Producto p)
        {
            if (ExisteCodigo(p.Codigo, p.IdProducto))
                throw new InvalidOperationException("Ya existe otro producto con el código " + p.Codigo + ".");

            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"UPDATE dbo.Producto 
                                 SET codigo = @codigo, nombre = @nombre, id_categoria = @idCat, 
                                     stock_actual = @stockAct, stock_minimo = @stockMin, 
                                     precio_compra = @precio, fecha_vencimiento = @fechaVenc,
                                     unidades_por_caja = @unidades, precio_venta = @precioVenta
                                 WHERE id_producto = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", p.IdProducto);
                    cmd.Parameters.AddWithValue("@codigo", p.Codigo);
                    cmd.Parameters.AddWithValue("@nombre", p.Nombre);
                    cmd.Parameters.AddWithValue("@idCat", p.IdCategoria);
                    cmd.Parameters.AddWithValue("@stockAct", p.StockActual);
                    cmd.Parameters.AddWithValue("@stockMin", p.StockMinimo);
                    cmd.Parameters.AddWithValue("@precio", p.PrecioCompra);
                    cmd.Parameters.AddWithValue("@fechaVenc", p.FechaVencimiento.Date);
                    cmd.Parameters.AddWithValue("@unidades", p.UnidadesPorCaja);
                    cmd.Parameters.AddWithValue("@precioVenta", p.PrecioVenta);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Busca productos por nombre o codigo
        public static List<Producto> Buscar(string texto)
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT id_producto, codigo, nombre, id_categoria, stock_actual, 
                                        stock_minimo, precio_compra, fecha_vencimiento, unidades_por_caja, precio_venta
                                 FROM dbo.Producto 
                                 WHERE nombre LIKE @buscar OR codigo LIKE @buscar 
                                 ORDER BY nombre ASC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@buscar", "%" + texto + "%");
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearProducto(dr));
                        }
                    }
                }
            }
            return lista;
        }

        // Obtiene los productos cuyo stock actual es menor al stock minimo
        public static List<Producto> ProductosBajoStock()
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT id_producto, codigo, nombre, id_categoria, stock_actual, 
                                        stock_minimo, precio_compra, fecha_vencimiento, unidades_por_caja, precio_venta
                                 FROM dbo.Producto 
                                 WHERE stock_actual < stock_minimo 
                                 ORDER BY stock_actual ASC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearProducto(dr));
                        }
                    }
                }
            }
            return lista;
        }

        // Obtiene productos que estan por vencer en los proximos 30 dias
        public static List<Producto> ProductosPorVencer()
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT id_producto, codigo, nombre, id_categoria, stock_actual, 
                                        stock_minimo, precio_compra, fecha_vencimiento, unidades_por_caja, precio_venta
                                 FROM dbo.Producto 
                                 WHERE fecha_vencimiento IS NOT NULL 
                                   AND fecha_vencimiento <= DATEADD(day, 30, CAST(GETDATE() AS DATE))
                                 ORDER BY fecha_vencimiento ASC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(MapearProducto(dr));
                        }
                    }
                }
            }
            return lista;
        }

        // Obtiene un producto por su ID
        public static Producto? ObtenerPorId(int id)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT id_producto, codigo, nombre, id_categoria, stock_actual, 
                                        stock_minimo, precio_compra, fecha_vencimiento, unidades_por_caja, precio_venta
                                 FROM dbo.Producto 
                                 WHERE id_producto = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return MapearProducto(dr);
                        }
                    }
                }
            }
            return null;
        }

        // Obtiene el nombre de la categoria segun su ID
        public static string ObtenerNombreCategoria(int idCategoria)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "SELECT nombre FROM dbo.CategoriaProducto WHERE id_categoria = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", idCategoria);
                    object res = cmd.ExecuteScalar();
                    if (res != null && res != DBNull.Value)
                    {
                        return res.ToString()!;
                    }
                }
            }
            return "";
        }

        // Agrega un movimiento de inventario y actualiza el stock correspondiente en SQL
        public static bool AgregarMovimiento(MovimientoInventario m)
        {
            Producto? prod = ObtenerPorId(m.IdProducto);
            if (prod == null) return false;

            // Validar que no saquemos mas de lo que hay en inventario
            if (m.Tipo == "Salida" && m.Cantidad > prod.StockActual)
            {
                return false;
            }

            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                // 1. Insertamos el movimiento
                string queryMov = @"INSERT INTO dbo.MovimientoInventario (id_producto, tipo, cantidad, fecha, descripcion)
                                    VALUES (@idProd, @tipo, @cant, @fecha, @desc);
                                    SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmdMov = new SqlCommand(queryMov, con))
                {
                    cmdMov.Parameters.AddWithValue("@idProd", m.IdProducto);
                    cmdMov.Parameters.AddWithValue("@tipo", m.Tipo);
                    cmdMov.Parameters.AddWithValue("@cant", m.Cantidad);
                    cmdMov.Parameters.AddWithValue("@fecha", m.Fecha);
                    cmdMov.Parameters.AddWithValue("@desc", (object?)m.Descripcion ?? DBNull.Value);

                    object idGen = cmdMov.ExecuteScalar();
                    if (idGen != null && idGen != DBNull.Value)
                    {
                        m.IdMovimiento = Convert.ToInt32(idGen);
                    }
                }

                // 2. Calculamos y actualizamos el stock del producto
                int nuevoStock = prod.StockActual;
                if (m.Tipo == "Entrada")
                {
                    nuevoStock += m.Cantidad;
                }
                else if (m.Tipo == "Salida")
                {
                    nuevoStock -= m.Cantidad;
                }
                else // Ajuste directo
                {
                    nuevoStock = m.Cantidad;
                }

                string queryStock = "UPDATE dbo.Producto SET stock_actual = @stock WHERE id_producto = @idProd";
                using (SqlCommand cmdStock = new SqlCommand(queryStock, con))
                {
                    cmdStock.Parameters.AddWithValue("@stock", nuevoStock);
                    cmdStock.Parameters.AddWithValue("@idProd", m.IdProducto);
                    cmdStock.ExecuteNonQuery();
                }
            }

            return true;
        }

        // Lista el historial de movimientos de inventario
        public static List<MovimientoInventario> ListarMovimientos()
        {
            List<MovimientoInventario> lista = new List<MovimientoInventario>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = @"SELECT id_movimiento, id_producto, tipo, cantidad, fecha, descripcion 
                                 FROM dbo.MovimientoInventario 
                                 ORDER BY fecha DESC, id_movimiento DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new MovimientoInventario
                            {
                                IdMovimiento = Convert.ToInt32(dr["id_movimiento"]),
                                IdProducto = Convert.ToInt32(dr["id_producto"]),
                                Tipo = dr["tipo"].ToString()!,
                                Cantidad = Convert.ToInt32(dr["cantidad"]),
                                Fecha = Convert.ToDateTime(dr["fecha"]),
                                Descripcion = dr["descripcion"] != DBNull.Value ? dr["descripcion"].ToString()! : ""
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // ==========================================
        // GESTION DE CATEGORIAS

        public static List<CategoriaProducto> ListarCategorias()
        {
            List<CategoriaProducto> lista = new List<CategoriaProducto>();
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "SELECT id_categoria, nombre, descripcion FROM dbo.CategoriaProducto ORDER BY id_categoria";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new CategoriaProducto
                            {
                                IdCategoria = Convert.ToInt32(dr["id_categoria"]),
                                Nombre = dr["nombre"].ToString()!,
                                Descripcion = dr["descripcion"] != DBNull.Value ? dr["descripcion"].ToString()! : ""
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public static void AgregarCategoria(CategoriaProducto c)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                // Obtenemos el maximo ID para asignar el siguiente
                int siguienteId = 1;
                using (SqlCommand cmdMax = new SqlCommand("SELECT ISNULL(MAX(id_categoria), 0) + 1 FROM dbo.CategoriaProducto", con))
                {
                    siguienteId = Convert.ToInt32(cmdMax.ExecuteScalar());
                }

                c.IdCategoria = siguienteId;
                string query = "INSERT INTO dbo.CategoriaProducto (id_categoria, nombre, descripcion) VALUES (@id, @nombre, @desc)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", c.IdCategoria);
                    cmd.Parameters.AddWithValue("@nombre", c.Nombre);
                    cmd.Parameters.AddWithValue("@desc", (object?)c.Descripcion ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ModificarCategoria(CategoriaProducto c)
        {
            using (SqlConnection con = ConexionDB.ObtenerConexion())
            {
                string query = "UPDATE dbo.CategoriaProducto SET nombre = @nombre, descripcion = @desc WHERE id_categoria = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", c.IdCategoria);
                    cmd.Parameters.AddWithValue("@nombre", c.Nombre);
                    cmd.Parameters.AddWithValue("@desc", (object?)c.Descripcion ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Metodo de ayuda para mapear fila a objeto Producto
        private static Producto MapearProducto(SqlDataReader dr)
        {
            return new Producto
            {
                IdProducto = Convert.ToInt32(dr["id_producto"]),
                Codigo = dr["codigo"].ToString()!,
                Nombre = dr["nombre"].ToString()!,
                IdCategoria = Convert.ToInt32(dr["id_categoria"]),
                StockActual = Convert.ToInt32(dr["stock_actual"]),
                StockMinimo = Convert.ToInt32(dr["stock_minimo"]),
                PrecioCompra = Convert.ToDecimal(dr["precio_compra"]),
                FechaVencimiento = dr["fecha_vencimiento"] != DBNull.Value ? Convert.ToDateTime(dr["fecha_vencimiento"]) : DateTime.Today.AddYears(1),
                UnidadesPorCaja = Convert.ToInt32(dr["unidades_por_caja"]),
                PrecioVenta = Convert.ToDecimal(dr["precio_venta"])
            };
        }
    }
}
