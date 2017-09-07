using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace Agregador
{
    public class BOReportes
    {
        DAOReportes reps = new DAOReportes();

        public void listarCantidadAliados(ref DataTable dtcantidadAliados, DateTime fecini, DateTime fecfin, int idUsuario)
        {
            reps.listarCantidadAliados(ref dtcantidadAliados,fecini,fecfin, idUsuario);
        }

        public void listarProductosRegistrados(ref DataTable dtProductosRegistrados, DateTime fecini, DateTime fecfin, int idUsuario)
        {
            reps.listarProductosRegistrados(ref dtProductosRegistrados, fecini, fecfin, idUsuario);
        }

        public void listarTransacciones(ref DataTable dtTransacciones, DateTime fecini, DateTime fecfin, int idUsuario)
        {
            reps.listarTransacciones(ref dtTransacciones, fecini, fecfin, idUsuario);
        }

        public void listarValorTransaccion(ref DataTable dtValorTransaccion, DateTime fecini, DateTime fecfin, int idUsuario)
        {
            reps.listarValorTransaccion(ref dtValorTransaccion, fecini, fecfin, idUsuario);
        }

        public void listarSkuVenta(ref DataTable dtSkuVenta, DateTime fecini, DateTime fecfin)
        {
            reps.listarSkuVenta(ref dtSkuVenta, fecini, fecfin);
        }

        public void listarIngresosTotales(ref DataTable dtIngresosTotales, DateTime fecini, DateTime fecfin, int idUsuario)
        {
            reps.listarIngresosTotales(ref dtIngresosTotales, fecini, fecfin, idUsuario);
        }

        public void listarClientes(ref DataTable dtClientes, DateTime fecini, DateTime fecfin, int idUsuario)
        {
            reps.listarClientes(ref dtClientes, fecini, fecfin, idUsuario);
        }
    }
}