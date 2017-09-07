using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agregador
{
    public class BOCuentas
    {
        DAOCuentas cuentas = new DAOCuentas();
        public void listarCuentasAgregador(ref DataTable dtCuentas, string termino)
        {
            cuentas.listarCuentasAgregador(ref dtCuentas, termino);
        }

        public void listarMPUsuario(ref DataTable dtTiendas, int idUsuario)
        {
            cuentas.listarMPUsuario(ref dtTiendas, idUsuario);
        }

        public void listarMps(ref DataTable dtMps)
        {
            cuentas.listarMps(ref dtMps);
        }
        public bool crearCuenta(string nombre, bool esMP)
        {
            return cuentas.crearCuenta(nombre, esMP);
        } 
    }
}