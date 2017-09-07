using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agregador
{
    public partial class agregarCuenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!validarPagina())
                Response.Redirect("login.aspx");
        }
        private bool validarPagina()
        {
            return new BOUsuarios().paginaPermitida(Request.Url.AbsolutePath.ToString());
        }
        protected void btnCrear_Click(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                if (new BOCuentas().crearCuenta(txtCuenta.Text, chkMP.Checked))
                    Response.Redirect("cuentas.aspx");
                else
                    lblRespuesta.Text = "No se logró crear la cuenta";
            }
            else
                Response.Redirect("login.aspx");
        }
    }
}