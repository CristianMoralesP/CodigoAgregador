<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="tiendas.aspx.cs" Inherits="Agregador.tiendas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">
                Administración de tiendas
            </h1>
            <div class="navegacion">
                <asp:TextBox ID="txtBuscar" runat="server" class="buscador" placeholder="Buscar por cualquier término"></asp:TextBox>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="estiloBotones" OnClick="btnBuscar_Click"/>
                <div class="regresar"><a href="Menu.aspx"><i class="fa fa-chevron-circle-left" aria-hidden="true"></i></a><a href="Menu.aspx">Atrás</a></div>
            </div>
        </div>
    </div>
    <br />
    <asp:GridView ID="grTiendas" runat="server" AutoGenerateColumns="false" DataKeyNames="IdCuentaCam"  GridLines="None"  AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnSorting="grTiendas_Sorting" OnPageIndexChanging="grTiendas_PageIndexChanging">
    <Columns>
        <asp:HyperLinkField DataNavigateUrlFields="displaysiteurl" HeaderText="Nombre de la Tienda" Text="Ir" DataTextField="sitename" Target="_blank" SortExpression="sitename" HeaderStyle-ForeColor="White"/>
        <asp:BoundField  DataField="companyname" HeaderText="Nombre de la Empresa" SortExpression="companyname" HeaderStyle-ForeColor="White"/>
        <asp:BoundField  DataField="nombrecuentacam" HeaderText="Nombre de la Cuenta" SortExpression="nombrecuentacam" HeaderStyle-ForeColor="White"/>
        <asp:BoundField  DataField="correocuentacam" HeaderText="Administrador" SortExpression="correocuentacam" HeaderStyle-ForeColor="White"/>
        <asp:BoundField  DataField="phone" HeaderText="Teléfono" SortExpression="phone" HeaderStyle-ForeColor="White"/>
        <asp:BoundField  DataField="EstadoCuenta" HeaderText="Estado" SortExpression="EstadoCuenta" HeaderStyle-ForeColor="White"/>
        <asp:TemplateField HeaderText="">
            <ItemTemplate>
                <asp:CheckBox runat="server" ID="chkCuentaAct" checked='<%# bool.Parse(Eval("estadoCuentaCam").ToString()) %>'/>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    </asp:GridView>
    <asp:Label runat="server" ID="lblAviso"></asp:Label>
     <div align="right">
        <asp:Button runat="server" ID="btnGenerar" text="Descargar a Excel" OnClick="btnGenerar_Click" CssClass="estiloBotones"/>
    </div>
    <br />
</asp:Content>
