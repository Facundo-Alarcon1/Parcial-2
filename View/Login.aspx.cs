using System;
using ConcesionarioWEBFORM1111.Controller;
using ConcesionarioWEBFORM1111.Model;

namespace ConcesionarioWEBFORM1111
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly LoginController loginController = new LoginController();

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validar credenciales con el controlador
            var resultado = loginController.ValidarCredenciales(usuario, password);

            if (resultado.NombreUsuario != null)
            {
                // Crear objeto Empleados con los datos
                Empleados empleado = new Empleados
                {
                    ID_empleado = resultado.IdEmpleado,
                    Nombre = resultado.NombreUsuario,  // O traer el nombre real si tenés en BD
                    Apellido = "",  // Completar si tienes el apellido en BD
                    Puesto = resultado.Puesto
                };

                // Guardar el objeto completo en sesión
                Session["usuario"] = empleado;

                // Redirigir a la página principal
                Response.Redirect("Home.aspx");
            }
            else
            {
                lblMensaje.Text = "Usuario o contraseña incorrectos.";
            }
        }
    }
}
