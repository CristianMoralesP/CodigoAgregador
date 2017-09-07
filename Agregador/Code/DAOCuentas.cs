using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Agregador
{
    public class DAOCuentas
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

        public bool crearCuenta(string nombreCuenta, bool esMP)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.administrarCuentas");
                    objCon.cmdApp.Parameters.AddWithValue("@nombreCuentaCam", nombreCuenta);
                    objCon.cmdApp.Parameters.AddWithValue("@esmp", esMP);
                    objCon.cmdApp.ExecuteNonQuery();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                logErrorApp("crearCta", ex.Message);
                return false;
            }
            finally
            { desconectar(); }
        }
        public void listarCuentasAgregador(ref DataTable dtCuentas, string termino)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.listarCuentas_AdminPublicar");
                    objCon.cmdApp.Parameters.AddWithValue("@termino", termino);
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtCuentas);
                }
            }
            catch (Exception er)
            {
                logErrorApp("listCtas", er.Message);
            }
            finally
            { desconectar(); }
        }

        public void listarMps(ref DataTable dtCuentas)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.obtenerMps");
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtCuentas);
                }
            }
            catch (Exception er)
            {
                logErrorApp("listMps", er.Message);
            }
            finally
            { desconectar(); }
        }

        public void listarMPUsuario(ref DataTable dtMPs, int idUsuario)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.listarMPUsuario");
                    objCon.cmdApp.Parameters.AddWithValue("@codUsuario", idUsuario);
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtMPs);
                }
            }
            catch (Exception er)
            {
                logErrorApp("listMpsUsr", er.Message);
            }
            finally
            { desconectar(); }
        }
        public void agregarCuenta()
        {
            
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