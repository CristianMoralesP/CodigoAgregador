using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Sincronizador
{
    public class Conexion
    {
        private SqlConnection conBd = null;
        public SqlCommand cmdApp = null;
        public SqlDataAdapter adApp = null;
        public SqlConnection conectar()
        {
            try
            {
                this.conBd = new SqlConnection(ConfigurationManager.AppSettings["cadConexion"]);
                this.conBd.Open();
                return this.conBd;
            }
            catch (Exception e)
            {
                System.IO.File.AppendAllText(ConfigurationManager.AppSettings["archivoLogs"].ToString() + "log.txt", string.Format("Error {0}", e.Message));
                return null;
            }
        }

        public void desconectar()
        {
            if (this.conBd.State == ConnectionState.Open)
                this.conBd.Close();
            this.conBd = null;
        }

        public void configurarComando(string nombreTSQL, ref DataTable dtDatos)
        {
            this.cmdApp = new SqlCommand();
            this.cmdApp.Connection = this.conBd;
            this.cmdApp.CommandType = CommandType.StoredProcedure;
            this.cmdApp.CommandText = nombreTSQL;
        }

        public void configurarComando(string nombreTSQL)
        {
            this.cmdApp = new SqlCommand();
            this.cmdApp.Connection = this.conBd;
            this.cmdApp.CommandText = nombreTSQL;
            this.cmdApp.CommandType = CommandType.StoredProcedure;
        }

        public void logErrorApp(string codigo, string msj)
        {
            this.conectar();
            configurarComando("Logs.GuardarError");
            this.cmdApp.Parameters.AddWithValue("@codigo", codigo);
            this.cmdApp.Parameters.AddWithValue("@mensaje", msj);
            this.cmdApp.ExecuteNonQuery();
        }
        public void logRequest(string codigo, string rqst, string resp, string user)
        {
            this.conectar();
            configurarComando("Logs.GuardarRequest");
            cmdApp.Parameters.AddWithValue("@code", codigo);
            cmdApp.Parameters.AddWithValue("@request", rqst);
            cmdApp.Parameters.AddWithValue("@response", resp);
            cmdApp.Parameters.AddWithValue("@userId", user);
            cmdApp.Parameters.AddWithValue("@idAgregador", string.Empty);
            cmdApp.Parameters.AddWithValue("@idGenerado", string.Empty);
            cmdApp.ExecuteNonQuery();
            desconectar();
        }
    }
}
