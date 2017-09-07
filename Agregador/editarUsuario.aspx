<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="editarUsuario.aspx.cs" Inherits="Agregador.editarUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">
                Administrador de usuarios <i class="fa fa-chevron-circle-right" aria-hidden="true"></i> Editar usuario
            </h1>
            <div class="navegacion">
                <div class="regresar"><a href="administrarUsuarios.aspx"><i class="fa fa-chevron-circle-left" aria-hidden="true"></i></a><a href="administrarUsuarios.aspx">Atrás</a></div>
            </div>
        </div>
    </div>
    <!-- /.row -->

    <div class="row">
        <div class="col-lg-12">
            <div class="login-panel panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Editar usuario</h3>
                </div>
                <div class="panel-body">
                    <fieldset>
                        <div class="form-group">
                            <asp:TextBox ID="txtNombres" runat="server" class="form-control" placeholder="Nombres"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtClave" runat="server" TextMode="Password" class="form-control" placeholder="Contraseña" ></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtCorreo" runat="server" type="email" class="form-control" placeholder="Usuario ó correo electrónico" autofocus></asp:TextBox>
                        </div>
                        <div class="form-group">
                        <div class="usuario-tipo">Rol de usuario</div>
                            <asp:DropDownList ID="ddlRoles" runat="server" class="form-control" name="Tipo de usuario" placeholder="Rol"></asp:DropDownList>
                        </div>
                        <br />
                         MarketPlaces asignados<br />
                        <asp:GridView ID="grmps" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" CssClass="mGrid" DataKeyNames="IdCuentaCam" GridLines="None">
                            <Columns>
                                <asp:TemplateField HeaderText="Asignada">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCuentaAct" runat="server" checked='<%# bool.Parse(Eval("Asignada").ToString()) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="idCuentaCam" HeaderStyle-ForeColor="White" HeaderText="ID"/>
                                <asp:BoundField DataField="nombrecuentacam" HeaderStyle-ForeColor="White" HeaderText="Nombre de la Cuenta" />
                            </Columns>
                        </asp:GridView>
                        <!-- Change this to a button or input when using this as a form -->
                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" class="btn btn-lg btn-success btn-block" OnClick="btnSalvar_Click"/>
                        <asp:Label ID="lblRespuesta" runat="server" Text=""></asp:Label>
                    </fieldset>
                </div>
             </div>
        </div>
    </div>
</asp:Content>
