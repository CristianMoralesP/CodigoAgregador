<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="ProductosPublicados.aspx.cs" Inherits="Agregador.ProductosPublicados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">
                Administración de publicación y productos <i class="fa fa-chevron-circle-right" aria-hidden="true"></i> Productos publicados
            </h1>
            <div class="navegacion">
                <asp:TextBox ID="txtBuscar" runat="server" class="buscador" placeholder="Buscar por cualquier término "></asp:TextBox>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="estiloBotones" OnClick="btnBuscar_Click" />
                <div class="regresar"><a href="adminProductos.aspx"><i class="fa fa-chevron-circle-left" aria-hidden="true"></i></a><a href="adminProductos.aspx">Atrás</a></div>
            </div>
        </div>
    </div>  
    <div class="row">
        <asp:GridView ID="grProductos" runat="server" DataKeyNames="id" OnSelectedIndexChanged="grProductos_SelectedIndexChanged" AutoGenerateColumns="false" GridLines="None"  AllowPaging="true" PageSize="50" OnPageIndexChanging="grProductos_PageIndexChanging" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"   AllowSorting="True" OnSorting="grProductos_Sorting">
            <Columns>
                <asp:BoundField HeaderText="Nombre del Producto" DataField="name" SortExpression="name" HeaderStyle-ForeColor="White"/>
                <asp:BoundField HeaderText="Nombre de la Tienda" DataField="sitename" SortExpression="nombreCuentaCam" HeaderStyle-ForeColor="White"/>
                 <asp:TemplateField HeaderText="Valor del Catálogo">
                    <ItemTemplate>
                        <asp:label runat="server" Text='<%#Eval("listPriceConv")!=null?string.Format("{0:C}", double.Parse(Eval("listPriceConv").ToString())):""%>'></asp:label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Valor del Producto">
                    <ItemTemplate>
                        <asp:label runat="server" Text='<%#Eval("priceConv")!=null?string.Format("{0:C}", double.Parse(Eval("priceConv").ToString())):""%>'></asp:label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Fecha de Publicación" DataField="updated"/>
                <asp:ButtonField CommandName="Select" Text="Ver Producto" />
             </Columns>
        </asp:GridView>
           <div align="right">
                <asp:Button runat="server" ID="btnGenerar" text="Descargar a Excel" OnClick="btnGenerar_Click" CssClass="estiloBotones"/>
            </div>
    </div>
</asp:Content>
