using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sincronizador
{
    class BOCategorias
    {
        DAOCategorias objCategorias = new DAOCategorias();
        public bool CamilyoguardarInfoCategorias(Categoria categoria, String idCuenta)
        {
            return objCategorias.CamilyoguardarInfoCategoria(categoria, idCuenta);
        }

        public bool CamilyoguardarInfoSubCategorias(SubCategoria subCategoria)
        {
            return objCategorias.CamilyoguardarInfoSubCategoria(subCategoria);
        }

        public void sincronizarCategorias()
        {
            objCategorias.sincronizarCategorias();
        }

        public void sincronizarSubCategorias()
        {
            objCategorias.sincronizarSubCategorias();
        }
    }
}
