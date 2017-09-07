<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="Agregador.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Bienvenido.</h1>
    <br />
    <h2>Para iniciar, escoja una opción del menú de la izquierda.</h2>
    <br /><br />
    <h3>
        <asp:Label runat="server" ID="lblMensaje"></asp:Label>
    </h3>
    <asp:Button ID="btnSincronizarTodo" Visible="false" runat="server" Text="Sincronizar" OnClick="btnSincronizarTodo_Click" />
</asp:Content>
