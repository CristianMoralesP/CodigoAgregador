using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agregador
{
    public partial class Sitios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (validarPagina())
                    listarSitios();
                else
                    Response.Redirect("login.aspx");
            }
            else
                validarPagina();
        }

        private bool validarPagina()
        {
            return new BOUsuarios().paginaPermitida(Request.Url.AbsolutePath.ToString());
        }

        private void listarSitios()
        {
            if (Session["idCuenta"] != null)
            {
                DataTable dtTiendas = new DataTable();
                new BOTiendas().listarSitios(ref dtTiendas, int.Parse(Session["idCuenta"].ToString()));
                grSitios.DataSource = dtTiendas;
                grSitios.DataBind();
            }
            else
                Response.Redirect("tiendas.aspx");
        }
    }
}