<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="actualizarInfo.aspx.cs" Inherits="Agregador.actualizarInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">
                Administrador de usuarios <i class="fa fa-chevron-circle-right" aria-hidden="true"></i> Editar usuario
            </h1>
            <div class="navegacion">
                <div class="regresar"><i class="fa fa-chevron-circle-left" aria-hidden="true"></i> Atrás</div>
            </div>
        </div>
    </div>
    <!-- /.row -->

    <div class="row">
        <div class="col-lg-12">
            <div class="login-panel panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Actualizar Información</h3>
                </div>
                <div class="panel-body">
                    <fieldset>
                        <div class="form-group">
                            <asp:TextBox ID="txtNombres" runat="server" class="form-control" placeholder="Nombres"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtClave" runat="server" TextMode="Password" class="form-control" placeholder="Contraseña" ></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtCorreo" runat="server" type="email" class="form-control" placeholder="Usuario ó correo electrónico" autofocus></asp:TextBox>
                        </div>
                        <!-- Change this to a button or input when using this as a form -->
                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" class="btn btn-lg btn-success btn-block" OnClick="btnSalvar_Click"/>
                    </fieldset>
                </div>
             </div>
        </div>
    </div>
</asp:Content>
