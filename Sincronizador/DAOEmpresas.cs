using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Sincronizador
{
    class DAOEmpresas
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
        public void sincronizarEmpresas()
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.sincronizarEmpresas");
                    objCon.cmdApp.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                logErrorApp("syncEmp", e.Message);
            }
            finally
            { desconectar(); }
        }
        public bool CamilyoguardarInfoEmpresa(Empresa empresa, String idCuenta)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("Camilyo.GuardarInfoEmpresa");
                    objCon.cmdApp.Parameters.AddWithValue("@id", empresa.id);
                    objCon.cmdApp.Parameters.AddWithValue("@companyname", empresa.companyname);
                    objCon.cmdApp.Parameters.AddWithValue("@company_id", empresa.company_id);
                    objCon.cmdApp.Parameters.AddWithValue("@address", empresa.address);
                    objCon.cmdApp.Parameters.AddWithValue("@phone", empresa.phone);
                    objCon.cmdApp.Parameters.AddWithValue("@mobile", empresa.mobile);
                    objCon.cmdApp.Parameters.AddWithValue("@email", empresa.email);
                    objCon.cmdApp.Parameters.AddWithValue("@websitedomain", empresa.websitedomain);
                    objCon.cmdApp.Parameters.AddWithValue("@account_id", idCuenta);
                    objCon.cmdApp.ExecuteNonQuery();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                logErrorApp("StInfoEmp", e.Message);
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
