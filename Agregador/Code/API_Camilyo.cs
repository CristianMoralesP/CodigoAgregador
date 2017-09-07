using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Configuration;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;

namespace Agregador
{
    public class API_Camilyo
    {
        HttpClient camilyoClient = new HttpClient();
        private bool conectarAPI()
        {
            try
            {
                camilyoClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["CamilyoAPIUrl"]);
                var byteArray = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", ConfigurationManager.AppSettings["CamilyoAPIUser"], ConfigurationManager.AppSettings["CamilyoAPIPassword"]));
                camilyoClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                return true;
            }
            catch (Exception ex)
            {
                new BOAgregador().guardarError("conAp1", ex.Message);
                return false;
            }
        }

        public string obtenerInfoCuenta(string nombreCuenta)
        {
            return new RestAPI("http://manage.camilyo.us/api/accounts/", HttpVerb.GET).MakeRequest("?userName="+ nombreCuenta);
        }

        private void obtenerInfoJS()
        { }

        private void establecerInfoJS()
        { }
    }
}