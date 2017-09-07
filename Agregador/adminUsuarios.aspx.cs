using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agregador
{
    public partial class adminUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (validarPagina())
                cargarUsuarios();
            else
                Response.Redirect("Menu.aspx");
        }

        private bool validarPagina()
        {
            return new BOUsuarios().paginaPermitida(Request.Url.AbsolutePath.ToString());
        }
        private void cargarUsuarios()
        {
            /*
            System.Data.DataTable dtUsuarios = new System.Data.DataTable();
            new BOUsuarios().listarUsuarios(ref dtUsuarios);
            grUsuarios.DataSource = dtUsuarios;
            grUsuarios.DataBind();
            */
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                BOUsuarios objUsuarios = new BOUsuarios();
                if (objUsuarios.administrarUsuario(1, 0, txtNombres.Text, new encriptarDatos().obtenerMD5(txtClave.Text), txtCorreo.Text, int.Parse(txtRol.Text)))
                    lblRespuesta.Text = "Usuario creado";
                else
                    lblRespuesta.Text = "No se logró crear el usuario";
            }
            else
                Response.Redirect("login.aspx");
        }

        protected void txtEditar_Click(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                BOUsuarios objUsuarios = new BOUsuarios();
                if (objUsuarios.administrarUsuario(2, int.Parse(txtEdidUsuario.Text), txtEdNombres.Text, new encriptarDatos().obtenerMD5(txtEdClave.Text), txtEdCorreo.Text, int.Parse(txtEdRol.Text)))
                    lblResponse.Text = "Usuario editado";
                else
                    lblResponse.Text = "No se logró editar el usuario";
            }
            else
                Response.Redirect("login.aspx");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (validarPagina())
            {
                BOUsuarios objUsuarios = new BOUsuarios();
                if (objUsuarios.administrarUsuario(3, int.Parse(txtbridUsuario.Text), string.Empty, string.Empty, string.Empty, 0))
                    lblRest.Text = "Usuario eliminado";
                else
                    lblRest.Text = "No se logró eliminar el usuario";
            }
            else
                Response.Redirect("login.aspx");
        }
    }
}