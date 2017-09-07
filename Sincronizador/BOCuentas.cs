using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace Sincronizador
{
    public class BOCuentas
    {
        DAOCuentas objtiendas = new DAOCuentas();

        public void listarCuentas(ref DataTable dtCuentas)
        {
            objtiendas.listarTiendas(ref dtCuentas);
        }

        public void listarProductosMP_Ofertas(ref DataTable dtCuentas)
        {
            objtiendas.listarProductosMP_Ofertas(ref dtCuentas);
        }
        public void listarOfertasBorrar(ref DataTable dtOfertas)
        {
            objtiendas.listarOfertasBorrar(ref dtOfertas);
        }

        public void inactivarOfertas(string usuario, int id)
        {
            objtiendas.inactivarOferta(usuario, id);
        }
        public bool CamilyoguardarInfoCuenta(Cuenta cuenta)
        {
            if (cuenta.company_name == null)
                cuenta.company_name = string.Empty;
            return objtiendas.CamilyoguardarInfoTienda(cuenta);
        }

        public void sincronizarCuentas()
        {
            objtiendas.sincronizarCuentas();
        }

        public string obtenerCuentaMP()
        {
            return objtiendas.obtenerCuentaMP();
        }

        public string obtenerIdCuentaMP()
        {
            return objtiendas.obtenerIdCuentaMP();
        }

        public string obtenerIdOfertas()
        {
            return objtiendas.obtenerIdOfertas().ToString();
        }
    }
}
