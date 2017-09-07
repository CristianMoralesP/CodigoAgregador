<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="ofertas.aspx.cs" Inherits="Agregador.ofertas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">
                Ofertas
            </h1>
            <div class="navegacion">
            </div>
        </div>
    </div>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <div class="row">
        <%--<asp:GridView ID="grCategorias" runat="server" DataKeyNames="id" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" CssClass="mGrid" GridLines="None">
            <Columns>
                <asp:TemplateField HeaderText="Asignar">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkCuentaAct" runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="id" HeaderStyle-ForeColor="White" HeaderText="ID"/>
                <asp:BoundField DataField="name" HeaderStyle-ForeColor="White" HeaderText="Subcategoría" />
                <asp:BoundField DataField="categoryname" HeaderStyle-ForeColor="White" HeaderText="Categoría" />
            </Columns>
        </asp:GridView>--%>
        <br />
        <asp:Label runat="server" ID="lblMsj"></asp:Label>
        <br />
        <asp:Panel runat="server" ID="pnlCategorias">
            <asp:CheckBox ID="chkTodas" runat="server" OnCheckedChanged="chkTodas_CheckedChanged" AutoPostBack="true" Text="Escoger todas las categorías"/>
            <asp:GridView ID="grCategoriasMP" runat="server" AutoGenerateColumns="False" DataKeyNames="idCam" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnSelectedIndexChanged="grCategoriasMP_SelectedIndexChanged">
                <AlternatingRowStyle CssClass="alt" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" SelectText="Ver subcategorías"/>
                    <asp:BoundField DataField="idCam" HeaderText="ID" />
                    <asp:BoundField DataField="name" HeaderText="Categoría" />
                     <asp:TemplateField HeaderText="Escoger">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkCategoria" runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pgr" />
            </asp:GridView>
        </asp:Panel>
        <br />
        <asp:CheckBox ID="chkTodasSC" runat="server" OnCheckedChanged="chkTodasSC_CheckedChanged" AutoPostBack="true" Text="Escoger todas las subcategorías"/>
        <asp:GridView ID="grSubCategorias" runat="server" AutoGenerateColumns="False" DataKeyNames="id" PageSize="50" GridLines="None"  AllowPaging="True" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="grSubCategoriasMP_PageIndexChanging">
            <AlternatingRowStyle CssClass="alt" />
            <Columns>
                <asp:BoundField DataField="categoryname" HeaderText="Categoría" />
                <asp:BoundField DataField="id" HeaderText="ID Subcategoria" />
                <asp:BoundField DataField="name" HeaderText="SubCategoría" />
                <asp:TemplateField HeaderText="Seleccionar">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="chkSubCategoria"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pgr" />
        </asp:GridView>
        <br />
        <table>
            <tr align="left">
                <td><b> Mostrar desde </b></td>
                <td><asp:TextBox runat="server" TextMode="Date" ID="txtFechaInicial"></asp:TextBox></td>
            </tr>
            <tr align="right">
                <td><b> Mostrar hasta </b></td>
                <td><asp:TextBox runat="server" TextMode="Date" ID="txtFechaFinal"></asp:TextBox></td>
            </tr>
        </table>
        </div>
        <br />
        <div class="row">
            <div class="col-lg-8"></div>
            <div class="col-lg-2">
                <asp:LinkButton runat="server" ID="LinkButton1" class="btn btn-lg btn-success btn-block editar-productos" OnClick="btnCrear_Click"><i class="fa fa-check-circle" aria-hidden="true"></i> Publicar</asp:LinkButton>
            </div>
            <asp:Label ID="lblRespuesta" runat="server" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>
