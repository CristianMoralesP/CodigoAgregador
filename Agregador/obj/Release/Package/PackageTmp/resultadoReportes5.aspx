<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="resultadoReportes5.aspx.cs" Inherits="Agregador.resultadoReportes5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">
                Reportes <i class="fa fa-chevron-circle-right" aria-hidden="true"></i> SKU's por Venta
            </h1>
            <div class="navegacion">
                <div class="filtro-fechas">
                    <div class="titulo-filtro">Filtrar por fechas <i class="fa fa-chevron-circle-right" aria-hidden="true"></i></div>
                    <asp:TextBox runat="server" TextMode="Date" ID="txtFechaInicial"></asp:TextBox>
                    Fecha Final
                    <asp:TextBox runat="server" TextMode="Date" ID="txtFechaFinal"></asp:TextBox>                           
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="estiloBotones" OnClick="btnBuscar_Click1"/>
                </div>
                <div class="regresar"><a href="adminReportes.aspx"><i class="fa fa-chevron-circle-left" aria-hidden="true"></i></a><a href="adminReportes.aspx">Atrás</a></div>
            </div>
        </div>
    </div>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <div class="row">
        <asp:GridView ID="grSkuVenta" runat="server" DataKeyNames="id" AutoGenerateColumns="false" GridLines="None"  AllowPaging="true" PageSize="50" OnPageIndexChanging="grSkuVenta_PageIndexChanging" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnSorting="grSkuVenta_Sorting">
            <Columns>
                <asp:BoundField HeaderText="Nombre Aliado" DataField="companyname" SortExpression="companyname" HeaderStyle-ForeColor="White"/>
                <asp:BoundField HeaderText="Nombre Cuenta" DataField="nombreCuentaCam" SortExpression="nombreCuentaCam" HeaderStyle-ForeColor="White"/>
                <asp:BoundField HeaderText="Nombre Tienda" DataField="sitename" SortExpression="sitename" HeaderStyle-ForeColor="White"/>
                <asp:BoundField HeaderText="Cantidad Items Vendidos" DataField="cantidad" SortExpression="cantidad" HeaderStyle-ForeColor="White"/>
                <asp:BoundField HeaderText="Cantidad de Ordenes" DataField="valor" SortExpression="valor" HeaderStyle-ForeColor="White"/>
                <asp:BoundField HeaderText="Valor Promedio Items x Orden" DataField="promedio" SortExpression="promedio" HeaderStyle-ForeColor="White"/>
            </Columns>
        </asp:GridView>
         <div align="right">
            <asp:Button runat="server" ID="btnGenerar" text="Descargar a Excel" OnClick="btnGenerar_Click" CssClass="estiloBotones"/>
        </div>
    </div>
</asp:Content>
