using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Sincronizador
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

        public void listarTiendas(ref DataTable dtTiendas)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.listarCuentas_MP");
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtTiendas);
                }
            }
            catch (Exception er)
            {
                logErrorApp("listTie", er.Message);
            }
            finally
            { desconectar(); }
        }

        public void listarProductosMP_Ofertas(ref DataTable dtTiendas)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.listarProductosMPOfertas");
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtTiendas);
                }
            }
            catch (Exception er)
            {
                logErrorApp("listPrdOff", er.Message);
            }
            finally
            { desconectar(); }
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
                logErrorApp("Sync_idOfertas", er.Message);
            }
            finally
            { desconectar(); }
            return id;
        }

        public void listarOfertasBorrar(ref DataTable dtOfertas)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.listarOfertasInactivar");
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtOfertas);
                }
            }
            catch (Exception er)
            {
                logErrorApp("listOffInact", er.Message);
            }
            finally
            { desconectar(); }
        }
        public void inactivarOferta(string usuario, int id)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.inactivarOferta");
                    objCon.cmdApp.Parameters.AddWithValue("@idOferta", id);
                    objCon.cmdApp.Parameters.AddWithValue("@userId", usuario);
                    objCon.cmdApp.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                logErrorApp("syncInactCtas", e.Message);
            }
            finally
            { desconectar(); }
        }
        public void sincronizarCuentas()
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.sincronizarCuentas");
                    objCon.cmdApp.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                logErrorApp("syncCtas", e.Message);
            }
            finally
            { desconectar(); }
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
                logErrorApp("CtaMP", er.Message);
            }
            finally
            { desconectar(); }
            return nombre;
        }

        public string obtenerIdCuentaMP()
        {
            string nombre = string.Empty;
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.obtenerIdMP");
                    nombre = objCon.cmdApp.ExecuteScalar().ToString();
                }
            }
            catch (Exception er)
            {
                logErrorApp("idCtaMP", er.Message);
            }
            finally
            { desconectar(); }
            return nombre;
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
