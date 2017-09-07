using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Agregador
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {}

        private void iniciarSesion()
        {
            DataTable dtDatosUsuario = new DataTable();
            string claveEnc = new encriptarDatos().obtenerMD5(txtClave.Text);
            if (new BOUsuarios().iniciarSesion(ref dtDatosUsuario, txtUsuario.Text))
            {
                if (dtDatosUsuario.Rows.Count > 0)
                { 
                    if (dtDatosUsuario.Rows[0]["clave"].ToString() == claveEnc && bool.Parse(dtDatosUsuario.Rows[0]["activo"].ToString()) == true)
                    {
                        List<string> mpAsociados = new List<string>();
                        for (int i = 0; i < dtDatosUsuario.Rows.Count; i++)
                        {
                            mpAsociados.Add(dtDatosUsuario.Rows[i]["account_id"].ToString());
                        }
                        Session["idUsuario"] = dtDatosUsuario.Rows[0]["idUsuario"].ToString();
                        Session["Usuario"] = dtDatosUsuario.Rows[0]["nombres"].ToString();
                        Session["codRol"] = dtDatosUsuario.Rows[0]["codRol"].ToString();
                        Session["paginaActual"] = "login";
                        Session["mpPermitidos"] = mpAsociados;
                        Response.Redirect("Menu.aspx");
                    }
                    lblResultado.Text = "Clave no válida o usuario inactivo.";
                }
            else
                lblResultado.Text = "Usuario no válido";
            }
            else
            {
                lblResultado.Text = "Wops! Algo no sucedió bien";
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            iniciarSesion();
        }
        private void validarPermisos()
        {
            System.Data.DataTable dtRoles = new System.Data.DataTable();
            new BOUsuarios().rolesPorUsuario(int.Parse(Session["idUsuario"].ToString()), ref dtRoles);
            List<string> paginasPermitidas = new List<string>();
            for (int i = 0; i < dtRoles.Rows.Count; i++)
            {
                paginasPermitidas.Add(dtRoles.Rows[i]["direccion"].ToString());
                //lblIdUsuario.Text += string.Format("<a href='{0}'>{1}</a>", dtRoles.Rows[i]["direccion"], dtRoles.Rows[i]["direccion"]) + "<br/>";
            }
            Session["paginasPermitidas"] = paginasPermitidas;
        }
    }
}