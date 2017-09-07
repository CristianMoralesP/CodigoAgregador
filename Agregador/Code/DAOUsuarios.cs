using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Agregador
{
    public class DAOUsuarios
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

        #region CRUDUsuarios
        public bool administrarUsuario(int tipoTran, int idUsuario, string nombres, string clave, string correo, int codRol)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.administrarUsuario");
                    objCon.cmdApp.Parameters.AddWithValue("@tipoTran", tipoTran);
                    objCon.cmdApp.Parameters.AddWithValue("@idUsuario", idUsuario);
                    objCon.cmdApp.Parameters.AddWithValue("@nombres", nombres);
                    objCon.cmdApp.Parameters.AddWithValue("@clave", clave);
                    objCon.cmdApp.Parameters.AddWithValue("@correo", correo);
                    objCon.cmdApp.Parameters.AddWithValue("@codRol", codRol);
                    objCon.cmdApp.ExecuteNonQuery();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                logErrorApp("adUsr", ex.Message);
                return false;
            }
            finally
            { desconectar(); }
        }

        public int crearUsuario(string nombres, string clave, string correo, int codRol)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.administrarUsuario");
                    objCon.cmdApp.Parameters.AddWithValue("@tipoTran", 1);
                    objCon.cmdApp.Parameters.AddWithValue("@idUsuario", 0);
                    objCon.cmdApp.Parameters.AddWithValue("@nombres", nombres);
                    objCon.cmdApp.Parameters.AddWithValue("@clave", clave);
                    objCon.cmdApp.Parameters.AddWithValue("@correo", correo);
                    objCon.cmdApp.Parameters.AddWithValue("@codRol", codRol);
                    return int.Parse(objCon.cmdApp.ExecuteScalar().ToString());
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                logErrorApp("crUsr", ex.Message);
                return 0;
            }
            finally
            { desconectar(); }
        }

        public bool asignarMPsUsuario(int idUsuario, string idCuenta)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.AsignarMPsUsuario");
                    objCon.cmdApp.Parameters.AddWithValue("@codUsuario", idUsuario);
                    objCon.cmdApp.Parameters.AddWithValue("@account_id", idCuenta);
                    objCon.cmdApp.ExecuteNonQuery();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                logErrorApp("asgMpUsr", ex.Message);
                return false;
            }
            finally
            { desconectar(); }
        }

        public bool editarMPsUsuario(int idUsuario, string idCuenta, bool activo)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.EditarMPsUsuario");
                    objCon.cmdApp.Parameters.AddWithValue("@codUsuario", idUsuario);
                    objCon.cmdApp.Parameters.AddWithValue("@account_id", idCuenta);
                    objCon.cmdApp.Parameters.AddWithValue("@activo", activo);
                    objCon.cmdApp.ExecuteNonQuery();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                logErrorApp("editMpUsr", ex.Message);
                return false;
            }
            finally
            { desconectar(); }
        }

        #endregion
        public bool iniciarSesion(ref DataTable dtDatosUsuario, string usuario)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.obtenerUsuarioPorCorreo", ref dtDatosUsuario);
                    objCon.cmdApp.Parameters.AddWithValue("@usuario", usuario);
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtDatosUsuario);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                logErrorApp("login", ex.Message);
                return false;
            }
            finally
            { desconectar(); }
        }

        public void rolesPorUsuario(int idUsuario, ref DataTable dtRoles)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.ObtenerPermisosPorUsuarios", ref dtRoles);
                    objCon.cmdApp.Parameters.AddWithValue("@idUsuario", idUsuario);
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtRoles);
                }
            }
            catch (Exception er)
            {
                logErrorApp("RolXUsr", er.Message);
            }
            finally
            { desconectar(); }
        }

        public void listarUsuarios(ref DataTable dtUsuarios, string termino)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.listarUsuarios");
                    objCon.cmdApp.Parameters.AddWithValue("@termino", termino);
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtUsuarios);
                }
            }
            catch (Exception er)
            {
                logErrorApp("lstUsrs", er.Message);
            }
            finally
            { desconectar(); }
        }

        public void listarUsuarioPorId(ref DataTable dtUsuario, int id)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.listarUsuarioPorId");
                    objCon.cmdApp.Parameters.AddWithValue("@id", id);
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtUsuario);
                }
            }
            catch (Exception er)
            {
                logErrorApp("lstUsrXId", er.Message);
            }
            finally
            { desconectar(); }
        }

        public void listarRoles(ref DataTable dtRoles)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.listarRoles", ref dtRoles);
                    objCon.adApp = new SqlDataAdapter(objCon.cmdApp);
                    objCon.adApp.Fill(dtRoles);
                }
            }
            catch (Exception er)
            {
                logErrorApp("lstRles", er.Message);
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