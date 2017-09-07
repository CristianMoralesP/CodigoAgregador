using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agregador
{
    public partial class invalida : System.Web.UI.Page
    {
        BOUsuarios objUsuarios = new BOUsuarios();
        protected void Page_Load(object sender, EventArgs e)
        {
            validarPagina();
        }

        private void validarPagina()
        {
            Label1.Text = new BOUsuarios().paginaPermitida(Request.Url.AbsolutePath.Replace("/", string.Empty)).ToString();
        }
    }
}