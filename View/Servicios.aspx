<%@ Page Title="Servicios" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Servicios.aspx.cs" Inherits="ConcesionarioWEBFORM1111.Servicios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Bootstrap y main.css ya están incluidos en master page -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel-general">
        <h2>Agregar nuevo servicio</h2>

        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="input-general" placeholder="Descripción" />

        <asp:Calendar ID="calFecha" runat="server" CssClass="calendario" />

        <asp:DropDownList ID="ddlEmpleados" runat="server" CssClass="input-general" AppendDataBoundItems="true">
            <asp:ListItem Text="Seleccione un empleado..." Value="" />
        </asp:DropDownList>

        <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-general" Text="Agregar Servicio" OnClick="btnAgregar_Click" />

        <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje-error" ForeColor="Red" />
    </div>

    <div class="panel-general">
        <h2>Listado de servicios</h2>
        <div class="table-responsive">
            <asp:GridView ID="gvServicios" runat="server" AutoGenerateColumns="False" DataKeyNames="ID_servicio" CssClass="grid-general table table-bordered table-striped" OnRowCommand="gvServicios_RowCommand">
                <Columns>
                    <asp:BoundField DataField="ID_servicio" HeaderText="ID" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" />
                    <asp:BoundField DataField="NombreEmpleado" HeaderText="Empleado" />
                    <asp:ButtonField ButtonType="Button" CommandName="Realizado" Text="Marcar Realizado" />
                    <asp:ButtonField ButtonType="Button" CommandName="Eliminar" Text="Eliminar" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
