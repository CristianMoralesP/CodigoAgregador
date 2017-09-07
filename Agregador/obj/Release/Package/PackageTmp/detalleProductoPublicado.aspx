<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="detalleProductoPublicado.aspx.cs" Inherits="Agregador.detalleProductoPublicado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-4">
            <div class="imagen-producto">
                <asp:Image ID="img" runat="server" style="width:100%"/>
                <asp:LinkButton ID="lbtnEditar" runat="server" class="btn btn-lg btn-success btn-block editar-productos" OnClick="lbtnEditar_Click"><i class="fa fa-pencil" aria-hidden="true"></i> Editar producto</asp:LinkButton>
            </div>
        </div>
        <div class="col-lg-8">
            <div class="datos-responsive">
                <h2 class="titulo-campos">Nombre del producto</h2>
                <asp:TextBox ID="txtNombreProducto" runat="server" class="titulo-producto"></asp:TextBox>
                <asp:Label ID="lblNombreProducto" runat="server" class="campo-no-editable"></asp:Label>
                <h2 class="titulo-campos">Categoría</h2>
                <asp:Label ID="lblCategorias" runat="server" class="campo-no-editable"></asp:Label>
                <asp:Label ID="lblIdsCategorias" runat="server" class="campo-no-editable"></asp:Label>
                <br />
                <asp:TextBox runat="server" ID="txtBuscarCategorias"></asp:TextBox>
                <asp:Button runat="server" ID="btnBuscarCategorias" Text="Buscar Categorias" CssClass="estiloBotones" OnClick="btnBuscarCategorias_Click"/>
                <asp:Panel runat="server" ID="pnlCategorias" Visible="false">
                    <asp:GridView ID="grCategoriasMP" runat="server" AutoGenerateColumns="false" DataKeyNames="idCam" PageSize="8" OnPageIndexChanging="grCategoriasMP_PageIndexChanging"  GridLines="None"  AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                        <Columns>
                            <asp:BoundField DataField="idCam" HeaderText="ID" />
                            <asp:BoundField DataField="name" HeaderText="Categoría" />
                            <asp:TemplateField HeaderText="Seleccionar">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkSeleccionar"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div style="width:100%" align="right">
                        <asp:Button runat="server" ID="bntAsociarCategorias" text="Agregar" CssClass="estiloBotones" OnClick="bntAsociarCategorias_Click"/>
                        <asp:Button runat="server" ID="btnCancelar" text="Cancelar" CssClass="estiloBotones" OnClick="btnCancelar_Click"/>
                    </div>
                </asp:Panel>
                <h2 class="titulo-campos">Nombre de la tienda</h2>
                <asp:Label ID="lblTienda" runat="server" Text="" class="campo-no-editable"></asp:Label>
                <h2 class="titulo-campos">Precio</h2>
                <asp:Label ID="lblPrecio" runat="server" Text="" class="campo-no-editable"></asp:Label>
                <h2 class="titulo-campos">Fecha de publicación</h2>
                <asp:Label ID="lblFecha" runat="server" Text="" class="campo-no-editable"></asp:Label>
                <h2 class="titulo-campos">Descripción del producto</h2>
                <asp:Label ID="lblDescripcion" runat="server" Text="" class="campo-no-editable"></asp:Label>
                <asp:TextBox ID="txtDescripcion" runat="server" class="titulo-producto" TextMode="MultiLine" Height="200px" ></asp:TextBox>
            </div>
        </div>
    </div>
    <!-- /.row -->
    <div class="row">
        <div class="col-lg-8"></div>
        <div class="col-lg-2">
            <asp:LinkButton runat="server" ID="lbtnEliminar" class="btn btn-lg btn-success btn-block editar-productos" OnClick="lbtnEliminar_Click"><i class="fa fa-trash" aria-hidden="true"></i> Eliminar</asp:LinkButton>
        </div>
        <!--<div class="col-lg-2">
            <asp:LinkButton runat="server" ID="btnPreliminar" class="btn btn-lg btn-success btn-block editar-productos" OnClick="btnPreliminar_Click"><i class="fa fa-pencil" aria-hidden="true"></i> Preliminar</asp:LinkButton>
        </div>-->
        <div class="col-lg-2">
            <asp:LinkButton runat="server" ID="btnPublicar" class="btn btn-lg btn-success btn-block editar-productos" OnClick="btnPublicar_Click"><i class="fa fa-check-circle" aria-hidden="true"></i> Publicar</asp:LinkButton>
        </div>
    </div>
</asp:Content>