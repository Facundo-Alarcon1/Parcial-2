<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ConcesionarioWEBFORM1111.Login" MasterPageFile="~/Site1.Master" MaintainScrollPositionOnPostBack="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="login-container mx-auto" style="max-width:400px; width:90%;">
        <h2 class="mb-4 text-center">Iniciar Sesión</h2>
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" CssClass="d-block mb-3 text-center"></asp:Label>
        <asp:TextBox ID="txtUsuario" runat="server" placeholder="Usuario" CssClass="input-general form-control mb-3"></asp:TextBox>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Contraseña" CssClass="input-general form-control mb-3"></asp:TextBox>
        <asp:Button ID="btnLogin" runat="server" Text="Ingresar" OnClick="btnLogin_Click" CssClass="btn btn-general w-100" />
    </div>
</asp:Content>
