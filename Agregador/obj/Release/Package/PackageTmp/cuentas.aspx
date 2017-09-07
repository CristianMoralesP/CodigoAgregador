<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="cuentas.aspx.cs" Inherits="Agregador.cuentas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">
                Administrador de cuentas
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
                <asp:GridView ID="grCuentas" runat="server" AutoGenerateColumns="False" DataKeyNames="idCuentaCam"  GridLines="None"  AllowPaging="True" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnSorting="grCuentas_Sorting" OnPageIndexChanging="grCuentas_PageIndexChanging">
                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="nombrecuentaCam" HeaderText="Nombres" SortExpression="nombrecuentaCam" HeaderStyle-ForeColor="White"/>
                        <asp:BoundField DataField="idCuentaCam" HeaderText="ID Cuenta" SortExpression="idCuentaCam" HeaderStyle-ForeColor="White"/>
                        <asp:BoundField DataField="correoCuentaCam" HeaderText="Correo" SortExpression="correoCuentaCam" HeaderStyle-ForeColor="White"/>
                        <asp:BoundField DataField="fechaCreacionAgregador" HeaderText="Activo" SortExpression="fechaCreacionAgregador" HeaderStyle-ForeColor="White"/>
                        <asp:BoundField DataField="MarketPlace" HeaderText="Activo" SortExpression="MarketPlace" HeaderStyle-ForeColor="White"/>
                    </Columns>
                    <PagerStyle CssClass="pgr"></PagerStyle>
                </asp:GridView>
                <div align="right">
                    <asp:Button runat="server" ID="btnGenerar" text="Descargar a Excel" OnClick="btnGenerar_Click" CssClass="estiloBotones"/>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
    <div class="col-lg-3">
        <a href="agregarCuenta.aspx" class="btn btn-lg btn-success btn-block"><i class="fa fa-check-circle-o" aria-hidden="true"></i> Agregar cuenta</a>
    </div>
    </div>
</asp:Content>
