<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminUsuarios.aspx.cs" Inherits="Agregador.adminUsuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Administrar usuarios</h1>

            <br />
            <asp:GridView ID="grUsuarios" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="nombres" HeaderText="Nombres"/>
                    <asp:BoundField DataField="correo" HeaderText="Correo"/>
                    <asp:TemplateField HeaderText="Activo">
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkAct" checked='<%# bool.Parse(Eval("activo").ToString()) %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <br />
            <hr width="100%" />
            <br />
            Crear usuario<br />
            Nombres:
            <asp:TextBox ID="txtNombres" runat="server"></asp:TextBox>
            <br />
            Clave:
            <asp:TextBox ID="txtClave" runat="server"></asp:TextBox>
            <br />
            Correo:
            <asp:TextBox ID="txtCorreo" runat="server"></asp:TextBox>
            <br />
            Rol:&nbsp;
            <asp:TextBox ID="txtRol" runat="server"></asp:TextBox>

            <br />
            <asp:Button ID="btnCrear" runat="server" Text="Crear" OnClick="btnCrear_Click" />
            <br />
            <asp:Label ID="lblRespuesta" runat="server" Text="Label"></asp:Label>
            <hr width="100%" />
            <br />
            Editar usuario
            <br />
             idUsuario:
            <asp:TextBox ID="txtEdidUsuario" runat="server"></asp:TextBox>
            <br />
            Nombres:
            <asp:TextBox ID="txtEdNombres" runat="server"></asp:TextBox>
            <br />
            Clave:
            <asp:TextBox ID="txtEdClave" runat="server"></asp:TextBox>
            <br />
            Correo:
            <asp:TextBox ID="txtEdCorreo" runat="server"></asp:TextBox>
            <br />
            Rol:&nbsp;
            <asp:TextBox ID="txtEdRol" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="txtEditar" runat="server" Text="Editar" OnClick="txtEditar_Click"/>
            <br />
            <asp:Label ID="lblResponse" runat="server" Text="Label"></asp:Label>
            <hr width="100%" />
            Eliminar usuario
            <br />
             idUsuario:
            <asp:TextBox ID="txtbridUsuario" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click"/>
            <br />
            <asp:Label ID="lblRest" runat="server" Text="Label"></asp:Label>
        </div>
    </form>
</body>
</html>
