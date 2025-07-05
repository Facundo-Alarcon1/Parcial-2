using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ConcesionarioWEBFORM1111.Controller;
using ConcesionarioWEBFORM1111.Model;

namespace ConcesionarioWEBFORM1111
{
    public partial class Servicios : Page
    {
        private ServicioController servicioController = new ServicioController();
        private LoginController loginController = new LoginController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarEmpleados();
                CargarServicios();
                lblMensaje.Text = ""; // ocultar mensaje al cargar
            }
        }

        private void CargarEmpleados()
        {
            var empleados = loginController.ObtenerTodosLosEmpleados();
            ddlEmpleados.DataSource = empleados;
            ddlEmpleados.DataTextField = "NombreCompleto"; // ahora viene desde la propiedad
            ddlEmpleados.DataValueField = "ID_empleado";
            ddlEmpleados.DataBind();
            ddlEmpleados.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione un empleado...", ""));
        }

        private void CargarServicios()
        {
            var lista = servicioController.ObtenerTodosLosServicios(); 
            lista.Sort((a, b) => b.Fecha.CompareTo(a.Fecha));

            gvServicios.DataSource = lista;
            gvServicios.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;

            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                lblMensaje.Text = "Ingrese una descripción del servicio.";
                return;
            }

            if (string.IsNullOrEmpty(ddlEmpleados.SelectedValue))
            {
                lblMensaje.Text = "Seleccione un empleado antes de agregar.";
                return;
            }

            Servicio nuevo = new Servicio
            {
                Descripcion = txtDescripcion.Text.Trim(),
                Fecha = calFecha.SelectedDate != DateTime.MinValue ? calFecha.SelectedDate : DateTime.Today,
                Estado = "En proceso",
                ID_empleado = int.Parse(ddlEmpleados.SelectedValue)
            };

            if (servicioController.AgregarServicio(nuevo))
            {
                CargarServicios();
                txtDescripcion.Text = "";
                ddlEmpleados.SelectedIndex = 0;
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "✅ Servicio agregado correctamente.";
            }
            else
            {
                lblMensaje.Text = "❌ Error al agregar el servicio.";
            }
        }

        protected void gvServicios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int idServicio = Convert.ToInt32(gvServicios.DataKeys[index].Value);

            if (e.CommandName == "Realizado")
            {
                servicioController.MarcarComoRealizado(idServicio);
            }
            else if (e.CommandName == "Eliminar")
            {
                servicioController.EliminarServicio(idServicio);
            }

            CargarServicios();
        }
    }
}
