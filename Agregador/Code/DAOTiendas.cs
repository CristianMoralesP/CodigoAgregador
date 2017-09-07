using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Agregador
{
    public class DAOTiendas
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

        public void listarCuentas(ref DataTable dtTiendas, string termino, int idUsuario)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.listarCuentas");
                    objCon.cmdApp.Parameters.AddWithValue("@termino", termino);
                    objCon.cmdApp.Parameters.AddWithValue("@codUsuario", idUsuario);
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtTiendas);
                }
            }
            catch (Exception er)
            {
                logErrorApp("lstCtas", er.Message);
            }
            finally
            { desconectar(); }
        }

        public void listarSitios(ref DataTable dtSitios, int idCuenta)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.listarSitios");
                    objCon.cmdApp.Parameters.AddWithValue("@account_id", idCuenta);
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtSitios);
                }
            }
            catch (Exception er)
            {
                logErrorApp("lstSit", er.Message);
            }
            finally
            { desconectar(); }
        }

        public void cambiarEstadoCuenta(int idCuentaCam, bool estado)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.cambiarEstadoCuenta");
                    objCon.cmdApp.Parameters.AddWithValue("@idCuentaCam", idCuentaCam);
                    objCon.cmdApp.Parameters.AddWithValue("@activo", estado);
                    objCon.cmdApp.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                logErrorApp("cambEstCta", e.Message);
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
            desconectar();
        }

        public void logRequest(string codigo, string rqst, string resp, string user, string idAgregador, string idGenerado)
        {
            this.conectar();
            objCon.configurarComando("Logs.GuardarRequest");
            objCon.cmdApp.Parameters.AddWithValue("@code", codigo);
            objCon.cmdApp.Parameters.AddWithValue("@request", rqst);
            objCon.cmdApp.Parameters.AddWithValue("@response", resp);
            objCon.cmdApp.Parameters.AddWithValue("@userId", user);
            objCon.cmdApp.Parameters.AddWithValue("@idAgregador", idAgregador);
            objCon.cmdApp.Parameters.AddWithValue("@idGenerado", idGenerado);
            objCon.cmdApp.ExecuteNonQuery();
            desconectar();
        }

        public bool CamilyoguardarInfoTienda(Cuenta cuenta)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("Camilyo.GuardarInfoCuenta");
                    objCon.cmdApp.Parameters.AddWithValue("@id", cuenta.id);
                    objCon.cmdApp.Parameters.AddWithValue("@name", cuenta.name);
                    objCon.cmdApp.Parameters.AddWithValue("@foreignId", cuenta.foreignId);
                    objCon.cmdApp.Parameters.AddWithValue("@email", cuenta.email);
                    objCon.cmdApp.Parameters.AddWithValue("@password", cuenta.password);
                    objCon.cmdApp.Parameters.AddWithValue("@role", cuenta.role);
                    objCon.cmdApp.Parameters.AddWithValue("@roleType", cuenta.roleType);
                    objCon.cmdApp.Parameters.AddWithValue("@creation_time", cuenta.creation_time);
                    objCon.cmdApp.Parameters.AddWithValue("@last_update_time", cuenta.last_update_time);
                    objCon.cmdApp.Parameters.AddWithValue("@last_login_time", cuenta.last_login_time);
                    objCon.cmdApp.Parameters.AddWithValue("@is_active", cuenta.is_active);
                    objCon.cmdApp.Parameters.AddWithValue("@simplification_mode", cuenta.simplification_mode);
                    objCon.cmdApp.Parameters.AddWithValue("@use_external_login", cuenta.use_external_login);
                    objCon.cmdApp.Parameters.AddWithValue("@has_terms_flag", cuenta.has_terms_flag);
                    objCon.cmdApp.Parameters.AddWithValue("@onboarding_complited", cuenta.onboarding_complited);
                    objCon.cmdApp.Parameters.AddWithValue("@must_change_pwd", cuenta.must_change_pwd);
                    objCon.cmdApp.Parameters.AddWithValue("@company_name", cuenta.company_name);
                    objCon.cmdApp.ExecuteNonQuery();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                logErrorApp("StInfoTie", e.Message);
                return false;
            }
            finally
            { desconectar(); }
        }
    }
}