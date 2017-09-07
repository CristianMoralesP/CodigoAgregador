using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agregador
{
    public partial class cuentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                cargarCuentas(string.Empty);
                Session["paginaActual"] = "Administración de Cuentas";
            }
            else
                Response.Redirect("Menu.aspx");

        }
        private bool validarPagina()
        {
            return new BOUsuarios().paginaPermitida(Request.Url.AbsolutePath.ToString());
        }

        private void cargarCuentas(string termino)
        {
            DataTable dtCuentas = new DataTable();
            new BOCuentas().listarCuentasAgregador(ref dtCuentas, termino);
            grCuentas.DataSource = dtCuentas;
            grCuentas.DataBind();
            ViewState["dtCuentas"] = dtCuentas;
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                cargarCuentas(txtBuscar.Text);
            }
        }
        protected void grCuentas_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (validarPagina())
            {
                DataTable dtUsuarios = (DataTable)ViewState["dtCuentas"];
                dtUsuarios.DefaultView.Sort = e.SortExpression + " ASC";
                grCuentas.DataSource = dtUsuarios;
                grCuentas.DataBind();
            }
        }
        public void btnGenerar_Click(object sender, EventArgs e)
        {
            generarExcel((DataTable)ViewState["dtCuentas"]);
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

        protected void grCuentas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (validarPagina())
            {
                grCuentas.PageIndex = e.NewPageIndex;
                grCuentas.DataSource = (DataTable)ViewState["dtCuentas"];
                grCuentas.DataBind();
            }
        }
    }
}