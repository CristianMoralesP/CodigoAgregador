<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="adminReportes.aspx.cs" Inherits="Agregador.adminReportes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-wrapper">
        <div class="container-fluid">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">
                        Reportes
                    </h1>
                    <div class="navegacion">
                    </div>
                </div>
            </div>
            <div class="row">
                    <div class="col-lg-3">
                        <a href="resultadoReportes1.aspx" class="btn btn-lg3 btn-success btn-block"><i class="fa fa-globe" aria-hidden="true"></i><br>Cantidad de aliados:<br> Número de aliados activos<br> del programa con tienda activa.</a>
                    </div>
                    <div class="col-lg-3">
                        <a href="resultadoReportes2.aspx" class="btn btn-lg3 btn-success btn-block"><i class="fa fa-shopping-cart" aria-hidden="true"></i><br>Productos registrados:<br> Suma de productos activos<br> registrados en las Tiendas.</a>
                    </div>
                    <div class="col-lg-3">
                        <a href="resultadoReportes3.aspx" class="btn btn-lg3 btn-success btn-block"><i class="fa fa-line-chart" aria-hidden="true"></i><br>Transacciones:<br>Cantidad de transacciones<br>generadas en las Tiendas.</a>
                    </div>
                    <!--<div class="col-lg-3">
                        <a href="resultadoReportes4.aspx" class="btn btn-lg3 btn-success btn-block"><i class="fa fa-pie-chart" aria-hidden="true"></i><br>Valor Transacción:<br> Valor promedio<br> de una transacción.</a>
                    </div>-->
                <div class="col-lg-3">
                        <a href="resultadoReportes4.aspx" class="btn btn-lg3 btn-success btn-block"><i class="fa fa-archive" aria-hidden="true"></i><br>SKU´s por Venta:<br> Número promedio de ítems<br> vendidos en cada orden
</a>
                    </div>
                </div>
                <div class="row">
                    
                    <div class="col-lg-3">
                        <a href="resultadoReportes6.aspx" class="btn btn-lg3 btn-success btn-block"><i class="fa fa-usd" aria-hidden="true"></i><br>Ingresos totales: <br> Ingresos totales generados<br> a partir de la plataforma
</a>
                    </div>
                    <div class="col-lg-3">
                        <a href="resultadoReportes7.aspx" class="btn btn-lg3 btn-success btn-block"><i class="fa fa-user" aria-hidden="true"></i><br>Clientes:<br> Cantidad de clientes registrados<br> en las Tiendas Aliado
</a>
                    </div>
                    <div class="col-lg-3">
                        <a href="https://www.google.com/intl/es/analytics/" target="_blank" class="btn btn-lg3 btn-success btn-block"><i class="fa fa-bar-chart" aria-hidden="true"></i><br>Ir a google Analytics</a>
                    </div>
                </div>
        </div>
    </div>
</asp:Content>