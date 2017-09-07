using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agregador
{
    public partial class ofertas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                if (!Page.IsPostBack)
                {
                    Session["paginaActual"] = "Administración de Ofertas";
                    Session["paginaAnterior"] = "Administración de publicación y productos";
                    Label1.Text = "";
                    listarCategoriasMP();
                    chkTodasSC.Visible = false;
                }
            }
            else
                Response.Redirect("login.aspx");
        }
        private bool validarPagina()
        {
            return new BOUsuarios().paginaPermitida(Request.Url.AbsolutePath.ToString());
        }

        private void listarCategoriasMP()
        {
            if (validarPagina())
            {
                DataTable dtCategoriasMP = new DataTable();
                new BOProductos().listarCategoriasMP(ref dtCategoriasMP, "", int.Parse(Session["idUsuario"].ToString()));
                grCategoriasMP.DataSource = dtCategoriasMP;
                grCategoriasMP.DataBind();
                if (dtCategoriasMP.Rows.Count == 0)
                {
                    lblMsj.Text = "No hay subcategorias asociadas";
                    lblMsj.Visible = true;
                }
                else
                {
                    lblMsj.Text = "";
                    lblMsj.Visible = false;
                }
                txtFechaInicial.Focus();
            }
        }

        private void listarSubCategoriasMP(int id)
        {
            if (validarPagina())
            {
                DataTable dtSubCategoriasMP = new DataTable();
                new BOProductos().listarSubCategoriasMP(ref dtSubCategoriasMP, id, int.Parse(Session["idUsuario"].ToString()));
                if (dtSubCategoriasMP.Rows.Count > 0)
                {
                    grSubCategorias.DataSource = dtSubCategoriasMP;
                    chkTodasSC.Visible = true;
                }
                else
                {
                    grSubCategorias.DataSource = null;
                    chkTodasSC.Visible = false;
                }
                grSubCategorias.DataBind();
                ViewState["dtSubCategoriasMP"] = dtSubCategoriasMP;
            }
        }

        protected void grCategorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (validarPagina())
            {
                /*grCategorias.PageIndex = e.NewPageIndex;
                grCategorias.DataSource = (DataTable)ViewState["cantidadAliados"];
                grCategorias.DataBind();*/

            }
        }
        protected void grCategorias_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (validarPagina())
            {
                /*DataTable dtReporte = (DataTable)ViewState["cantidadAliados"];
                dtReporte.DefaultView.Sort = e.SortExpression + " ASC";
                grCategorias.DataSource = dtReporte;
                grCategorias.DataBind();*/
            }
        }
        protected void grSubCategoriasMP_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (validarPagina())
            {
                grCategoriasMP.PageIndex = e.NewPageIndex;
                grCategoriasMP.DataSource = (DataTable)ViewState["dtSubCategoriasMP"];
                grCategoriasMP.DataBind();
            }
        }
        protected void grCategoriasMP_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCategoria = 0;
            int.TryParse(grCategoriasMP.SelectedRow.Cells[1].Text, out idCategoria);
            listarSubCategoriasMP(idCategoria);
        }
        protected void btnCrear_Click(object sender, EventArgs e)
        {
            BOAgregador obj = new BOAgregador();
            BOProductos bop = new BOProductos();
            DateTime inicial = DateTime.Parse(txtFechaInicial.Text);
            DateTime final = DateTime.Parse(txtFechaFinal.Text);
            string res = string.Empty;
            string fechaIni = obj.fechaFormateada(inicial.Year.ToString(), inicial.Month.ToString(), inicial.Day.ToString());
            string fechaFin = obj.fechaFormateada(final.Year.ToString(), final.Month.ToString(), final.Day.ToString());
            foreach (GridViewRow _row in grSubCategorias.Rows)
            {
                CheckBox chkm = new CheckBox();
                chkm = (CheckBox)_row.FindControl("chkSubCategoria");
                if (chkm.Checked)
                {
                    
                    res = new BOProductos().crearProductoMP(_row.Cells[1].Text, res, fechaIni, fechaFin);
                }
            }
            foreach (GridViewRow _row in grCategoriasMP.Rows)
            {
                CheckBox chkm = new CheckBox();
                chkm = (CheckBox)_row.FindControl("chkCategoria");
                if (chkm.Checked)
                {
                    res = new BOProductos().crearProductoMP(_row.Cells[1].Text, res, fechaIni, fechaFin);
                }
            }
            if (res.Contains(':'))
            {
                if (new BOProductos().crearLoteOferta(fechaIni, fechaFin, Session["idUsuario"].ToString(), res))
                    lblMsj.Text = "Productos creados";
                else
                    lblMsj.Text = string.Format("Hubo un error al crear la oferta.Por favor contacte al administrador.Código OfX{0}{1}{2}{3}{4}", inicial.Year + inicial.Month + inicial.Day + inicial.Hour + inicial.Minute);
            }
            else
                lblMsj.Text = "No se lograron crear los productos. Por favor verifique con el administrador.";
            lblMsj.Visible = true;
        }

        protected void chkTodas_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow _row in grCategoriasMP.Rows)
            {
                CheckBox chkm = new CheckBox();
                chkm = (CheckBox)_row.FindControl("chkCategoria");
                chkm.Checked = chkTodas.Checked;
            }
        }

        protected void chkTodasSC_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow _row in grSubCategorias.Rows)
            {
                CheckBox chkm = new CheckBox();
                chkm = (CheckBox)_row.FindControl("chkSubCategoria");
                chkm.Checked = chkTodasSC.Checked;
            }
        }
    }
}