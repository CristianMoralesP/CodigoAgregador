using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agregador
{
    public partial class Previsualizacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (validarPagina())
                {
                    verDetalle();
                }
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

        private void verDetalle()
        {
            if (validarPagina())
            { 
                lblNombreProducto.Text = Session["prdNombre"].ToString();
                lblCategorias.Text = Session["prdCategorias"].ToString();
                lblTienda.Text = Session["prdTienda"].ToString();
                lblPrecio.Text = Session["prdPrecio"].ToString();
                lblDescripcion.Text = Session["prdDescripcion"].ToString();
                img.ImageUrl = Session["prdUrlImagen"].ToString();
            }
        }

        protected void lbtnVolver_Click(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                Response.Redirect("detalleProducto.aspx");
            }
        }
    }
}