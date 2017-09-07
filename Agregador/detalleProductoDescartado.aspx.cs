using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace Agregador
{
    public partial class detalleProductoDescartado : System.Web.UI.Page
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
                int id = 0;
                DataTable dtInfo = new DataTable();
                int.TryParse(Session["idProducto"].ToString(), out id);
                new BOProductos().listarDetalleProducto(ref dtInfo, id, false);
                if (dtInfo.Rows.Count > 0)
                {
                    decimal price = 0;
                    decimal.TryParse(dtInfo.Rows[0]["priceConv"].ToString(), out price);
                    string descripcion = dtInfo.Rows[0]["description"].ToString();
                    string cadenaSinTags = Regex.Replace(descripcion, "<.*?>", string.Empty);
                    ViewState["urlImagen"] = dtInfo.Rows[0]["mainImageUrl"].ToString();
                    ViewState["catAdicionales"] = dtInfo.Rows[0]["categoryIds"].ToString();

                    //Mostrar datos
                    lblNombreProducto.Text = dtInfo.Rows[0]["name"].ToString();
                    ViewState["categorias"] = dtInfo.Rows[0]["categoryIds"].ToString();
                    lblDescripcion.Text = dtInfo.Rows[0]["description"].ToString();
                    lblFecha.Text = dtInfo.Rows[0]["fechaCreacion"].ToString();
                    lblTienda.Text = dtInfo.Rows[0]["sitename"].ToString();
                    lblPrecio.Text = string.Format("{0:C}", double.Parse(dtInfo.Rows[0]["priceConv"].ToString()));
                    img.ImageUrl = dtInfo.Rows[0]["mainImageUrl"].ToString();
                    listarCategoriasProducto();
                }
            }
        }

        private void listarCategoriasProducto()
        {
            if (validarPagina())
            {
                int idCategoria;
                string cats = string.Empty;
                string[] categorias = ViewState["categorias"].ToString().Split(',');
                BOProductos categoriasProducto = new BOProductos();
                DataTable dtCategorias = new DataTable();
                for (int i = 0; i < categorias.Length; i++)
                {
                    int.TryParse(categorias[i], out idCategoria);
                    categoriasProducto.listarCategoriasProducto(ref dtCategorias, idCategoria);
                    if (dtCategorias.Rows.Count > 0)
                        cats += dtCategorias.Rows[0]["name"].ToString();
                }
                lblCategorias.Text = cats;
                ViewState["catPrecarga"] = cats;
            }
        }

        //
        protected void lbtnMarcarNuevo_Click(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                actualizarEstado(1);
                Response.Redirect("adminProductos.aspx");
            }
        }
        private void actualizarEstado(int nuevoEstado)
        {
            if (validarPagina())
            {
                int id = 0;
                int.TryParse(Session["idProducto"].ToString(), out id);
                new BOProductos().actualizarEstadoProducto(id, nuevoEstado);
            }
        }
    }
}