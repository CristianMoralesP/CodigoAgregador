<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="detalleProductoDescartado.aspx.cs" Inherits="Agregador.detalleProductoDescartado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-4">
            <div class="imagen-producto">
                <asp:Image ID="img" runat="server" style="width:100%"/>
                <asp:LinkButton ID="lbtnMarcarNuevo" runat="server" class="btn btn-lg btn-success btn-block editar-productos" OnClick="lbtnMarcarNuevo_Click"><i class="fa fa-pencil" aria-hidden="true"></i> Marcar como nuevo</asp:LinkButton>
            </div>
        </div>
        <div class="col-lg-8">
            <div class="datos-responsive">
                <h2 class="titulo-campos">Nombre del producto</h2>
                <asp:Label ID="lblNombreProducto" runat="server" class="campo-no-editable"></asp:Label>
                <h2 class="titulo-campos">Categoría</h2>
                <asp:Label ID="lblCategorias" runat="server" class="campo-no-editable"></asp:Label>
                <br />
                <h2 class="titulo-campos">Nombre de la tienda</h2>
                <asp:Label ID="lblTienda" runat="server" Text="" class="campo-no-editable"></asp:Label>
                <h2 class="titulo-campos">Precio</h2>
                <asp:Label ID="lblPrecio" runat="server" Text="" class="campo-no-editable"></asp:Label>
                <h2 class="titulo-campos">Fecha de publicación</h2>
                <asp:Label ID="lblFecha" runat="server" Text="" class="campo-no-editable"></asp:Label>
                <h2 class="titulo-campos">Descripción del producto</h2>
                <asp:Label ID="lblDescripcion" runat="server" Text="" class="campo-no-editable"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
