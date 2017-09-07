<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="detalleProductoNuevo.aspx.cs" Inherits="Agregador.detalleProductoNuevo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="regresar"><a href="adminProductos.aspx"><i class="fa fa-chevron-circle-left" aria-hidden="true"></i></a><a href="adminProductos.aspx">Atrás</a></div>
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
                <h2 class="titulo-campos">Categoría(s) Tienda</h2>
                <asp:Label ID="lblCategorias" runat="server" class="campo-no-editable"></asp:Label>
                 <h2 class="titulo-campos">Categoría(s) MarketPlace</h2>
                <asp:Label ID="lblCategoriasMP" runat="server" class="campo-no-editable"></asp:Label>
                <asp:Label ID="lblIdsCategoriasMP" runat="server" class="campo-no-editable"></asp:Label>
                <br />
                <asp:TextBox runat="server" ID="txtBuscarCategorias"></asp:TextBox>
                <asp:Button runat="server" ID="btnBuscarCategorias" Text="Buscar Categorias" CssClass="estiloBotones" OnClick="btnBuscarCategorias_Click"/>
                <asp:Panel runat="server" ID="pnlCategorias" Visible="false">
                    <asp:GridView ID="grCategoriasMP" runat="server" AutoGenerateColumns="False" DataKeyNames="idCam" PageSize="8" OnPageIndexChanging="grCategoriasMP_PageIndexChanging"  GridLines="None"  AllowPaging="True" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnSelectedIndexChanged="grCategoriasMP_SelectedIndexChanged">
                        <AlternatingRowStyle CssClass="alt" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar"/>
                            <asp:BoundField DataField="idCam" HeaderText="ID" />
                            <asp:BoundField DataField="name" HeaderText="Categoría" />
                        </Columns>
                        <PagerStyle CssClass="pgr" />
                    </asp:GridView>

                    <!--Grilla subcategorias-->
                    <asp:GridView ID="grSubCategorias" runat="server" AutoGenerateColumns="False" DataKeyNames="id" PageSize="8" GridLines="None"  AllowPaging="True" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                        <AlternatingRowStyle CssClass="alt" />
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="ID" />
                            <asp:BoundField DataField="name" HeaderText="SubCategoría" />
                            <asp:TemplateField HeaderText="Seleccionar">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkSubCategoria"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="pgr" />
                    </asp:GridView>
                    <div style="width:100%" align="right">
                        <asp:Button runat="server" ID="bntAsociarCategorias" text="Agregar" CssClass="estiloBotones" OnClick="bntAsociarCategorias_Click"/>
                        <asp:Button runat="server" ID="btnCancelar" text="Cancelar" CssClass="estiloBotones" OnClick="btnCancelar_Click"/>
                    </div>
                </asp:Panel>
                <h2 class="titulo-campos">Nombre de la tienda</h2>
                <asp:Label ID="lblTienda" runat="server" Text="" class="campo-no-editable"></asp:Label>
                <h2 class="titulo-campos">Precio - Precio Catálogo - Precio Final</h2>
                <asp:Label ID="lblPrecio" runat="server" Text="" class="campo-no-editable"></asp:Label>
                &nbsp;-
                <asp:Label ID="lblPrecioCatalogo" runat="server" Text="" class="campo-no-editable"></asp:Label>
                &nbsp;-
                <asp:Label ID="lblPrecioFinal" runat="server" Text="" class="campo-no-editable"></asp:Label>
                &nbsp;<h2 class="titulo-campos">Fecha de publicación</h2>
                <asp:Label ID="lblFecha" runat="server" Text="" class="campo-no-editable"></asp:Label>
                &nbsp;<h2 class="titulo-campos">Descuento</h2>
                <asp:Label ID="lblDescuento" runat="server" Text="" class="campo-no-editable"></asp:Label>
                <br />
                <asp:CheckBox runat="server" ID="chkPrdDisponible" Enabled="false" Text="Producto Disponible"/>
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
            <asp:LinkButton runat="server" ID="lbtnDescartar" class="btn btn-lg btn-success btn-block editar-productos" OnClick="lbtnDescartar_Click"><i class="fa fa-trash" aria-hidden="true"></i> Descartar</asp:LinkButton>
        </div>
        <!--<div class="col-lg-2">
            <asp:LinkButton runat="server" ID="btnPreliminar" class="btn btn-lg btn-success btn-block editar-productos" OnClick="btnPreliminar_Click"><i class="fa fa-pencil" aria-hidden="true"></i> Preliminar</asp:LinkButton>
        </div>-->
        <div class="col-lg-2">
            <asp:LinkButton runat="server" ID="btnPublicar" class="btn btn-lg btn-success btn-block editar-productos" OnClick="btnPublicar_Click"><i class="fa fa-check-circle" aria-hidden="true"></i> Publicar</asp:LinkButton>
        </div>
    </div>
</asp:Content>
