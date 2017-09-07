using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace Agregador
{
    public class BOUsuarios
    {
        DAOUsuarios objUsuarios = new DAOUsuarios();
        public bool iniciarSesion(ref DataTable dtDatosUsuario, string usuario)
        {
            return objUsuarios.iniciarSesion(ref dtDatosUsuario, usuario);
        }

        public void rolesPorUsuario(int idUsuario, ref DataTable dtRoles)
        {
            objUsuarios.rolesPorUsuario(idUsuario, ref dtRoles);
        }

        public void listarRoles(ref DataTable dtRoles)
        {
            objUsuarios.listarRoles(ref dtRoles);
        }

        public void listarUsuarios(ref DataTable dtUsuarios, string termino)
        {
            objUsuarios.listarUsuarios(ref dtUsuarios, termino);
        }

        public void listarUsuarioPorId(ref DataTable dtUsuario, int id)
        {
            objUsuarios.listarUsuarioPorId(ref dtUsuario, id);
        }

        public bool validarSesion()
        {
            return HttpContext.Current.Session["idUsuario"] != null;
        }
        public bool paginaPermitida(string paginaValidar)
        {
            try
            {
                if (validarSesion())
                {
                    string[] ruta = paginaValidar.Split('/');
                    string nombrePagina = ruta[ruta.Length - 1];
                    List<string> paginasPermitidas = (List<string>)HttpContext.Current.Session["paginasPermitidas"];
                    return paginasPermitidas.Contains(nombrePagina);
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                objUsuarios.logErrorApp("pp", e.Message);
                return false;
            }
        }
        public bool administrarUsuario(int tipoTran, int idUsuario, string nombres, string clave, string correo, int codRol)
        {
            return objUsuarios.administrarUsuario(tipoTran, idUsuario, nombres, clave, correo, codRol);
        }
        public int crearUsuario(string nombres, string clave, string correo, int codRol)
        {
            return objUsuarios.crearUsuario(nombres, clave, correo, codRol);
        }
        public bool asignarMPsUsuario(int idUsuario, string cuenta)
        {
            return objUsuarios.asignarMPsUsuario(idUsuario, cuenta);
        }

        public bool editarMPsUsuario(int idUsuario, string cuenta, bool activo)
        {
            return objUsuarios.editarMPsUsuario(idUsuario, cuenta, activo);
        }

        public string imprimirAlerta(string mensaje)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script>");
            sb.Append("alert(");
            sb.Append(mensaje.Replace("\"", "'"));
            sb.Append(")");
            sb.Append("</script>");
            return sb.ToString();
        }
    }
}