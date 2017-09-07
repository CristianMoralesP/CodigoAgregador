<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="agregarCuenta.aspx.cs" Inherits="Agregador.agregarCuenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">
                Administrador de usuarios <i class="fa fa-chevron-circle-right" aria-hidden="true"></i> Crear usuario
            </h1>
            <div class="navegacion">
                <div class="regresar"><a href="cuentas.aspx"><i class="fa fa-chevron-circle-left" aria-hidden="true"></i></a><a href="cuentas.aspx">Atrás</a></div>
            </div>
        </div>
    </div>
    <!-- /.row -->

    <div class="row">
        <div class="col-lg-12">
            <div class="login-panel panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Crear nueva cuenta</h3>
                </div>
                <div class="panel-body">
                    <fieldset>
                        <div class="form-group">
                            <asp:TextBox ID="txtCuenta" runat="server" class="form-control" placeholder="Nombre"></asp:TextBox>
                            <asp:CheckBox ID="chkMP" runat="server" Text="Market Place"/>
                        </div>
                        <asp:Button ID="btnCrear" runat="server" Text="Crear" class="btn btn-lg btn-success btn-block" OnClick="btnCrear_Click"/>
                        <asp:Label ID="lblRespuesta" runat="server" Text=""></asp:Label>
                    </fieldset>
                </div>
             </div>
        </div>
    </div>
</asp:Content>
