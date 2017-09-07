using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Sincronizador
{
    class APIConsumer
    {
        public string crearRequestActualizacionProd(string idCuenta, string name, string idCategoria, string idProducto, string listPrice, string price, string costPrice, string discountLabel)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{\"id\":\"");
                sb.Append(idProducto);
                sb.Append("\",\"name\":\"");
                sb.Append(name);
                sb.Append("\",\"categoryIds\":\"");
                sb.Append(idCategoria);
                sb.Append("\",\"categoryForeignIds\":\"");
                sb.Append(string.Empty);
                
                sb.Append("\",\"foreignId\":\"");
                sb.Append(string.Empty);
               
                sb.Append("\", \"listPrice\":\"");
                sb.Append(listPrice);
                sb.Append("\", \"price\":\"");
                sb.Append(price);
                
                sb.Append("\", \"isOnline\":\"");
                sb.Append("true\"");
                sb.Append("}");
                //string prov = "{\"id\": 3790361, \"categoryIds\": \"\", \"categoryForeignIds\": \"\", \"name\": \"Anillo Marania actu\", \"foreignId\": \"\",   \"mainImageUrlExternal\": \"http://www.aldeaviral.com/wp-content/uploads/2016/03/a-7.jpg\", \"shortDescription\": \"short desc\", \"description\": \"long desc\", \"listPrice\": 17.0, \"price\": 18.0, \"inventory\": 21, \"isOnline\": true}";
                //return new RestAPI("https://manage.dynamiapublicar.co/api/accounts/11157/catalog/categories/375735/products", HttpVerb.PUT, prov).MakeRequest();
                string cabecera = string.Format("{0}api/accounts/{1}/catalog/categories/{2}/products", ConfigurationManager.AppSettings["CamilyoAPIUrl"].ToString(), idCuenta, idCategoria);
                string res = new RestAPI(cabecera, HttpVerb.PUT, sb.ToString()).MakeRequest();
                new Conexion().logRequest("Sync_ActOff", string.Format("Headers: {0}. Verbo: {1}. Request: {2}", cabecera, "PUT", sb.ToString()), res, "0");
                return res;
            }
            catch (Exception e)
            {
                new Conexion().logErrorApp("Sync_ErrorActualizarOfertas", e.Message);
                return string.Empty;
            }
        }
        public string crearRequestEliminacionProd(string idCuenta, string categoryId, string productid)
        {
            try
            {
                //return new RestAPI(string.Format("http://manage.camilyo.us/api/accounts/{0}/catalog/categories/{1}/products/{2}", idCuenta, categoryId, productid), HttpVerb.DELETE).MakeRequest();
                string cabecera = string.Format("{0}api/accounts/{1}/catalog/categories/{2}/products/{3}", ConfigurationManager.AppSettings["CamilyoAPIUrl"].ToString());
                string res = new RestAPI(cabecera, HttpVerb.DELETE).MakeRequest();
                new Conexion().logRequest("Sync_InactOff", string.Format("Headers: {0}. Verbo: {1}.", cabecera, "DELETE"), res, "0");
                return res;
            }
            catch (Exception e)
            {
                new Conexion().logErrorApp("borrarOff", e.Message);
                return string.Empty;
            }
        }

        private string crearRequestEliminacionProd(int idCuenta, int categoryId, int productid)
        {
            try
            {
                return new RestAPI(string.Format("{0}api/accounts/{1}/catalog/categories/{2}/products/{3}", ConfigurationManager.AppSettings["CamilyoAPIUrl"].ToString(), idCuenta, categoryId, productid), HttpVerb.DELETE).MakeRequest();
            }
            catch (Exception e)
            {
                new Conexion().logErrorApp("console_crep", e.Message);
                return string.Empty;
            }
        }
    }
}
