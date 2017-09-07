using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agregador
{
    public partial class tiendas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (validarPagina())
                {
                    listarTiendas(string.Empty);
                    Session["paginaActual"] = "Administración de tiendas";
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

        private void listarTiendas(string termino)
        {
            DataTable dtTiendas = new DataTable();
            new BOTiendas().listarCuentas(ref dtTiendas, termino, int.Parse(Session["idUsuario"].ToString()));
            grTiendas.DataSource = dtTiendas;
            grTiendas.DataBind();
            ViewState["dtTiendas"] = dtTiendas;
            if (dtTiendas.Rows.Count == 0)
                lblAviso.Text = "No hay cuentas asociadas a su marketplace asignado";
            else
                lblAviso.Text = string.Empty;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                string correoCuenta = string.Empty;
                int i = 0;
                foreach (GridViewRow _row in grTiendas.Rows)
                {
                    int idCuenta;
                    int.TryParse(grTiendas.DataKeys[i].Value.ToString(), out idCuenta);
                    CheckBox chkCuenta = (CheckBox)_row.FindControl("chkCuentaAct");
                    bool estadoActual = validaCambioEstado(idCuenta);
                    if (!(estadoActual == chkCuenta.Checked))
                    {
                        //Cuenta de pruebas
                        if (idCuenta == 5777)
                            new BOTiendas().cambiarEstadoCuenta(_row.Cells[2].Text, chkCuenta.Checked, idCuenta, _row.Cells[5].Text);
                    }
                    //q hago con el string?
                    i++;
                }
                listarTiendas(string.Empty);
            }
        }

        private bool validaCambioEstado(int idCuenta)
        {
            bool flag = false;
            DataView view = new DataView((DataTable)ViewState["dtTiendas"]);
            view.RowFilter = "IdCuentaCam = " + idCuenta;
            DataTable dtFiltrado = view.ToTable();
            if (dtFiltrado.Rows.Count > 0)
                bool.TryParse(dtFiltrado.Rows[0]["estadoCuentaCam"].ToString(), out flag);
            return flag;
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
        public void btnGenerar_Click(object sender, EventArgs e)
        {
            generarExcel((DataTable)ViewState["dtTiendas"]);
        }

        protected void grTiendas_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (validarPagina())
            {
                DataTable dtTiendas = (DataTable)ViewState["dtTiendas"];
                dtTiendas.DefaultView.Sort = e.SortExpression + " ASC";
                grTiendas.DataSource = dtTiendas;
                grTiendas.DataBind();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                listarTiendas(txtBuscar.Text);
            }
        }
        protected void grTiendas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (validarPagina())
            {
                grTiendas.PageIndex = e.NewPageIndex;
                grTiendas.DataSource = (DataTable)ViewState["dtTiendas"];
                grTiendas.DataBind();
            }
        }
    }
}