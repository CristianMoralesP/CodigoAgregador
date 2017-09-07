using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agregador
{
    public partial class actualizarInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                if (!Page.IsPostBack)
                {
                    cargarUsuario();
                    Session["paginaActual"] = "Administración de Usuarios";
                }
            }
        }
        private bool validarPagina()
        {
            return new BOUsuarios().paginaPermitida(Request.Url.AbsolutePath.ToString());
        }
        private void cargarUsuario()
        {
            if (validarPagina())
            {
                DataTable dtUsuario = new DataTable();
                int id;
                int.TryParse(Session["idUsuario"].ToString(), out id);
                new BOUsuarios().listarUsuarioPorId(ref dtUsuario, id);
                if (dtUsuario.Rows.Count > 0)
                {
                    txtNombres.Text = dtUsuario.Rows[0]["nombres"].ToString();
                    txtClave.Text = dtUsuario.Rows[0]["clave"].ToString();
                    txtCorreo.Text = dtUsuario.Rows[0]["correo"].ToString();
                }
            }
        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                BOUsuarios objUsuarios = new BOUsuarios();
                string nvaClave = string.Empty;
                if (!string.IsNullOrEmpty(txtClave.Text))
                    nvaClave = new encriptarDatos().obtenerMD5(txtClave.Text);
                if (objUsuarios.administrarUsuario(2, int.Parse(Session["idUsuario"].ToString()), txtNombres.Text, nvaClave, txtCorreo.Text, int.Parse(Session["codRol"].ToString())))
                    Response.Redirect("login.aspx");
                else
                    Response.Redirect("Menu.aspx?cambioClave=0");
            }
        }
    }
}