using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Sincronizador
{
    class DAOCategorias
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
        public void sincronizarCategorias()
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.sincronizarCategorias");
                    objCon.cmdApp.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                logErrorApp("syncCat", e.Message);
            }
            finally
            { desconectar(); }
        }

        public void sincronizarSubCategorias()
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.sincronizarSubCategorias");
                    objCon.cmdApp.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                logErrorApp("syncSubCat", e.Message);
            }
            finally
            { desconectar(); }
        }
        public bool CamilyoguardarInfoCategoria(Categoria categoria, String idCuenta)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("Camilyo.GuardarInfoCategoria");
                    objCon.cmdApp.Parameters.AddWithValue("@id", categoria.id);
                    objCon.cmdApp.Parameters.AddWithValue("@foreignId", categoria.foreignId);
                    objCon.cmdApp.Parameters.AddWithValue("@name", categoria.name);
                    objCon.cmdApp.Parameters.AddWithValue("@imageUrl", categoria.imageUrl);
                    objCon.cmdApp.Parameters.AddWithValue("@shortDescription", categoria.shortDescription);
                    objCon.cmdApp.Parameters.AddWithValue("@isOnline", categoria.isOnline);
                    objCon.cmdApp.Parameters.AddWithValue("@parentId", categoria.parentId);
                    objCon.cmdApp.Parameters.AddWithValue("@account_id", idCuenta);
                    objCon.cmdApp.ExecuteNonQuery();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                logErrorApp("StInfoCat", e.Message);
                return false;
            }
            finally
            { desconectar(); }
        }

        public bool CamilyoguardarInfoSubCategoria(SubCategoria subCategoria)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("Camilyo.GuardarInfoSubCategoria");
                    objCon.cmdApp.Parameters.AddWithValue("@id", subCategoria.id);
                    objCon.cmdApp.Parameters.AddWithValue("@name", subCategoria.name);
                    objCon.cmdApp.Parameters.AddWithValue("@parentId", subCategoria.parentId);
                    objCon.cmdApp.Parameters.AddWithValue("@isOnline", subCategoria.isOnline);
                    objCon.cmdApp.ExecuteNonQuery();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                logErrorApp("StInfoSubCat", e.Message);
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
