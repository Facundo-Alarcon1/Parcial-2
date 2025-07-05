using System;
using System.Web.UI.WebControls;
using ConcesionarioWEBFORM1111.Controller;
using ConcesionarioWEBFORM1111.Model;

namespace ConcesionarioWEBFORM1111
{
    public partial class Autos : System.Web.UI.Page
    {
        private readonly AutoController autoController = new AutoController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                VerificarPermisos();
                CargarAutos();
            }

            Empleados empleado = (Empleados)Session["usuario"];
            lblUsuario.Text = $"Bienvenido, {empleado.Nombre} ({empleado.Puesto})";
        }

        private void VerificarPermisos()
        {
            Empleados empleado = (Empleados)Session["usuario"];

            // Ocultar funcionalidades según rol
            if (empleado.Puesto != "Gerente")
            {
                pnlAgregarAuto.Visible = false;
                gvAutos.Columns[8].Visible = false; // Editar
                gvAutos.Columns[9].Visible = false; // Eliminar
               
            }
        }

        private void CargarAutos()
        {
            gvAutos.DataSource = autoController.BuscarAutosPorEstado("Disponible");
            gvAutos.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Empleados empleado = (Empleados)Session["usuario"];

            try
            {
                Auto nuevo = new Auto
                {
                    Marca = txtMarca.Text,
                    Modelo = txtModelo.Text,
                    Color = txtColor.Text,
                    Patente = txtPatente.Text,
                    Anio = int.Parse(txtAnio.Text),
                    Precio = decimal.Parse(txtPrecio.Text),
                    ID_empleado = empleado.ID_empleado
                };

                bool agregado = autoController.AgregarAuto(nuevo, empleado);
                lblAgregarResultado.Text = agregado ? "Auto agregado." : "Error al agregar auto.";
                CargarAutos();
            }
            catch
            {
                lblAgregarResultado.Text = "Error: datos inválidos.";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscar.Text.Trim();
            gvAutos.DataSource = autoController.BuscarAuto(criterio);
            gvAutos.DataBind();
        }

        protected void btnVerTodos_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            CargarAutos();
        }

        protected void gvAutos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvAutos.EditIndex = e.NewEditIndex;
            CargarAutos();
        }

        protected void gvAutos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAutos.EditIndex = -1;
            CargarAutos();
        }

        protected void gvAutos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Empleados empleado = (Empleados)Session["usuario"];
            if (empleado.Puesto != "Gerente")
            {
                lblGridMensaje.Text = "No tiene permisos para modificar autos.";
                return;
            }

            int id = Convert.ToInt32(gvAutos.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvAutos.Rows[e.RowIndex];

            string marca = ((TextBox)row.Cells[1].Controls[0]).Text;
            string modelo = ((TextBox)row.Cells[2].Controls[0]).Text;
            string color = ((TextBox)row.Cells[3].Controls[0]).Text;
            string patente = ((TextBox)row.Cells[4].Controls[0]).Text;
            int anio = int.Parse(((TextBox)row.Cells[5].Controls[0]).Text);
            decimal precio = decimal.Parse(((TextBox)row.Cells[6].Controls[0]).Text);

            autoController.ActualizarMarcaAuto(id, marca);
            autoController.ActualizarModeloAuto(id, modelo);
            autoController.ActualizarColorAuto(id, color);
            autoController.ActualizarPatenteAuto(id, patente);
            autoController.ActualizarAnioAuto(id, anio);
            autoController.ActualizarPrecioAuto(id, precio);

            gvAutos.EditIndex = -1;
            CargarAutos();
        }

        protected void gvAutos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Empleados empleado = (Empleados)Session["usuario"];
            if (empleado.Puesto != "Gerente")
            {
                lblGridMensaje.Text = "No tiene permisos para eliminar autos.";
                return;
            }

            int id = Convert.ToInt32(gvAutos.DataKeys[e.RowIndex].Value);
            bool eliminado = autoController.EliminarAuto(id);

            lblGridMensaje.Text = eliminado ? "Auto eliminado." : "No se pudo eliminar.";
            CargarAutos();
        }

        protected void gvAutos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Vender")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int idAuto = Convert.ToInt32(gvAutos.DataKeys[index].Value);

                Empleados empleado = (Empleados)Session["usuario"];
                if (empleado == null)
                {
                    lblGridMensaje.Text = "Debe iniciar sesión.";
                    return;
                }

                bool vendido = autoController.VenderAuto(idAuto, empleado.ID_empleado, "Venta desde web");
                lblGridMensaje.Text = vendido ? "Auto vendido con comprobante." : "Error al vender.";
                CargarAutos();
            }
        }
    }
}
