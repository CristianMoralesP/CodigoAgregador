<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="administrarUsuarios.aspx.cs" Inherits="Agregador.administrarUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">
                Administrador de usuarios
            </h1>
            <div class="navegacion">
                <asp:TextBox ID="txtBuscar" runat="server" class="buscador" placeholder="Buscar por cualquier término "></asp:TextBox>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="estiloBotones" OnClick="btnBuscar_Click"/>
                <div class="regresar"><a href="Menu.aspx"><i class="fa fa-chevron-circle-left" aria-hidden="true"></i></a><a href="Menu.aspx">Atrás</a></div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="table-responsive">
                <asp:GridView ID="grUsuarios" runat="server" AutoGenerateColumns="False" DataKeyNames="idUsuario"  GridLines="None"  AllowPaging="True" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnRowEditing="grUsuarios_RowEditing" OnRowDeleting="grUsuarios_RowDeleting" AllowSorting="True" OnSorting="grUsuarios_Sorting" OnPageIndexChanging="grUsuarios_PageIndexChanging">
<AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="nombres" HeaderText="Nombres" SortExpression="nombres" HeaderStyle-ForeColor="White"/>
                        <asp:BoundField DataField="Rol" HeaderText="Rol" SortExpression="Rol" HeaderStyle-ForeColor="White"/>
                        <asp:BoundField DataField="correo" HeaderText="Correo" SortExpression="correo" HeaderStyle-ForeColor="White"/>
                        <asp:BoundField DataField="activo" HeaderText="Activo" SortExpression="activo" HeaderStyle-ForeColor="White"/>
                        <asp:CommandField ShowCancelButton="False" ShowEditButton="True" EditText="Editar" />
                        <asp:CommandField ShowDeleteButton="True" DeleteText="Eliminar"/>
                    </Columns>
<PagerStyle CssClass="pgr"></PagerStyle>
                </asp:GridView>
                <div align="right">
                    <asp:Button runat="server" ID="btnGenerar" text="Descargar a Excel" OnClick="btnGenerar_Click" CssClass="estiloBotones"/>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-lg-3">
            <a href="crearUsuario.aspx" class="btn btn-lg btn-success btn-block"><i class="fa fa-user" aria-hidden="true"></i> Agregar usuario</a>
        </div>
    </div>
</asp:Content>
