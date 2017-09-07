using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agregador
{
    public partial class administrarUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                cargarUsuarios(string.Empty);
                Session["paginaActual"] = "Administración de Usuarios";
            }
            else
                Response.Redirect("Home.aspx");
        }

        private bool validarPagina()
        {
            return new BOUsuarios().paginaPermitida(Request.Url.AbsolutePath.ToString());
        }
        private void cargarUsuarios(string termino)
        {
            if (validarPagina())
            {
                System.Data.DataTable dtUsuarios = new System.Data.DataTable();
                new BOUsuarios().listarUsuarios(ref dtUsuarios, termino);
                grUsuarios.DataSource = dtUsuarios;
                grUsuarios.DataBind();
                ViewState["dtUsuarios"] = dtUsuarios;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                cargarUsuarios(txtBuscar.Text);
            }
        }

        public void btnGenerar_Click(object sender, EventArgs e)
        {
            generarExcel((DataTable)ViewState["dtUsuarios"]);
        }

        private void generarExcel(System.Data.DataTable dtInfo)
        {
            if (dtInfo.Rows.Count > 0)
            {
                string filename = "Rpt" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + ".xls";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dtInfo;
                dgGrid.DataBind();
                
                dgGrid.RenderControl(hw);
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentEncoding = System.Text.Encoding.Default;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                this.EnableViewState = false;
                Response.Write(tw.ToString());
                Response.End();
            }
        }
        protected void grUsuarios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (validarPagina())
            {
                int pos;
                int idUsuarioEditar;
                int.TryParse(e.NewEditIndex.ToString(), out pos);
                int.TryParse(grUsuarios.DataKeys[pos].Value.ToString(), out idUsuarioEditar);
                Session["idUsuarioEditar"] = idUsuarioEditar;
                Response.Redirect("editarUsuario.aspx");
            }
        }

        protected void grUsuarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (validarPagina())
            {
                int idUsuarioEliminar;
                int.TryParse(grUsuarios.DataKeys[int.Parse(e.RowIndex.ToString())].Value.ToString(), out idUsuarioEliminar);
                BOUsuarios objUsuarios = new BOUsuarios();
                objUsuarios.administrarUsuario(3, idUsuarioEliminar, string.Empty, string.Empty, string.Empty, 0);
                cargarUsuarios(string.Empty);
            }
        }

        protected void grUsuarios_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (validarPagina())
            {
                DataTable dtUsuarios = (DataTable)ViewState["dtUsuarios"];
                dtUsuarios.DefaultView.Sort = e.SortExpression + " ASC";
                grUsuarios.DataSource = dtUsuarios;
                grUsuarios.DataBind();
            }
        }

        protected void grUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (validarPagina())
            {
                grUsuarios.PageIndex = e.NewPageIndex;
                grUsuarios.DataSource = (DataTable)ViewState["dtUsuarios"];
                grUsuarios.DataBind();
            }
        }
    }
}