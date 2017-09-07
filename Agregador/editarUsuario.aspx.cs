using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agregador
{
    public partial class editarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                if (!Page.IsPostBack)
                {
                    listarRoles();
                    cargarUsuario();
                    Session["paginaActual"] = "Administración de Usuarios";
                }
            }
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
            ddlRoles.DataBind();
        }
        private void cargarUsuario()
        {
            if (validarPagina())
            {
                DataTable dtUsuario = new DataTable();
                int id;
                int.TryParse(Session["idUsuarioEditar"].ToString(), out id);
                new BOUsuarios().listarUsuarioPorId(ref dtUsuario, id);
                if (dtUsuario.Rows.Count > 0)
                {
                    txtNombres.Text = dtUsuario.Rows[0]["nombres"].ToString();
                    txtClave.Text = dtUsuario.Rows[0]["clave"].ToString();
                    txtCorreo.Text =  dtUsuario.Rows[0]["correo"].ToString();
                    ddlRoles.SelectedValue = dtUsuario.Rows[0]["idRol"].ToString();
                }
                listarmps(id);
            }
        }
        private void listarmps(int id)
        {
            DataTable dtMPs = new DataTable();
            new BOCuentas().listarMPUsuario(ref dtMPs, id);
            grmps.DataSource = dtMPs;
            grmps.DataBind();
            ViewState["dtMPs"] = dtMPs;
        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                BOUsuarios objUsuarios = new BOUsuarios();
                string nvaClave = string.Empty;
                int idUserEditar = int.Parse(Session["idUsuarioEditar"].ToString());
                if (!string.IsNullOrEmpty(txtClave.Text))
                    nvaClave = new encriptarDatos().obtenerMD5(txtClave.Text);
                if (objUsuarios.administrarUsuario(2, idUserEditar, txtNombres.Text, nvaClave, txtCorreo.Text, int.Parse(ddlRoles.SelectedValue)))
                {
                    foreach (GridViewRow _row in grmps.Rows)
                    {
                        CheckBox chkm = new CheckBox();
                        chkm = (CheckBox)_row.FindControl("chkCuentaAct");
                        new BOUsuarios().editarMPsUsuario(idUserEditar, _row.Cells[1].Text, chkm.Checked);
                    }
                    Response.Redirect("administrarUsuarios.aspx");
                }
                else
                    lblRespuesta.Text = "No se logró actualizar la información del usuario";   
            }
        }
    }
}