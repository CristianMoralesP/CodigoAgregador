using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sincronizador
{
    class BOSitios
    {
        DAOSitios objSitios = new DAOSitios();
        public bool CamilyoguardarInfoSitio(Sitio sitio)
        {
            return objSitios.CamilyoguardarInfoSitio(sitio);
        }

        public void sincronizarSitios()
        {
            objSitios.sincronizarSitios();
        }
    }
}
