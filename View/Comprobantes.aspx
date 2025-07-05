<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Comprobantes.aspx.cs" Inherits="ConcesionarioWEBFORM1111.Comprobantes" MasterPageFile="~/Site1.Master" MaintainScrollPositionOnPostBack="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Bootstrap y main.css ya están incluidos en master page -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel-general">
        <h2>Lista de Comprobantes</h2>

        <asp:DropDownList ID="ddlTipoFiltro" runat="server" CssClass="input-general">
            <asp:ListItem Text="Todos" Value=""></asp:ListItem>
            <asp:ListItem Text="Compra" Value="compra"></asp:ListItem>
            <asp:ListItem Text="Venta" Value="venta"></asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnFiltrar" runat="server" CssClass="btn btn-general" Text="Filtrar" OnClick="btnFiltrar_Click" />
    </div>

    <div class="panel-general">
        <div class="table-responsive">
            <asp:GridView ID="gvComprobantes" runat="server" AutoGenerateColumns="false" CssClass="grid-general table table-bordered table-striped">
                <Columns>
                    <asp:BoundField DataField="ID_comprobante" HeaderText="ID" />
                    <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                    <asp:BoundField DataField="FechaHora" HeaderText="Fecha y Hora" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                    <asp:BoundField DataField="ID_auto" HeaderText="ID Auto" />
                    <asp:BoundField DataField="ID_empleado" HeaderText="ID Empleado" />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" />
                    <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" />
                    <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="${0:N2}" HtmlEncode="false" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
