using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace Sincronizador
{
    class BOProductos
    {
        DAOProductos objProductos = new DAOProductos();
        public bool CamilyoguardarInfoProductos(Producto producto, String idCuenta)
        {
            return objProductos.CamilyoguardarInfoProducto(producto, idCuenta);
        }

        public void sincronizarProductos()
        {
            objProductos.sincronizarProductos();
        }

        public void ProductosEliminados(ref DataTable dtProductosEliminados)
        {
            objProductos.ProductosEliminados(ref dtProductosEliminados);
        }

        public void ProductosSinInventario(ref DataTable dtProductosSinInventario)
        {
            objProductos.ProductosSinInventario(ref dtProductosSinInventario);
        }

        public void ProductosConInventario(ref DataTable dtProductosConInventario)
        {
            objProductos.ProductosConInventario(ref dtProductosConInventario);
        }
    }
}
