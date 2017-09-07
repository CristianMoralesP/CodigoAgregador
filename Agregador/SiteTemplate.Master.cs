using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agregador
{
    public partial class SiteTemplate : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null)
            {
                lblUsuario.Text = Session["Usuario"].ToString();
                //lblNombrePagina.Text = Session["paginaActual"].ToString();
                imprimirOpciones();
            }
            else
                Response.Redirect("login.aspx");
        }

        private void imprimirOpciones()
        {
            BOUsuarios permisos = new BOUsuarios();

            if (permisos.paginaPermitida("tiendas.aspx"))
                Session["tiendasVisible"] = "display:block;";
            else
                Session["tiendasVisible"] = "display:none;";

            if (permisos.paginaPermitida("adminReportes.aspx"))
                Session["reportesVisible"] = "display:block;";
            else
                Session["reportesVisible"] = "display:none;";

            if (permisos.paginaPermitida("adminProductos.aspx"))
                Session["productosVisible"] = "display:block;";
            else
                Session["productosVisible"] = "display:none;";

            if (permisos.paginaPermitida("administrarUsuarios.aspx"))
                Session["usuariosVisible"] = "display:block;";
            else
                Session["usuariosVisible"] = "display:none;";

            if (permisos.paginaPermitida("cuentas.aspx"))
                Session["cuentasVisible"] = "display:block;";
            else
                Session["cuentasVisible"] = "display:none;";

            if (permisos.paginaPermitida("ofertas.aspx"))
                Session["ofertasVisible"] = "display:block;";
            else
                Session["ofertasVisible"] = "display:none;";
        }
    }
}