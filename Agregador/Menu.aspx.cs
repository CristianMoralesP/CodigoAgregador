using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agregador
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUsuario"] != null)
            {
                validarPermisos();
                validarMensaje();
            }
        }

        private void validarPermisos()
        {
            System.Data.DataTable dtRoles = new System.Data.DataTable();
            new BOUsuarios().rolesPorUsuario(int.Parse(Session["idUsuario"].ToString()), ref dtRoles);
            List<string> paginasPermitidas = new List<string>();
            for (int i = 0; i < dtRoles.Rows.Count; i++)
            {
                paginasPermitidas.Add(dtRoles.Rows[i]["direccion"].ToString());
            }
            Session["paginasPermitidas"] = paginasPermitidas;
        }

        private void validarMensaje()
        {
            if (Request.QueryString["cambioClave"] != null)
            {
                if (Request.QueryString["cambioClave"].ToString() == "0")
                    lblMensaje.Text = "No se logró procesar su solicitud. Por favor intente más tarde";
            }
        }

        protected void btnSincronizarTodo_Click(object sender, EventArgs e)
        {
            new BOAgregador().ejecutarSincronizadorTodo();
        }
    }
}