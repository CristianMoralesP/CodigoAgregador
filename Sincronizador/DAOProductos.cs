using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Sincronizador
{
    class DAOProductos
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
        public void sincronizarProductos()
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.sincronizarProductos");
                    objCon.cmdApp.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                logErrorApp("syncPrd", e.Message);
            }
            finally
            { desconectar(); }
        }
        public bool CamilyoguardarInfoProducto(Producto producto, String idCuenta)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("Camilyo.GuardarInfoProducto");
                    objCon.cmdApp.Parameters.AddWithValue("@id", producto.id);
                    objCon.cmdApp.Parameters.AddWithValue("@categoryIds", producto.categoryIds);
                    objCon.cmdApp.Parameters.AddWithValue("@categoryForeignIds", producto.categoryForeignIds);
                    objCon.cmdApp.Parameters.AddWithValue("@name", producto.name);
                    objCon.cmdApp.Parameters.AddWithValue("@foreignId", producto.foreignId);
                    objCon.cmdApp.Parameters.AddWithValue("@mainImageUrl", producto.mainImageUrl);
                    objCon.cmdApp.Parameters.AddWithValue("@mainImageUrlExternal", producto.mainImageUrlExternal);
                    objCon.cmdApp.Parameters.AddWithValue("@pageImageURL", producto.pageImageURL);
                    objCon.cmdApp.Parameters.AddWithValue("@pageImageURLExternal", producto.pageImageURLExternal);
                    objCon.cmdApp.Parameters.AddWithValue("@shortDescription", producto.shortDescription);
                    objCon.cmdApp.Parameters.AddWithValue("@description", producto.description);
                    objCon.cmdApp.Parameters.AddWithValue("@catalogNumber", producto.catalogNumber);
                    objCon.cmdApp.Parameters.AddWithValue("@gtin", producto.gtin);
                    objCon.cmdApp.Parameters.AddWithValue("@model", producto.model);
                    objCon.cmdApp.Parameters.AddWithValue("@brandId", producto.brandId);
                    objCon.cmdApp.Parameters.AddWithValue("@brandName", producto.brandName);
                    objCon.cmdApp.Parameters.AddWithValue("@listPrice", producto.listPrice);
                    objCon.cmdApp.Parameters.AddWithValue("@price", producto.price);
                    objCon.cmdApp.Parameters.AddWithValue("@costPrice", producto.costPrice);
                    objCon.cmdApp.Parameters.AddWithValue("@currencyName", producto.currencyName);
                    objCon.cmdApp.Parameters.AddWithValue("@deliveryTime", producto.deliveryTime);
                    objCon.cmdApp.Parameters.AddWithValue("@inventory", producto.inventory);
                    objCon.cmdApp.Parameters.AddWithValue("@warranty", producto.warranty);
                    objCon.cmdApp.Parameters.AddWithValue("@isOnline", producto.isOnline);
                    objCon.cmdApp.Parameters.AddWithValue("@updated", producto.updated);
                    objCon.cmdApp.Parameters.AddWithValue("@account_id", idCuenta);
                    objCon.cmdApp.Parameters.AddWithValue("@discountLabel", producto.discountLabel);
                    objCon.cmdApp.ExecuteNonQuery();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                logErrorApp("StInfoPrd", e.Message);
                return false;
            }
            finally
            { desconectar(); }
        }

        public void ProductosEliminados(ref DataTable dtProductosEliminados)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.listarProductosEliminados");
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtProductosEliminados);
                }
            }
            catch (Exception er)
            {
                logErrorApp("prdElim", er.Message);
            }
            finally
            { desconectar(); }
        }

        public void ProductosSinInventario(ref DataTable dtProductosSinInventario)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.listarProductosSinInventario");
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtProductosSinInventario);
                }
            }
            catch (Exception er)
            {
                logErrorApp("prdNoInv", er.Message);
            }
            finally
            { desconectar(); }
        }

        public void ProductosConInventario(ref DataTable dtProductosConInventario)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.listarProductosConInventario");
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtProductosConInventario);
                }
            }
            catch (Exception er)
            {
                logErrorApp("prdnvoInv", er.Message);
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
