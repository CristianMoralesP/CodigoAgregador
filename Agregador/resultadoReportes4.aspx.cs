using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agregador
{
    public partial class resultadoReportes4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (validarPagina())
                {

                    Session["paginaActual"] = "Administración de Reportes";
                    Session["paginaAnterior"] = "ProductosNuevos";
                    Label1.Text = "";
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

        private void listarValorTransaccion(DateTime fecini, DateTime fecfin)
        {
            DataTable dtValorTransaccion = new DataTable();
            new BOReportes().listarValorTransaccion(ref dtValorTransaccion, fecini, fecfin, int.Parse(Session["idUsuario"].ToString()));
            grValorTransaccion.DataSource = dtValorTransaccion;
            grValorTransaccion.DataBind();
            ViewState["ValorTransaccion"] = dtValorTransaccion;
        }

        protected void grValorTransaccion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (validarPagina())
            {
                grValorTransaccion.PageIndex = e.NewPageIndex;
                grValorTransaccion.DataSource = (DataTable)ViewState["ValorTransaccion"];
                grValorTransaccion.DataBind();

            }
        }

        protected void btnBuscar_Click1(object sender, EventArgs e)
        {

            string fechaInicio;
            string fechafinal;
            DateTime fecIni;
            DateTime fecFin;

            fechaInicio = txtFechaInicial.Text;
            fechafinal = txtFechaFinal.Text;

            if ((DateTime.TryParse(fechaInicio, out fecIni)) && (DateTime.TryParse(fechafinal, out fecFin)))
            {
                if (fecFin > fecIni)
                {
                    Label1.Text = "";
                    listarValorTransaccion(fecIni, fecFin);
                }
                else
                {
                    Label1.Text = "La fecha de fin debe ser mayor que la fecha de inicio";
                }
            }
            else
            {
                Label1.Text = "Valide que las fechas sean correctas";
            }
        }
        public void btnGenerar_Click(object sender, EventArgs e)
        {
            generarExcel((DataTable)ViewState["ValorTransaccion"]);
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
        protected void grValorTransaccion_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (validarPagina())
            {
                DataTable dtReporte = (DataTable)ViewState["ValorTransaccion"];
                dtReporte.DefaultView.Sort = e.SortExpression + " ASC";
                grValorTransaccion.DataSource = dtReporte;
                grValorTransaccion.DataBind();
            }
        }
    }
}