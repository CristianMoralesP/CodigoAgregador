using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agregador
{
    public partial class recuperarClave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            iniciarSesion();
        }
        private void iniciarSesion()
        {
            System.Data.DataTable dtDatosUsuario = new System.Data.DataTable();
            if (new BOUsuarios().iniciarSesion(ref dtDatosUsuario, txtUsuario.Text))
            {
                if (dtDatosUsuario.Rows.Count > 0)
                {
                    string claveOriginal = new BOAgregador().generarClave();
                    string nvaClave = new encriptarDatos().obtenerMD5(claveOriginal);
                    string correo = dtDatosUsuario.Rows[0]["correo"].ToString();
                    BOUsuarios objUsuarios = new BOUsuarios();
                    if (objUsuarios.administrarUsuario(2, int.Parse(dtDatosUsuario.Rows[0]["idUsuario"].ToString()), dtDatosUsuario.Rows[0]["nombres"].ToString(), nvaClave, dtDatosUsuario.Rows[0]["correo"].ToString(), int.Parse(dtDatosUsuario.Rows[0]["codRol"].ToString())))
                    {
                        new MailClass().sendMail(correo, "Recuperación de clave", string.Format("Se generó automáticamente la clave {0} para su ingreso al sistema.", claveOriginal));
                        lblResultado.Text = "Por favor revise su bandeja";
                    }
                }
                else
                    lblResultado.Text = "No se encuentran usuarios con ese correo";
            }
            else
            {
                lblResultado.Text = "Wops! Algo no sucedió bien";
            }
        }
    }
}