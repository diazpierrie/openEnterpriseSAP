﻿using EE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace DAL
{
    public class VentaDal : ConnectionDal
    {
        private readonly CompradorDal _compradorDal = new CompradorDal();
        private readonly MetodoPagoDal _metodoPago = new MetodoPagoDal();
        private readonly ProductoDal _productoDal = new ProductoDal();
        private readonly SucursalDal _sucursalDal = new SucursalDal();
        private readonly UsuarioDal _usuarioDal = new UsuarioDal();
        private readonly VentaEstadoDal _ventaEstadoDal = new VentaEstadoDal();

        public bool CambiarEstadoAPagado(VentaEe venta)
        {
            try
            {
                const string strQuery = "UPDATE[dbo].[venta] " +
                                        "SET[idEstado] = 2 " +
                                        "WHERE id = @id";

                var query = new SqlCommand(strQuery, Conn);

                query.Parameters.AddWithValue("@id", venta.Id);

                return ExecuteQuery(query);
            }
            catch (Exception e)
            {
                ErrorManagerDal.AgregarMensaje(e.ToString());
                return false;
            }
        }

        public bool Cancelar(VentaEe venta)
        {
            try
            {
                const string strQuery = "UPDATE[dbo].[venta] " +
                                        "SET[idEstado] = 3 " +
                                        "WHERE id = @id";

                var query = new SqlCommand(strQuery, Conn);

                query.Parameters.AddWithValue("@id", venta.Id);

                return ExecuteQuery(query);
            }
            catch (Exception e)
            {
                ErrorManagerDal.AgregarMensaje(e.ToString());
                return false;
            }
        }

        public int Crear(VentaEe obj)
        {
            var columnas = new List<string> { "idUsuario", "idComprador", "idSucursal", "idMetodoPago", "idTipoEntrega", "idEstado", "fecha", "total" };
            var valores = new List<string> { obj.Empleado.Id.ToString(), obj.Comprador.Id.ToString(), obj.Sucursal.Id.ToString(), obj.MetodoPago.Id.ToString(), obj.TipoEntrega.Id.ToString(), obj.Estado.Id.ToString(), DateTime.Today.ToString(CultureInfo.InvariantCulture), obj.Total.ToString(CultureInfo.InvariantCulture) };

            return Insert("venta", columnas.ToArray(), valores.ToArray());
        }

        public int CrearDetalle(VentaEe venta, ProductoEdificioEe producto)
        {
            var columnas = new List<string> { "idVenta", "idProducto", "costoUnitario", "precioUnitario", "cantidad" };
            var valores = new[] { venta.Id.ToString(), producto.Id.ToString(), producto.Costo.ToString(CultureInfo.InvariantCulture),
                producto.Precio.ToString(CultureInfo.InvariantCulture), producto.CantidadAComprar.ToString() };

            return Insert("venta_detalle", columnas.ToArray(), valores);
        }

        public bool MarcarVentaComoPerdida(VentaEe venta)
        {
            var query = new SqlCommand("UPDATE venta SET idEstado = @idEstado WHERE id = @id", Conn);
            query.Parameters.AddWithValue("@id", venta.Id);
            query.Parameters.AddWithValue("@idEstado", 4);

            return ExecuteQuery(query);
        }

        public bool MarcarVentaConDevoluciones(VentaEe venta)
        {
            var query = new SqlCommand("UPDATE venta SET idEstado = @idEstado WHERE id = @id", Conn);
            query.Parameters.AddWithValue("@id", venta.Id);
            query.Parameters.AddWithValue("@idEstado", venta.Estado.Id == 4 ? 8 : 5);

            return ExecuteQuery(query);
        }

        public bool MarcarVentaConPerdidasYDevoluciones(VentaEe venta)
        {
            var query = new SqlCommand("UPDATE venta SET idEstado = @idEstado WHERE id = @id", Conn);
            query.Parameters.AddWithValue("@id", venta.Id);
            query.Parameters.AddWithValue("@idEstado", 8);

            return ExecuteQuery(query);
        }

        public bool MarcarVentaDevuelta(VentaEe venta)
        {
            var query = new SqlCommand("UPDATE venta SET idEstado = @idEstado WHERE id = @id", Conn);
            query.Parameters.AddWithValue("@id", venta.Id);
            query.Parameters.AddWithValue("@idEstado", 6);

            return ExecuteQuery(query);
        }

        public bool MarcarVentaDevueltoConPerdidas(VentaEe venta)
        {
            var query = new SqlCommand("UPDATE venta SET idEstado = @idEstado WHERE id = @id", Conn);
            query.Parameters.AddWithValue("@id", venta.Id);
            query.Parameters.AddWithValue("@idEstado", 9);

            return ExecuteQuery(query);
        }

        public VentaEe Obtener(int id)
        {
            try
            {
                var strQuery = "SELECT" +
                               " id," +
                               " idUsuario," +
                               " idComprador," +
                               " idSucursal," +
                               " idMetodoPago," +
                               " idEstado," +
                               " total," +
                               " fecha FROM venta" +
                               $" WHERE id = {id}";

                var query = new SqlCommand(strQuery, Conn);

                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.Open();
                }

                var data = query.ExecuteReader();
                VentaEe venta = null;

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        venta = CastDto(data);
                    }
                }

                Conn.Close();
                return venta;
            }
            catch (Exception e)
            {
                ErrorManagerDal.AgregarMensaje(e.ToString());
                return null;
            }
        }

        public List<VentaEe> Obtener()
        {
            try
            {
                const string strQuery = "SELECT" +
                                        " id," +
                                        " idUsuario," +
                                        " idComprador," +
                                        " idSucursal," +
                                        " idMetodoPago," +
                                        " idEstado," +
                                        " total," +
                                        " fecha FROM venta";

                var query = new SqlCommand(strQuery, Conn);

                Conn.Open();
                var data = query.ExecuteReader();
                var ventas = new List<VentaEe>();

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        ventas.Add(CastDto(data));
                    }
                }

                Conn.Close();
                return ventas;
            }
            catch (Exception e)
            {
                ErrorManagerDal.AgregarMensaje(e.ToString());
                return new List<VentaEe>();
            }
        }

        public List<VentaEe> Obtener(UsuarioEe user)
        {
            try
            {
                var strQuery = "SELECT" +
                               " id," +
                               " idUsuario," +
                               " idComprador," +
                               " idSucursal," +
                               " idMetodoPago," +
                               " idEstado," +
                               " total," +
                               " fecha FROM venta" +
                               $" WHERE v.idUsuario = {user.Id}";

                var query = new SqlCommand(strQuery, Conn);

                Conn.Open();
                var data = query.ExecuteReader();
                var ventas = new List<VentaEe>();

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        ventas.Add(CastDto(data));
                    }
                }

                Conn.Close();
                return ventas;
            }
            catch (Exception e)
            {
                ErrorManagerDal.AgregarMensaje(e.ToString());
                return new List<VentaEe>();
            }
        }

        public List<VentaEe> Obtener(CompradorEe comprador)
        {
            try
            {
                var strQuery = "SELECT" +
                               " id," +
                               " idUsuario," +
                               " idComprador," +
                               " idSucursal," +
                               " idMetodoPago," +
                               " idEstado," +
                               " total," +
                               " fecha FROM venta" +
                               $" WHERE v.idUsuario = {comprador.Id}";

                var query = new SqlCommand(strQuery, Conn);

                Conn.Open();
                var data = query.ExecuteReader();
                var sucus = new List<VentaEe>();

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        sucus.Add(CastDto(data));
                    }
                }

                Conn.Close();
                return sucus;
            }
            catch (Exception e)
            {
                ErrorManagerDal.AgregarMensaje(e.ToString());
                return new List<VentaEe>();
            }
        }

        public List<VentaEe> Obtener(SucursalEe sucursal)
        {
            try
            {
                var strQuery = "SELECT" +
                               " id," +
                               " idUsuario," +
                               " idComprador," +
                               " idSucursal," +
                               " idMetodoPago," +
                               " idEstado," +
                               " total," +
                               " fecha FROM venta" +
                               $" WHERE idSucursal = {sucursal.Id}";

                var query = new SqlCommand(strQuery, Conn);

                Conn.Open();
                var data = query.ExecuteReader();
                var sucus = new List<VentaEe>();

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        sucus.Add(CastDto(data));
                    }
                }

                Conn.Close();
                return sucus;
            }
            catch (Exception e)
            {
                ErrorManagerDal.AgregarMensaje(e.ToString());
                return new List<VentaEe>();
            }
        }

        public VentaDetalleEe ObtenerDetalle(int id)
        {
            try
            {
                var strQuery = "SELECT" +
                               " id," +
                               " idVenta," +
                               " idProducto," +
                               " costoUnitario," +
                               " precioUnitario," +
                               " cantidad" +
                               " FROM venta_detalle" +
                               $" WHERE id = {id}";

                var query = new SqlCommand(strQuery, Conn);

                Conn.Open();
                var data = query.ExecuteReader();
                var detalle = new VentaDetalleEe();

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        detalle = CastDtoDetalle(data);
                    }
                }

                Conn.Close();
                return detalle;
            }
            catch (Exception e)
            {
                ErrorManagerDal.AgregarMensaje(e.ToString());
                return null;
            }
        }

        public List<VentaDetalleEe> ObtenerDetalles(int id)
        {
            try
            {
                var strQuery = "SELECT" +
                               " id," +
                               " idVenta," +
                               " idProducto," +
                               " costoUnitario," +
                               " precioUnitario," +
                               " cantidad" +
                               " FROM venta_detalle" +
                               $" WHERE idVenta = {id}";

                var query = new SqlCommand(strQuery, Conn);

                Conn.Open();
                var data = query.ExecuteReader();
                var detalles = new List<VentaDetalleEe>();

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        detalles.Add(CastDtoDetalle(data));
                    }
                }

                Conn.Close();
                return detalles;
            }
            catch (Exception e)
            {
                ErrorManagerDal.AgregarMensaje(e.ToString());
                return new List<VentaDetalleEe>();
            }
        }

        public List<VentaDetalleEe> ObtenerDetallesAgrupados(int id)
        {
            try
            {
                var strQuery = $"SELECT vd.idProducto, costoUnitario, SUM(vd.cantidad) AS Cantidad FROM venta_detalle as vd WHERE vd.idVenta = {id} AND cantidad != 0 GROUP BY vd.idProducto, costoUnitario";

                var query = new SqlCommand(strQuery, Conn);

                Conn.Open();
                var data = query.ExecuteReader();
                var detalles = new List<VentaDetalleEe>();

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        detalles.Add(CastDtoDetalleAgrupado(data));
                    }
                }

                Conn.Close();
                return detalles;
            }
            catch (Exception e)
            {
                ErrorManagerDal.AgregarMensaje(e.ToString());
                return new List<VentaDetalleEe>();
            }
        }

        public List<VentaDetalleEe> ObtenerDevolucionesAgrupadas(int id)
        {
            try
            {
                var strQuery = $"SELECT dd.idProducto, SUM(dd.cantidad) AS Cantidad FROM  devolucion_detalle as dd, (SELECT id FROM devolucion WHERE idVenta = {id}) as d WHERE dd.idDevolucion = d.id GROUP BY dd.idProducto";

                var query = new SqlCommand(strQuery, Conn);

                Conn.Open();
                var data = query.ExecuteReader();
                var detalles = new List<VentaDetalleEe>();

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        detalles.Add(CastDtoDevolucionOPerdidaAgrupada(data));
                    }
                }

                Conn.Close();
                return detalles;
            }
            catch (Exception e)
            {
                ErrorManagerDal.AgregarMensaje(e.ToString());
                return new List<VentaDetalleEe>();
            }
        }

        public List<VentaEe> ObtenerPendienteDePago(SucursalEe sucursal)
        {
            try
            {
                var strQuery = "SELECT" +
                               " id," +
                               " idUsuario," +
                               " idComprador," +
                               " idSucursal," +
                               " idMetodoPago," +
                               " idEstado," +
                               " total," +
                               " fecha FROM venta" +
                               $" WHERE idSucursal = {sucursal.Id}" +
                               " AND idEstado = 1";

                var query = new SqlCommand(strQuery, Conn);

                Conn.Open();
                var data = query.ExecuteReader();
                var sucus = new List<VentaEe>();

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        sucus.Add(CastDto(data));
                    }
                }

                Conn.Close();
                return sucus;
            }
            catch (Exception e)
            {
                ErrorManagerDal.AgregarMensaje(e.ToString());
                return new List<VentaEe>();
            }
        }

        public List<VentaDetalleEe> ObtenerPerdidasAgrupadas(int id)
        {
            try
            {
                var strQuery = $"SELECT pd.idProducto, SUM(pd.cantidad) AS Cantidad FROM  perdida_detalle as pd, (SELECT id FROM perdida WHERE idVenta = {id}) as d WHERE pd.idPerdida = d.id GROUP BY pd.idProducto";

                var query = new SqlCommand(strQuery, Conn);

                Conn.Open();
                var data = query.ExecuteReader();
                var detalles = new List<VentaDetalleEe>();

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        detalles.Add(CastDtoDevolucionOPerdidaAgrupada(data));
                    }
                }

                Conn.Close();
                return detalles;
            }
            catch (Exception e)
            {
                ErrorManagerDal.AgregarMensaje(e.ToString());
                return new List<VentaDetalleEe>();
            }
        }

        public bool ReducirStockDeposito(ProductoEdificioEe producto)
        {
            var query = new SqlCommand("UPDATE [dbo].[deposito_producto] SET [stock] = stock - @stock WHERE [idDeposito] = @idDeposito AND [idProducto] = @idProducto", Conn);
            query.Parameters.AddWithValue("@idDeposito", producto.Edificio.Id);
            query.Parameters.AddWithValue("@idProducto", producto.Id);
            query.Parameters.AddWithValue("@stock", producto.CantidadAComprar);

            return ExecuteQuery(query);
        }

        public bool ReducirStockSucursal(ProductoEdificioEe producto)
        {
            var query = new SqlCommand("UPDATE [dbo].[sucursal_producto] SET [stock] = stock - @stock WHERE [idSucursal] = @idSucursal AND [idProducto] = @idProducto", Conn);
            query.Parameters.AddWithValue("@idSucursal", producto.Edificio.Id);
            query.Parameters.AddWithValue("@idProducto", producto.Id);
            query.Parameters.AddWithValue("@stock", producto.CantidadAComprar);

            return ExecuteQuery(query);
        }

        public int RegistrarDetallesDevolucion(DevolucionEe devolucion, List<VentaDetalleEe> productos)
        {
            var columnas = new List<string> { "idDevolucion", "idProducto", "cantidad" };
            var valores = new List<string[]>();
            foreach (var producto in productos)
            {
                string[] value =
                {
                    devolucion.Id.ToString(),
                    producto.Producto.Id.ToString(),
                    producto.Cantidad.ToString()
                };
                valores.Add(value);
            }
            return Insert("devolucion_detalle", columnas.ToArray(), valores);
        }

        public int RegistrarDetallesPerdida(PerdidaEe perdida, List<VentaDetalleEe> productos)
        {
            var columnas = new List<string> { "idPerdida", "idProducto", "costoUnitario", "cantidad" };
            var valores = new List<string[]>();
            foreach (var producto in productos)
            {
                string[] value =
                {
                    perdida.Id.ToString(),
                    producto.Producto.Id.ToString(),
                    producto.Costo.ToString(CultureInfo.InvariantCulture),
                    producto.Cantidad.ToString()
                };
                valores.Add(value);
            }
            return Insert("perdida_detalle", columnas.ToArray(), valores);
        }

        public int RegistrarDevolucion(VentaEe obj)
        {
            var columnas = new List<string> { "idSucursal", "idVenta", "idUsuario", "fecha" };
            var valores = new List<string>
            {
                obj.Sucursal.Id.ToString(),
                obj.Id.ToString(),
                obj.Empleado.Id.ToString(),
                DateTime.Today.ToString(CultureInfo.InvariantCulture)
            };

            return Insert("devolucion", columnas.ToArray(), valores.ToArray());
        }

        public int RegistrarPago(PagoEe pago)
        {
            var columnas = new List<string> { "idVenta", "montoCobrado", "vuelto" };
            var valores = new List<string> { pago.Venta.Id.ToString(), pago.MontoCobrado.ToString(CultureInfo.InvariantCulture), pago.Vuelto.ToString(CultureInfo.InvariantCulture) };

            return Insert("pago", columnas.ToArray(), valores.ToArray());
        }

        public int RegistrarPerdida(VentaEe obj, double total)
        {
            var columnas = new List<string> { "idSucursal", "idVenta", "idUsuario", "fecha", "total" };
            var valores = new List<string>
            {
                obj.Sucursal.Id.ToString(),
                obj.Id.ToString(),
                obj.Empleado.Id.ToString(),
                DateTime.Today.ToString(CultureInfo.InvariantCulture),
                total.ToString(CultureInfo.InvariantCulture)
            };

            return Insert("perdida", columnas.ToArray(), valores.ToArray());
        }

        public void RegistrarProductosDevueltos(List<VentaDetalleEe> productos)
        {
            foreach (var producto in productos)
            {
                if (_productoDal.ObtenerPorSucursal(Sesion.ObtenerSesion().Sucursal).Any(x => x.Id == producto.Producto.Id))
                {
                    _productoDal.ActualizarStockSucursal(Sesion.ObtenerSesion().Sucursal, producto);
                }
                else
                {
                    _productoDal.AgregarProductoSucursal(Sesion.ObtenerSesion().Sucursal, producto);
                }
            }
        }

        private VentaEe CastDto(SqlDataReader data)
        {
            return new VentaEe
            {
                Id = int.Parse(data["id"].ToString()),
                Empleado = _usuarioDal.Obtener(int.Parse(data["idUsuario"].ToString())),
                Comprador = _compradorDal.Obtener(int.Parse(data["idComprador"].ToString())),
                Sucursal = _sucursalDal.Obtener(int.Parse(data["idSucursal"].ToString())),
                MetodoPago = _metodoPago.Obtener(int.Parse(data["idMetodoPago"].ToString())),
                Estado = _ventaEstadoDal.Obtener(int.Parse(data["idEstado"].ToString())),
                Fecha = DateTime.Parse(data["fecha"].ToString()),
                Total = double.Parse(data["total"].ToString())
            };
        }

        private static VentaDetalleEe CastDtoDetalle(SqlDataReader data)
        {
            return new VentaDetalleEe
            {
                Id = int.Parse(data["id"].ToString()),
                Venta = new VentaEe() { Id = int.Parse(data["idVenta"].ToString()) },
                Producto = new ProductoEe() { Id = int.Parse(data["idProducto"].ToString()) },
                //Venta = Obtener(int.Parse(data["idVenta"].ToString())),
                //Producto = _productoDal.Obtener(int.Parse(data["idProducto"].ToString())),
                Costo = double.Parse(data["costoUnitario"].ToString()),
                Precio = double.Parse(data["precioUnitario"].ToString()),
                Cantidad = int.Parse(data["cantidad"].ToString())
            };
        }

        private static VentaDetalleEe CastDtoDetalleAgrupado(SqlDataReader data)
        {
            return new VentaDetalleEe
            {
                Producto = new ProductoEe() { Id = int.Parse(data["idProducto"].ToString()) },
                Costo = double.Parse(data["costoUnitario"].ToString()),
                Cantidad = int.Parse(data["cantidad"].ToString())
            };
        }

        private static VentaDetalleEe CastDtoDevolucionOPerdidaAgrupada(SqlDataReader data)
        {
            return new VentaDetalleEe
            {
                Producto = new ProductoEe() { Id = int.Parse(data["idProducto"].ToString()) },
                Cantidad = int.Parse(data["cantidad"].ToString())
            };
        }
    }
}