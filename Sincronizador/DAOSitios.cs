using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace Sincronizador
{
    class DAOSitios
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
        public void sincronizarSitios()
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.sincronizarSitios");
                    objCon.cmdApp.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                logErrorApp("syncSit", e.Message);
            }
            finally
            { desconectar(); }
        }
        public bool CamilyoguardarInfoSitio(Sitio sitio)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("Camilyo.GuardarInfoSitio");
                    objCon.cmdApp.Parameters.AddWithValue("@id", sitio.id);
                    objCon.cmdApp.Parameters.AddWithValue("@domain", sitio.domain);
                    objCon.cmdApp.Parameters.AddWithValue("@sitename", sitio.sitename);
                    objCon.cmdApp.Parameters.AddWithValue("@is_active", sitio.is_active);
                    objCon.cmdApp.Parameters.AddWithValue("@is_prod_active", sitio.is_prod_active);
                    objCon.cmdApp.Parameters.AddWithValue("@displaysiteurl", sitio.displaysiteurl);
                    objCon.cmdApp.Parameters.AddWithValue("@last_update_time_iso_str", sitio.last_update_time_iso_str);
                    objCon.cmdApp.Parameters.AddWithValue("@creation_time_iso_str", sitio.creation_time_iso_str);
                    objCon.cmdApp.Parameters.AddWithValue("@account_id", sitio.account_id);
                    objCon.cmdApp.Parameters.AddWithValue("@account_name", sitio.account_name);
                    objCon.cmdApp.Parameters.AddWithValue("@up_to_date", sitio.up_to_date);
                    objCon.cmdApp.ExecuteNonQuery();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                logErrorApp("StInfSit", e.Message);
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
