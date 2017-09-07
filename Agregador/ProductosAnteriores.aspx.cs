using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agregador
{
    public partial class ProductosAnteriores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (validarPagina())
                {
                    listarProductos(string.Empty);
                    Session["paginaActual"] = "Administración de publicación y productos";
                    Session["paginaAnterior"] = "ProductosAnteriores";
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

        private void listarProductos(string termino)
        {
            DataTable dtProductos = new DataTable();
            new BOProductos().listarProductos(ref dtProductos, 3, 0, termino);
            grProductos.DataSource = dtProductos;
            grProductos.DataBind();
            ViewState["productos"] = dtProductos;
        }

        protected void grProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idProducto;
            int.TryParse(grProductos.DataKeys[grProductos.SelectedIndex].Value.ToString(), out idProducto);
            Session["idProducto"] = idProducto;
            Response.Redirect("detalleProducto.aspx");
        }

        protected void grProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (validarPagina())
            {
                grProductos.PageIndex = e.NewPageIndex;
                grProductos.DataSource = (DataTable)ViewState["productos"];
                grProductos.DataBind();
            }
        }
        public void btnGenerar_Click(object sender, EventArgs e)
        {
            generarExcel((DataTable)ViewState["productos"]);
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
        protected void grProductos_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (validarPagina())
            {
                DataTable dtReporte = (DataTable)ViewState["productos"];
                dtReporte.DefaultView.Sort = e.SortExpression + " ASC";
                grProductos.DataSource = dtReporte;
                grProductos.DataBind();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                listarProductos(txtBuscar.Text);
            }
        }
    }
}