using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Agregador
{
    public class DAOProductos
    {
        SqlConnection conBd = null;
        #region BD
        Conexion objCon = new Conexion();

        private bool conectar()
        {
            this.conBd = objCon.conectar();
            if (this.conBd != null)
            {
                if (this.conBd.State == ConnectionState.Open)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        private void desconectar()
        {
            this.objCon.desconectar();
        }

        #endregion

        public void listarProductos(ref DataTable dtProductos, int idEstado, int idCuenta, string termino)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.ListarProductos");
                    objCon.cmdApp.Parameters.AddWithValue("@idEstado", idEstado);
                    objCon.cmdApp.Parameters.AddWithValue("@account_id", idCuenta);
                    objCon.cmdApp.Parameters.AddWithValue("@termino", termino);
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtProductos);
                }
            }
            catch (Exception er)
            {
                logErrorApp("listPrd", er.Message);
            }
            finally
            { desconectar(); }
        }

        public void listarProductosXEstado(ref DataTable dtProductos, int idUsuario)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.listarProductosXEstado");
                    objCon.cmdApp.Parameters.AddWithValue("@codUsuario", idUsuario);
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtProductos);
                }
            }
            catch (Exception er)
            {
                logErrorApp("listPrdEst", er.Message);
            }
            finally
            { desconectar(); }
        }

        public void listarDetalleProducto(ref DataTable dtProducto, int idProducto, bool marketplace)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.listarDetalleProducto");
                    objCon.cmdApp.Parameters.AddWithValue("@id", idProducto);
                    objCon.cmdApp.Parameters.AddWithValue("@mp", marketplace);
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtProducto);
                }
            }
            catch (Exception er)
            {
                logErrorApp("listDetPrd", er.Message);
            }
            finally
            { desconectar(); }
        }

        public void listarDetalleProductoCategoria(ref DataTable dtProducto, string categorias, string marketplace)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.listarDetalleProductoPorCategoria");
                    objCon.cmdApp.Parameters.AddWithValue("@mp", marketplace);
                    objCon.cmdApp.Parameters.AddWithValue("@id", categorias);
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtProducto);
                }
            }
            catch (Exception er)
            {
                logErrorApp("listDetPrdCat", er.Message);
            }
            finally
            { desconectar(); }
        }

        public void listarCategoriasProducto(ref DataTable dtCategorias, int idCategoriaProd)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.listarCategoriasProducto");
                    objCon.cmdApp.Parameters.AddWithValue("@idCategoriaProducto", idCategoriaProd);
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtCategorias);
                }
            }
            catch (Exception er)
            {
                logErrorApp("listCatPrd", er.Message);
            }
            finally
            { desconectar(); }
        }

        public void listarCategoriasMP(ref DataTable dtCategorias, string nombre, int codUsuario)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.listarCategoriasMP");
                    objCon.cmdApp.Parameters.AddWithValue("@name", nombre);
                    objCon.cmdApp.Parameters.AddWithValue("@codUsuario", codUsuario);
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtCategorias);
                }
            }
            catch (Exception er)
            {
                logErrorApp("lstCatMP", er.Message);
            }
            finally
            { desconectar(); }
        }

        public void listarSubCategoriasMP(ref DataTable dtCategorias, int parentID, int codUsuario)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.listarSubCategoriasMP");
                    objCon.cmdApp.Parameters.AddWithValue("@parentId", parentID);
                    objCon.cmdApp.Parameters.AddWithValue("@codUsuario", codUsuario);
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtCategorias);
                }
            }
            catch (Exception er)
            {
                logErrorApp("listSubCatMP", er.Message);
            }
            finally
            { desconectar(); }
        }
        public void listarEstados(ref DataTable dtEstados)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.listarEstadosProductos", ref dtEstados);
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtEstados);
                }
            }
            catch (Exception er)
            {
                logErrorApp("listEst", er.Message);
            }
            finally
            { desconectar(); }
        }

        public int obtenerIdMP()
        {
            int id = 0;
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.obtenerIdMP");
                    int.TryParse(objCon.cmdApp.ExecuteScalar().ToString(), out id);
                }
            }
            catch (Exception er)
            {
                logErrorApp("idMP", er.Message);
            }
            finally
            { desconectar(); }
            return id;
        }

        public int obtenerIdOfertas()
        {
            int id = 0;
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.obtenerIdCatOfertasMP");
                    int.TryParse(objCon.cmdApp.ExecuteScalar().ToString(), out id);
                }
            }
            catch (Exception er)
            {
                logErrorApp("idOfertas", er.Message);
            }
            finally
            { desconectar(); }
            return id;
        }
        public string obtenerCuentaMP()
        {
            string nombre = string.Empty;
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.obtenerCuentaMP");
                    nombre = objCon.cmdApp.ExecuteScalar().ToString();
                }
            }
            catch (Exception er)
            {
                logErrorApp("ctaMP", er.Message);
            }
            finally
            { desconectar(); }
            return nombre;
        }

        public bool actualizarEstadoProducto(int idProducto, int idEstado)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.cambiarEstadoProducto");
                    objCon.cmdApp.Parameters.AddWithValue("@id", idProducto);
                    objCon.cmdApp.Parameters.AddWithValue("@nuevoEstado", idEstado);
                    objCon.cmdApp.ExecuteNonQuery();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                logErrorApp("ActEstPrd", ex.Message);
                return false;
            }
            finally
            { desconectar(); }
        }
        public bool agregarProductoMP(string cuentaOrigen, string productoOrigen, string cuentaDestino, string productoDestino, string usuario)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.agregarProductoMP");
                    objCon.cmdApp.Parameters.AddWithValue("@cuentaOrigen", cuentaOrigen);
                    objCon.cmdApp.Parameters.AddWithValue("@productoOrigen", productoOrigen);
                    objCon.cmdApp.Parameters.AddWithValue("@cuentaDestino", cuentaDestino);
                    objCon.cmdApp.Parameters.AddWithValue("@productoDestino", productoDestino);
                    objCon.cmdApp.Parameters.AddWithValue("@usuario", usuario);
                    objCon.cmdApp.ExecuteNonQuery();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                logErrorApp("AggPrdMp", ex.Message);
                return false;
            }
            finally
            { desconectar(); }
        }
        public bool crearLoteOferta(string fechaInicio, string fechaFinal, string usuario, string idsproductos)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.guardarOferta");
                    objCon.cmdApp.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    objCon.cmdApp.Parameters.AddWithValue("@fechafinal", fechaFinal);
                    objCon.cmdApp.Parameters.AddWithValue("@userId", usuario);
                    objCon.cmdApp.Parameters.AddWithValue("@idsproductos", idsproductos);
                    objCon.cmdApp.ExecuteNonQuery();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                logErrorApp("CrearLoteOff", ex.Message);
                return false;
            }
            finally
            { desconectar(); }
        }
        public void logErrorApp(string codigo, string msj)
        {
            this.conectar();
            objCon.configurarComando("Logs.GuardarError");
            objCon.cmdApp.Parameters.AddWithValue("@codigo", codigo);
            objCon.cmdApp.Parameters.AddWithValue("@mensaje", msj);
            objCon.cmdApp.ExecuteNonQuery();
        }
    }
}