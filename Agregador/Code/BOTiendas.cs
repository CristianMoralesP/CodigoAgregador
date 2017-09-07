using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Configuration;

namespace Agregador
{
    public class BOTiendas
    {
        DAOTiendas objtiendas = new DAOTiendas();

        public void listarCuentas(ref DataTable dtTiendas, string termino, int idUsuario)
        {
            objtiendas.listarCuentas(ref dtTiendas, termino, idUsuario);
        }

        public void listarSitios(ref DataTable dtSitios, int idCuenta)
        {
            objtiendas.listarSitios(ref dtSitios, idCuenta);
        }

        public string cambiarEstadoCuenta(string nombreCuenta, bool estado, int idCuentaCamilyo, string correoCuenta)
        {
            try
            {
                string res = string.Empty;
                objtiendas.cambiarEstadoCuenta(idCuentaCamilyo, estado);
                if (!estado)
                    res = inhabilitarCuentaCam(idCuentaCamilyo);
                else
                {
                    res = habilitarCuenta(idCuentaCamilyo, nombreCuenta, correoCuenta);
                }
                return res;

            }
            catch (Exception ex)
            {
                objtiendas.logErrorApp("cec", ex.Message);
                return ex.Message;
            }
        }

        private string publicarSitio(int idCuenta, int idSitio)
        {
            try
            {
                //return new RestAPI("http://manage.dynamiapublicar.co/api/accounts/5777/sites/140739/publish", HttpVerb.POST).MakeRequest();
                return new RestAPI(string.Format("{0}api/accounts/{1}/sites/{2}/publish", ConfigurationManager.AppSettings["CamilyoAPIUrl"].ToString(), idCuenta, idSitio), HttpVerb.POST).MakeRequest();
            }
            catch (Exception e)
            {
                objtiendas.logErrorApp("ps", e.Message);
                return string.Empty;
            }
        }

        private string inhabilitarCuentaCam(int idCuentaCamilyo)
        {
            try
            {
                new RestAPI(string.Format("{0}api/accounts/{1}/suspend?suspendAllAssets=true", ConfigurationManager.AppSettings["CamilyoAPIUrl"].ToString(), idCuentaCamilyo), HttpVerb.PUT).MakeRequest();
                return "ok";
            }
            catch (Exception e) 
            {
                objtiendas.logErrorApp("icc", e.Message);
                return e.Message;
            }
        }

        private string habilitarCuenta(int idCuentaCamilyo, string nombreCuenta, string correoCuenta)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{\"id\":\"");
                sb.Append(idCuentaCamilyo);
                sb.Append("\",\"name\":\"");
                sb.Append(nombreCuenta);
                sb.Append("\", \"email\":\"");
                sb.Append(correoCuenta);
                sb.Append("\",");
                sb.Append("\"is_active\":true");
                sb.Append("}");
                //return new RestAPI("http://manage.dynamiapublicar.co/api/accounts/", HttpVerb.PUT, sb.ToString()).MakeRequest();
                return new RestAPI(string.Format("{0}api/accounts/", ConfigurationManager.AppSettings["CamilyoAPIUrl"].ToString()), HttpVerb.PUT, sb.ToString()).MakeRequest();
            }
            catch (Exception e)
            {
                objtiendas.logErrorApp("hc", e.Message);
                return e.Message;
            }
        }
    }
}