using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sincronizador
{
    class BOEmpresas
    {
        DAOEmpresas objEmpresas = new DAOEmpresas();
        public bool CamilyoguardarInfoEmpresas(Empresa empresa, String idCuenta)
        {
            if (empresa.company_id == null)
                empresa.company_id = string.Empty;
            if (empresa.websitedomain == null)
                empresa.websitedomain = string.Empty;
            if (empresa.address == null)
                empresa.address = string.Empty;
            if (empresa.phone == null)
                empresa.phone = string.Empty;
            if (empresa.mobile == null)
                empresa.mobile = string.Empty;
            if (empresa.websitedomain == null)
                empresa.websitedomain = string.Empty;
            if (empresa.email == null)
                empresa.email = string.Empty;
            return objEmpresas.CamilyoguardarInfoEmpresa(empresa, idCuenta);
        }

        public void sincronizarEmpresas()
        {
            objEmpresas.sincronizarEmpresas();
        }
    }
}
