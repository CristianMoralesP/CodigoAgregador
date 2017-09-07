using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;

namespace Agregador
{
    public class BOAgregador
    {
        public void guardarError(string codigo, string msj)
        {
            new DAOTiendas().logErrorApp(codigo, msj);
        }

        public void logRequest(string codigo, string rqst, string resp, string user, string idAgregador, string idGenerado)
        {
            new DAOTiendas().logRequest(codigo, rqst, resp, user, idAgregador, idGenerado);
        }

        public string generarClave()
        {
            List<string> descartados = new List<string>();
            string nuevaClave = string.Empty;
            int pos = 0;
            List<string> lista = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l",  "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", ".", "*", "-", "_"};
            for (int i = 0; i < 7; i++)
            {
                Random r = new Random();
                pos = r.Next(0, lista.Count);
                while (descartados.Contains(lista[pos]))
                    pos = r.Next(0, lista.Count);
                descartados.Add(lista[pos]);
                nuevaClave += lista[pos];
            }
            return nuevaClave;
        }

        public void ejecutarSincronizador()
        {
            string ruta = ConfigurationManager.AppSettings["archivoValidacionEjecucion"].ToString();
            if (File.Exists(ruta))
                File.Delete(ruta);
            System.Threading.Thread.Sleep(1000);
            File.AppendAllText(ruta, "1");
            System.Diagnostics.Process.Start(ruta);
            File.Delete(ruta);
            File.AppendAllText(ruta, "*");
        }

        public void ejecutarSincronizadorTodo()
        {
            string ruta = ConfigurationManager.AppSettings["archivoValidacionEjecucion"].ToString();
            if (File.Exists(ruta))
                File.Delete(ruta);
            System.Threading.Thread.Sleep(1000);
            File.AppendAllText(ruta, "*");
            System.Diagnostics.Process.Start(ruta);
            File.Delete(ruta);
            File.AppendAllText(ruta, "*");
        }

        public string fechaFormateada(string y, string m, string d)
        {
            if (m.Length == 1)
                m = "0" + m.ToString();
            if (d.Length == 1)
                d = "0" + d.ToString();
            return y + m + d;
        }
    }
}