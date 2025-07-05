<%@ Page Title="Autos" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Auto.aspx.cs" Inherits="ConcesionarioWEBFORM1111.Autos" MaintainScrollPositionOnPostBack="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- No hace falta linkear main.css aquí porque ya está en master page -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblUsuario" runat="server" Font-Bold="true" ForeColor="Blue" />

    <!-- Buscar -->
    <div class="panel-general">
        <h3>Buscar Autos</h3>
        <asp:TextBox ID="txtBuscar" runat="server" CssClass="input-general" placeholder="Buscar por marca, modelo, patente..." />
        <asp:Button ID="btnBuscar" runat="server" CssClass="btn-general" Text="Buscar" OnClick="btnBuscar_Click" />
        <asp:Button ID="btnVerTodos" runat="server" CssClass="btn-general" Text="Ver Todos" OnClick="btnVerTodos_Click" />
    </div>

    <!-- Formulario para agregar auto -->
    <asp:Panel ID="pnlAgregarAuto" runat="server" CssClass="panel-general">
        <h3>Agregar Auto (solo Gerente)</h3>
        <asp:TextBox ID="txtMarca" runat="server" CssClass="input-general" placeholder="Marca" />
        <asp:TextBox ID="txtModelo" runat="server" CssClass="input-general" placeholder="Modelo" />
        <asp:TextBox ID="txtColor" runat="server" CssClass="input-general" placeholder="Color" />
        <asp:TextBox ID="txtPatente" runat="server" CssClass="input-general" placeholder="Patente" />
        <asp:TextBox ID="txtAnio" runat="server" CssClass="input-general" placeholder="Año" />
        <asp:TextBox ID="txtPrecio" runat="server" CssClass="input-general" placeholder="Precio" />
        <asp:Button ID="btnAgregar" runat="server" CssClass="btn-general" Text="Agregar Auto" OnClick="btnAgregar_Click" />
        <asp:Label ID="lblAgregarResultado" runat="server" />
    </asp:Panel>

    <!-- Tabla -->
    <div class="table-responsive">
        <asp:GridView ID="gvAutos" runat="server" AutoGenerateColumns="False" DataKeyNames="ID_auto"
            OnRowEditing="gvAutos_RowEditing"
            OnRowUpdating="gvAutos_RowUpdating"
            OnRowCancelingEdit="gvAutos_RowCancelingEdit"
            OnRowDeleting="gvAutos_RowDeleting"
            OnRowCommand="gvAutos_RowCommand" CssClass="grid-general table table-bordered table-striped">
            <Columns>
                <asp:BoundField DataField="ID_auto" HeaderText="ID" ReadOnly="true" />
                <asp:BoundField DataField="Marca" HeaderText="Marca" />
                <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                <asp:BoundField DataField="Color" HeaderText="Color" />
                <asp:BoundField DataField="Patente" HeaderText="Patente" />
                <asp:BoundField DataField="Anio" HeaderText="Año" />
                <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="${0:N2}" HtmlEncode="false" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" ReadOnly="true" />
                <asp:CommandField ShowEditButton="True" />
                <asp:CommandField ShowDeleteButton="True" />
                <asp:ButtonField Text="Vender" CommandName="Vender" ButtonType="Button" />
            </Columns>
        </asp:GridView>
    </div>

    <asp:Label ID="lblGridMensaje" runat="server" ForeColor="Red" />
</asp:Content>
