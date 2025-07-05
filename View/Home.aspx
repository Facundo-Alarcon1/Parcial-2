<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ConcesionarioWEBFORM1111.Home" MaintainScrollPositionOnPostBack="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Bootstrap y main.css ya están incluidos en master page -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="home-welcome">
        <h2 class="mb-3">Bienvenido al Sistema del Concesionario</h2>
        <p>Este sistema te permite gestionar vehículos, empleados, servicios y comprobantes de venta.</p>
        <ul class="ps-3">
            <li>📦 Ver y administrar el <strong>Inventario de Autos</strong>.</li>
            <li>👨‍🔧 Cargar y actualizar <strong>Servicios realizados</strong>.</li>
            <li>🧾 Visualizar y emitir <strong>Comprobantes</strong>.</li>
            <li>👥 Control de acceso por <strong>Roles de Empleado</strong>.</li>
        </ul>
        <p class="mt-3">Usá el menú superior para navegar por las secciones del sistema.</p>
    </div>
</asp:Content>
