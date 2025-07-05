using System;
using ConcesionarioWEBFORM1111.Controller;
using ConcesionarioWEBFORM1111.Model;
using System.Collections.Generic;

namespace ConcesionarioWEBFORM1111
{
    public partial class Comprobantes : System.Web.UI.Page
    {
        private readonly ComprobanteController comprobanteController = new ComprobanteController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarComprobantes();
            }
        }

        private void CargarComprobantes()
        {
            List<Comprobante> lista = comprobanteController.ObtenerTodosLosComprobantes();
            gvComprobantes.DataSource = lista;
            gvComprobantes.DataBind();
        }
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string tipoFiltro = ddlTipoFiltro.SelectedValue;
            CargarComprobantes(tipoFiltro);
        }

        private void CargarComprobantes(string tipoFiltro = "")
        {
            List<Comprobante> lista = comprobanteController.ObtenerTodosLosComprobantes();

            if (!string.IsNullOrEmpty(tipoFiltro))
            {
                lista = lista.FindAll(c => c.Tipo.Equals(tipoFiltro, StringComparison.OrdinalIgnoreCase));
            }

            gvComprobantes.DataSource = lista;
            gvComprobantes.DataBind();
        }

    }
}
