using System;
using System.Data;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agregador
{
    public partial class adminProductos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                Session["paginaActual"] = "Administración de publicación y productos";
                listarProductosXEstado();
            }
            else
                Response.Redirect("login.aspx");
        }

        private bool validarPagina()
        {
            return new BOUsuarios().paginaPermitida(Request.Url.AbsolutePath.ToString());
        }

        private void listarProductosXEstado()
        {
            DataTable dtProductos = new DataTable();
            new BOProductos().listarProductosXEstado(ref dtProductos, int.Parse(Session["idUsuario"].ToString()));
            if (dtProductos.Rows.Count > 0)
            {
                for (int i = 0; i < dtProductos.Rows.Count; i++)
                {
                    switch (dtProductos.Rows[i]["Estado"].ToString())
                    {
                        case "Nuevo":
                            lblNuevos.Text = dtProductos.Rows[i]["Total"].ToString();
                            break;
                        case "Publicados":
                            lblPublicados.Text = dtProductos.Rows[i]["Total"].ToString();
                            break;
                        case "Anteriores":
                            lblAnteriores.Text = dtProductos.Rows[i]["Total"].ToString();
                            break;
                        case "Descartados":
                            lblDescartados.Text = dtProductos.Rows[i]["Total"].ToString();
                            break;
                    }
                }
            }
        }
    }
}