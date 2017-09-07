<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="adminProductos.aspx.cs" Inherits="Agregador.adminProductos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-wrapper">
        <div class="container-fluid">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">
                        Administración de publicación y productos
                    </h1>
                    <div class="navegacion">
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3">
                    <a href="ProductosNuevos.aspx" class="btn btn-lg2 btn-success btn-block"><i class="fa fa-plus-circle" aria-hidden="true"></i><br/>Productos nuevos</a>                        
                    <div class="notififacion-producto">
                        <asp:Label ID="lblNuevos" runat="server" Text="0"></asp:Label>
                    </div>
                </div>
                <div class="col-lg-3">
                    <a href="ProductosPublicados.aspx" class="btn btn-lg2 btn-success btn-block"><i class="fa fa-shopping-cart" aria-hidden="true"></i><br>Productos publicados</a>
                     <div class="notififacion-producto">
                        <asp:Label ID="lblPublicados" runat="server" Text="0"></asp:Label>
                    </div>
                </div>
                <div class="col-lg-3">
                    <a href="ProductosAnteriores.aspx" class="btn btn-lg2 btn-success btn-block"><i class="fa fa-minus-circle" aria-hidden="true"></i><br>Productos anteriores</a>
                     <div class="notififacion-producto">
                        <asp:Label ID="lblAnteriores" runat="server" Text="0"></asp:Label>
                    </div>
                </div>
                <div class="col-lg-3">
                    <a href="ProductosDescartados.aspx" class="btn btn-lg2 btn-success btn-block"><i class="fa fa-trash" aria-hidden="true"></i><br>Productos descartados</a>
                     <div class="notififacion-producto">
                        <asp:Label ID="lblDescartados" runat="server" Text="0"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
