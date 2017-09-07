using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sincronizador
{
    class BOOrdenes
    {
        DAOOrdenes objOrdenes = new DAOOrdenes();
        public bool CamilyoguardarInfoOrdenes(Orden orden)
        {
            return objOrdenes.CamilyoguardarInfoOrden(orden);
        }

        public void sincronizarOrdenes()
        {
            objOrdenes.sincronizarOrdenes();
        }
    }
}
