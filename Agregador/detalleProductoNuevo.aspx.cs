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
    public partial class detalleProductoNuevo : System.Web.UI.Page
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
                new BOProductos().listarDetalleProducto(ref dtInfo, id, false);
                if (dtInfo.Rows.Count > 0)
                {
                    decimal price = 0;
                    decimal.TryParse(dtInfo.Rows[0]["priceConv"].ToString(), out price);
                    string descripcion = dtInfo.Rows[0]["description"].ToString();
                    string cadenaSinTags = Regex.Replace(descripcion, "<.*?>", string.Empty);
                    ViewState["urlImagen"] = dtInfo.Rows[0]["mainImageUrl"].ToString();
                    ViewState["categorias"] = dtInfo.Rows[0]["categoryIds"].ToString();
                    if (string.IsNullOrEmpty(dtInfo.Rows[0]["priceConv"].ToString()))
                        dtInfo.Rows[0]["priceConv"] = "0";
                    if (string.IsNullOrEmpty(dtInfo.Rows[0]["listPrice"].ToString()))
                        dtInfo.Rows[0]["listPrice"] = "0";
                    if (string.IsNullOrEmpty(dtInfo.Rows[0]["costPrice"].ToString()))
                        dtInfo.Rows[0]["costPrice"] = "0";
                    if (string.IsNullOrEmpty(dtInfo.Rows[0]["inventory"].ToString()))
                        dtInfo.Rows[0]["inventory"] = "0";
                    if (int.Parse(dtInfo.Rows[0]["inventory"].ToString()) > 0)
                        chkPrdDisponible.Checked = true;
                    else
                        chkPrdDisponible.Checked = false;
                    if (string.IsNullOrEmpty(dtInfo.Rows[0]["discountlabel"].ToString()))
                        lblDescuento.Text = "NA";
                    else
                        lblDescuento.Text = dtInfo.Rows[0]["discountlabel"].ToString();

                    //Mostrar datos
                    lblNombreProducto.Text = dtInfo.Rows[0]["name"].ToString();
                    txtNombreProducto.Text = dtInfo.Rows[0]["name"].ToString();
                    ViewState["categorias"] = dtInfo.Rows[0]["categoryIds"].ToString();
                    lblDescripcion.Text = dtInfo.Rows[0]["description"].ToString();
                    txtDescripcion.Text = cadenaSinTags.Trim();
                    lblFecha.Text = dtInfo.Rows[0]["fechaCreacion"].ToString();
                    lblTienda.Text = dtInfo.Rows[0]["sitename"].ToString();
                    lblPrecio.Text = string.Format("{0:C}", double.Parse(dtInfo.Rows[0]["priceConv"].ToString()));
                    lblPrecioCatalogo.Text = string.Format("{0:C}", double.Parse(dtInfo.Rows[0]["listPrice"].ToString()));
                    lblPrecioFinal.Text = string.Format("{0:C}", double.Parse(dtInfo.Rows[0]["costPrice"].ToString()));
                    img.ImageUrl = dtInfo.Rows[0]["mainImageUrl"].ToString();
                    ViewState["urlOriginal"] = dtInfo.Rows[0]["UrlPrdOriginal"].ToString(); 
                    listarCategoriasProducto();
                    listarCategoriasMP();
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
                Session["prdCategorias"] = lblCategorias.Text;
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
                new BOProductos().listarCategoriasMP(ref dtCategoriasMP, string.Empty, int.Parse(Session["idUsuario"].ToString()));
                for (int i = 0; i < dtCategoriasMP.Rows.Count; i++)
                {
                    for (int j = 0; j < categoriasActuales.Length; j++)
                    {
                        if (dtCategoriasMP.Rows[i]["name"].ToString().Trim().Equals(categoriasActuales[j].Trim()))
                        {
                            lblCategoriasMP.Text += dtCategoriasMP.Rows[i]["name"].ToString() + ";";
                            lblIdsCategoriasMP.Text = dtCategoriasMP.Rows[i]["idCam"].ToString() + ";";
                        }
                    }
                }
                ViewState["categoriasMP"] = lblCategoriasMP.Text;
            }
        }
        private void listarCategoriasMP(string filtro)
        {
            if (validarPagina())
            {
                List<string> categoriasSincronizar = new List<string>();
                string[] categoriasActuales = lblCategoriasMP.Text.Split(';');
                DataTable dtCategoriasMP = new DataTable();
                new BOProductos().listarCategoriasMP(ref dtCategoriasMP, filtro, int.Parse(Session["idUsuario"].ToString()));
                for (int i = 0; i < dtCategoriasMP.Rows.Count; i++)
                {
                    for (int j = 0; j < categoriasActuales.Length; j++)
                    {
                        if (dtCategoriasMP.Rows[i]["name"].ToString().Equals(categoriasActuales[j]))
                        {
                            categoriasSincronizar.Add(dtCategoriasMP.Rows[i]["idCam"].ToString());
                            dtCategoriasMP.Rows.RemoveAt(i);
                            break;
                        }
                    }
                }
                grCategoriasMP.DataSource = dtCategoriasMP;
                grCategoriasMP.DataBind();
                ViewState["dtCategoriasMP"] = dtCategoriasMP;
                ViewState["categoriasHomologas"] = categoriasSincronizar;
            }
        }

        private void listarSubCategoriasMP(int idCategoria)
        {
            if (validarPagina())
            {
                DataTable dtSubCategoriasMP = new DataTable();
                new BOProductos().listarSubCategoriasMP(ref dtSubCategoriasMP, idCategoria, int.Parse(Session["idUsuario"].ToString()));
                grSubCategorias.DataSource = dtSubCategoriasMP;
                grSubCategorias.DataBind();
            }
        }

        protected void lbtnDescartar_Click(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                actualizarEstado(4);
                Response.Redirect("adminProductos.aspx");
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
                try
                {
                    if (string.IsNullOrEmpty(ViewState["categorias"].ToString()))
                        ClientScript.RegisterClientScriptBlock(Page.GetType(), "SinCategorias", string.Format("<script>alert('{0}');</script>", "La categoría del Marketplace no puede ser vacía"));
                    else
                    {
                        int id = 0;
                        BOProductos bop = new BOProductos();
                        int.TryParse(Session["idProducto"].ToString(), out id);
                        previsualizarCambios();
                        string idCategoria = lblIdsCategoriasMP.Text.Split(';')[0];
                        string res = bop.crearProductoMP(id, Session["prdNombre"].ToString(), Session["prdDescripcion"].ToString(), idCategoria, Session["idUsuario"].ToString(), ViewState["categorias"].ToString(), lblDescuento.Text);
                        int nuevoId;
                        if (int.TryParse(res, out nuevoId))
                        {
                            bop.agregarProductoMP(lblTienda.Text, Session["idProducto"].ToString(), nuevoId.ToString(), Session["idUsuario"].ToString());
                            actualizarEstado(2);
                            new BOAgregador().ejecutarSincronizador();
                        }
                        Response.Redirect("adminProductos.aspx");
                    }
                }
                catch (Exception ex)
                {
                    new BOAgregador().guardarError("Error al publicar", ex.Message);
                }
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
                listarCategoriasMP(txtBuscarCategorias.Text);
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
                string catAdicionales = string.Empty;
                if (!string.IsNullOrEmpty(ViewState["categorias"].ToString()))
                    catAdicionales = ViewState["categorias"].ToString();
                foreach (GridViewRow _row in grSubCategorias.Rows)
                {
                    int idSubCategoria;
                    int.TryParse(_row.Cells[0].Text, out idSubCategoria);
                    CheckBox chkSubCategoria = (CheckBox)_row.FindControl("chkSubCategoria");
                    if (chkSubCategoria.Checked)
                    {
                        catAdicionales += _row.Cells[0].Text + ";";
                    }
                }
                catAdicionales = catAdicionales.Substring(0, catAdicionales.Length - 1);
                ViewState["categorias"] = catAdicionales;
                listarCategoriasMP(string.Empty);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                if (ViewState["catOriginal"] != null)
                {
                    lblCategoriasMP.Text = ViewState["catOriginal"].ToString();
                    lblIdsCategoriasMP.Text = ViewState["idCatOriginal"].ToString();
                }
                lblCategoriasMP.Text = string.Empty;
                pnlCategorias.Visible = true;
                grCategoriasMP.Visible = true;
                grSubCategorias.Visible = false;
            }
        }

        protected void grCategoriasMP_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCategoria = 0;
            int.TryParse(grCategoriasMP.SelectedRow.Cells[1].Text, out idCategoria);
            lblIdsCategoriasMP.Text = idCategoria.ToString();
            lblCategoriasMP.Text = grCategoriasMP.SelectedRow.Cells[2].Text;
            grCategoriasMP.Visible = false;
            listarSubCategoriasMP(idCategoria);
            grSubCategorias.Visible = true;
            ViewState["categorias"] = idCategoria;
        }
    }
}