using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Agregador
{
    public class DAOReportes
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

        public void listarCantidadAliados(ref DataTable dtCantidadAliados,DateTime fecini, DateTime fecfin, int idUsuario)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.rptCantidadAliados");
                    objCon.cmdApp.Parameters.AddWithValue("@fecini", fecini);
                    objCon.cmdApp.Parameters.AddWithValue("@fecfin", fecfin);
                    objCon.cmdApp.Parameters.AddWithValue("@codUsuario", idUsuario);
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtCantidadAliados);
                }
            }
            catch (Exception er)
            {
                logErrorApp("lstCantAld", er.Message);
            }
            finally
            { desconectar(); }
        }

        public void listarProductosRegistrados(ref DataTable dtProductosRegistrados, DateTime fecini, DateTime fecfin, int idUsuario)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.rptProductosRegistrados");
                    objCon.cmdApp.Parameters.AddWithValue("@fecini", fecini);
                    objCon.cmdApp.Parameters.AddWithValue("@fecfin", fecfin);
                    objCon.cmdApp.Parameters.AddWithValue("@codUsuario", idUsuario);
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtProductosRegistrados);
                }
            }
            catch (Exception er)
            {
                logErrorApp("lstPrdReg", er.Message);
            }
            finally
            { desconectar(); }
        }

        public void listarTransacciones(ref DataTable dtTransacciones, DateTime fecini, DateTime fecfin, int idUsuario)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.rptTransacciones");
                    objCon.cmdApp.Parameters.AddWithValue("@fecini", fecini);
                    objCon.cmdApp.Parameters.AddWithValue("@fecfin", fecfin);
                    objCon.cmdApp.Parameters.AddWithValue("@codUsuario", idUsuario);
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtTransacciones);
                }
            }
            catch (Exception er)
            {
                logErrorApp("lstTrans", er.Message);
            }
            finally
            { desconectar(); }
        }

        public void listarValorTransaccion(ref DataTable dtValorTransaccion, DateTime fecini, DateTime fecfin, int idUsuario)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.rptValorTransaccion");
                    objCon.cmdApp.Parameters.AddWithValue("@fecini", fecini);
                    objCon.cmdApp.Parameters.AddWithValue("@fecfin", fecfin);
                    objCon.cmdApp.Parameters.AddWithValue("@codUsuario", idUsuario);
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtValorTransaccion);
                }
            }
            catch (Exception er)
            {
                logErrorApp("lstVlrTrans", er.Message);
            }
            finally
            { desconectar(); }
        }

        public void listarSkuVenta(ref DataTable dtSkuVenta, DateTime fecini, DateTime fecfin)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.rptSkuVenta");
                    objCon.cmdApp.Parameters.AddWithValue("@fecini", fecini);
                    objCon.cmdApp.Parameters.AddWithValue("@fecfin", fecfin);
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtSkuVenta);
                }
            }
            catch (Exception er)
            {
                logErrorApp("lstSkVta", er.Message);
            }
            finally
            { desconectar(); }
        }

        public void listarIngresosTotales(ref DataTable dtIngresosTotales, DateTime fecini, DateTime fecfin, int idUsuario)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.rptIngresosTotales");
                    objCon.cmdApp.Parameters.AddWithValue("@fecini", fecini);
                    objCon.cmdApp.Parameters.AddWithValue("@fecfin", fecfin);
                    objCon.cmdApp.Parameters.AddWithValue("@codUsuario", idUsuario);
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtIngresosTotales);
                }
            }
            catch (Exception er)
            {
                logErrorApp("lstIngTot", er.Message);
            }
            finally
            { desconectar(); }
        }

        public void listarClientes(ref DataTable dtClientes, DateTime fecini, DateTime fecfin, int idUsuario)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.rptClientes");
                    objCon.cmdApp.Parameters.AddWithValue("@fecini", fecini);
                    objCon.cmdApp.Parameters.AddWithValue("@fecfin", fecfin);
                    objCon.cmdApp.Parameters.AddWithValue("@codUsuario", idUsuario);
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtClientes);
                }
            }
            catch (Exception er)
            {
                logErrorApp("lstCltes", er.Message);
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