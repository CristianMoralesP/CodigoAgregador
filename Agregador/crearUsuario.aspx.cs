using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agregador
{
    public partial class crearUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                if (!Page.IsPostBack)
                {
                    listarRoles();
                    listarMPS();
                }
            }
            else
                Response.Redirect("login.aspx");
        }

        private bool validarPagina()
        {
            return new BOUsuarios().paginaPermitida(Request.Url.AbsolutePath.ToString());
        }

        private void listarRoles()
        {
            DataTable dtRoles = new DataTable();
            new BOUsuarios().listarRoles(ref dtRoles);
            ddlRoles.DataSource = dtRoles;
            ddlRoles.DataTextField = "nombre";
            ddlRoles.DataValueField = "idRol";
            ddlRoles.Items.Add(new ListItem("--Seleccione--", "0"));
            ddlRoles.DataBind();
        }

        private void listarMPS()
        {
            DataTable dtMPs = new DataTable();
            new BOCuentas().listarMps(ref dtMPs);
            grmps.DataSource = dtMPs;
            grmps.DataBind();
        }
        protected void btnCrear_Click(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                BOUsuarios objUsuarios = new BOUsuarios();
                int id = objUsuarios.crearUsuario(txtNombres.Text, new encriptarDatos().obtenerMD5(txtClave.Text), txtCorreo.Text, int.Parse(ddlRoles.SelectedValue));
                if (id != 0)
                {
                    foreach (GridViewRow _row in grmps.Rows)
                    {
                        CheckBox chkm = new CheckBox();
                        chkm = (CheckBox)_row.FindControl("chkCuentaAct");
                        if (chkm.Checked)
                        {
                            new BOUsuarios().asignarMPsUsuario(id, _row.Cells[1].Text);
                        }
                    }
                    lblRespuesta.Text = "Usuario creado";
                    Response.Redirect("administrarUsuarios.aspx");
                }
                else
                    lblRespuesta.Text = "No se logró crear el usuario";
            }
            else
                Response.Redirect("login.aspx");
        }
    }
}