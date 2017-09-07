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
    public partial class detalleProductoPublicado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (validarPagina())
                {
                    verDetalle();
                    txtNombreProducto.Visible = false;
                    txtDescripcion.Visible = false;
                    lblNombreProducto.Visible = true;
                    lblDescripcion.Visible = true;
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
                new BOProductos().listarDetalleProducto(ref dtInfo, id, true);
                if (dtInfo.Rows.Count > 0)
                {
                    decimal price = 0;
                    decimal.TryParse(dtInfo.Rows[0]["priceConv"].ToString(), out price);
                    string descripcion = dtInfo.Rows[0]["description"].ToString();
                    string cadenaSinTags = Regex.Replace(descripcion, "<.*?>", string.Empty);
                    ViewState["urlImagen"] = dtInfo.Rows[0]["pageImageURLExternal"].ToString();
                    ViewState["catAdicionales"] = dtInfo.Rows[0]["categoryIds"].ToString();
                    ViewState["idCam"] = dtInfo.Rows[0]["idCam"].ToString();

                    //Mostrar datos
                    lblNombreProducto.Text = dtInfo.Rows[0]["name"].ToString();
                    txtNombreProducto.Text = dtInfo.Rows[0]["name"].ToString();
                    ViewState["categorias"] = dtInfo.Rows[0]["categoryIds"].ToString();
                    lblDescripcion.Text = dtInfo.Rows[0]["description"].ToString();
                    txtDescripcion.Text = cadenaSinTags.Trim();
                    lblFecha.Text = dtInfo.Rows[0]["fechaCreacion"].ToString();
                    lblTienda.Text = dtInfo.Rows[0]["sitename"].ToString();
                    lblPrecio.Text = string.Format("{0:C}", double.Parse(dtInfo.Rows[0]["priceConv"].ToString())); 
                    img.ImageUrl = dtInfo.Rows[0]["pageImageURLExternal"].ToString();
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
                string ids = string.Empty;
                string[] categorias = ViewState["categorias"].ToString().Split(',');
                BOProductos categoriasProducto = new BOProductos();
                DataTable dtCategorias = new DataTable();
                for (int i = 0; i < categorias.Length; i++)
                {
                    int.TryParse(categorias[i], out idCategoria);
                    categoriasProducto.listarCategoriasProducto(ref dtCategorias, idCategoria);
                    if (dtCategorias.Rows.Count > 0)
                    {
                        cats += dtCategorias.Rows[0]["name"].ToString();
                        ids += dtCategorias.Rows[0]["idCam"].ToString();
                    }
                }
                lblCategorias.Text = cats;
                lblIdsCategorias.Text = ids;
                ViewState["catPrecarga"] = cats;
                ViewState["idsPrecarga"] = ids;
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
        private void previsualizarCambios()
        {
            if (validarPagina())
            {
                Session["prdNombre"] = txtNombreProducto.Text;
                Session["prdCategorias"] = lblIdsCategorias.Text;
                Session["prdTienda"] = lblTienda.Text;
                Session["prdPrecio"] = lblPrecio.Text;
                Session["prdDescripcion"] = txtDescripcion.Text;
                Session["prdUrlImagen"] = ViewState["urlImagen"];
            }
        }

        private void listarCategoriasMP()
        {
            if (validarPagina())
            {
                List<string> categoriasSincronizar = new List<string>();
                string[] categoriasActuales = lblCategorias.Text.Split(';');
                DataTable dtCategoriasMP = new DataTable();
                new BOProductos().listarCategoriasMP(ref dtCategoriasMP, txtBuscarCategorias.Text, int.Parse(Session["idUsuario"].ToString()));
                for (int i = 0; i < dtCategoriasMP.Rows.Count; i++)
                {
                    for (int j = 0; j < categoriasActuales.Length; j++)
                    {
                        if (dtCategoriasMP.Rows[i]["name"].ToString().Equals(categoriasActuales[j]))
                        {
                            categoriasSincronizar.Add(dtCategoriasMP.Rows[i]["idCam"].ToString());
                            dtCategoriasMP.Rows.RemoveAt(i);
                        }
                    }
                }
                /*
                if (ViewState["catPrecarga"] != null)
                {
                    string catIniciales = ViewState["catPrecarga"].ToString();
                    string[] categorias = catIniciales.Split(';');
                    for (int i = 0; i < dtCategoriasMP.Rows.Count; i++)
                    {
                        for (int j = 0; j < catIniciales.Length; j++)
                        {
                            if (dtCategoriasMP.Rows[i]["name"].Equals(catIniciales[j]))
                                dtCategoriasMP.Rows.RemoveAt(i);
                        }
                    }
                }*/
                grCategoriasMP.DataSource = dtCategoriasMP;
                grCategoriasMP.DataBind();
                ViewState["dtCategoriasMP"] = dtCategoriasMP;
                ViewState["categoriasHomologas"] = categoriasSincronizar;
            }
        }
        protected void btnPreliminar_Click(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                previsualizarCambios();
                Response.Write("<script> window.open('Previsualizacion.aspx', '_blank');</script>");
            }
        }
        protected void btnPublicar_Click(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                int id = 0;
                int.TryParse(Session["idProducto"].ToString(), out id);
                previsualizarCambios();
                string idCategoria = lblIdsCategorias.Text.Split(';')[0];
                string res = new BOProductos().actualizarProductoMP(id, txtNombreProducto.Text, txtDescripcion.Text, idCategoria);
                actualizarEstado(2);
                Response.Redirect("adminProductos.aspx");
            }
        }
        protected void lbtnEditar_Click(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                txtNombreProducto.Visible = true;
                txtDescripcion.Visible = true;
                lblNombreProducto.Visible = false;
                lblDescripcion.Visible = false;
            }
        }
        protected void btnBuscarCategorias_Click(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                listarCategoriasMP();
                pnlCategorias.Visible = true;
            }
        }
        protected void grCategoriasMP_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (validarPagina())
            {
                grCategoriasMP.PageIndex = e.NewPageIndex;
                grCategoriasMP.DataSource = (DataTable)ViewState["dtCategoriasMP"];
                grCategoriasMP.DataBind();
            }
        }
        protected void bntAsociarCategorias_Click(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                //aqui 2
                List<string> categoriasExistentes = new List<string>();
                categoriasExistentes = (List<string>)ViewState["categoriasHomologas"];
                ViewState["catOriginal"] = lblCategorias.Text;
                string catAdicionales = string.Empty;
                foreach (GridViewRow _row in grCategoriasMP.Rows)
                {
                    int idCategoria;
                    int.TryParse(_row.Cells[0].Text, out idCategoria);
                    CheckBox chkCategoria = (CheckBox)_row.FindControl("chkSeleccionar");
                    if (chkCategoria.Checked)
                    {
                        catAdicionales += idCategoria.ToString() + ";";
                        lblCategorias.Text += ";" + _row.Cells[1].Text;
                        categoriasExistentes.Add(_row.Cells[0].Text);
                    }
                }
                ViewState["catAdicionales"] = lblCategorias.Text + ";" + catAdicionales;

                //Armar string para sincronizar categorías
                string catsSinc = string.Empty;
                for (int i = 0; i < categoriasExistentes.Count; i++)
                {
                    catsSinc += categoriasExistentes[i] + ";";
                }
                ViewState["catsSincronizar"] = catsSinc;
                //listarCategoriasMP();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                if (ViewState["catOriginal"] != null)
                    lblCategorias.Text = ViewState["catOriginal"].ToString();
                pnlCategorias.Visible = false;
            }
        }
        protected void lbtnEliminar_Click(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                actualizarEstado(3);
                int idProducto, idCategoria;
                int.TryParse(ViewState["idCam"].ToString(), out idProducto);
                int.TryParse(ViewState["categorias"].ToString(), out idCategoria);
                new BOProductos().borrarProductoMP(idProducto, idCategoria);
                Response.Redirect("adminProductos.aspx");
            }
        }
    }
}