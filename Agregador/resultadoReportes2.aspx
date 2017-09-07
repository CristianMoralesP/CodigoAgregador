<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="resultadoReportes2.aspx.cs" Inherits="Agregador.resultadoReportes2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">
                Reportes <i class="fa fa-chevron-circle-right" aria-hidden="true"></i> Productos Registrados
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
        <asp:GridView ID="grProductosRegistrados" runat="server" DataKeyNames="id" AutoGenerateColumns="false" GridLines="None"  AllowPaging="true" PageSize="50" OnPageIndexChanging="grProductosRegistrados_PageIndexChanging" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnSorting="grProductosRegistrados_Sorting">
            <Columns>
                <asp:BoundField HeaderText="Nombre Cuenta" DataField="nombreCuentaCam" SortExpression="nombreCuentaCam" HeaderStyle-ForeColor="White"/>
                <asp:BoundField HeaderText="Empresa" DataField="companyname" SortExpression="companyname" HeaderStyle-ForeColor="White"/>
                <asp:BoundField HeaderText="Teléfono" DataField="phone" SortExpression="phone" HeaderStyle-ForeColor="White"/>
                <asp:BoundField HeaderText="Correo" DataField="email" SortExpression="email" HeaderStyle-ForeColor="White"/>
                <asp:BoundField HeaderText="Producto" DataField="name" SortExpression="name" HeaderStyle-ForeColor="White"/>
                <asp:TemplateField HeaderText="Precio">
                    <ItemTemplate>
                        <asp:label runat="server" Text='<%#Eval("price")!=null?string.Format("{0:C}", double.Parse(Eval("price").ToString())):""%>'></asp:label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
         <div align="right">
            <asp:Button runat="server" ID="btnGenerar" text="Descargar a Excel" OnClick="btnGenerar_Click" CssClass="estiloBotones"/>
        </div>
    </div>
</asp:Content>
