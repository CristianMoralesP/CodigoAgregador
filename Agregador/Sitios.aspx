<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="Sitios.aspx.cs" Inherits="Agregador.Sitios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="tiendas.aspx">Volver</a>
    <asp:GridView ID="grSitios" runat="server" AutoGenerateColumns="false" DataKeyNames="Id">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="id"/>
            <asp:BoundField HeaderText="ID Camilyo" DataField="idCam"/>
            <asp:BoundField HeaderText="Usuario" DataField="usuario"/>
            <asp:BoundField HeaderText="Nombre Sitio" DataField="sitename"/>
            <asp:BoundField HeaderText="URI" DataField="displaysiteurl"/>
            <asp:BoundField HeaderText="Cuenta" DataField="account_name"/>
            <asp:BoundField HeaderText="Actualizado" DataField="up_to_date"/>
            <asp:TemplateField HeaderText="Activo">
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="chkAct" checked='<%# bool.Parse(Eval("is_active").ToString()) %>'/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
