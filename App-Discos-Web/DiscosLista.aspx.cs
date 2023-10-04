using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio; // Agrego el Negocio.
using dominio;


namespace App_Discos_Web
{
    public partial class DiscosLista : System.Web.UI.Page
    {
        public bool FiltroAvanzado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.esAdmin(Session["usuario"]))
            {
                Session.Add("error", "Se requiere permisos de admin para acceder a esta pantalla");
                Response.Redirect("Error.aspx");
            }

            //if (!IsPostBack)  // Este PostBack afecta el Paginado 2, si lo saco funciona.
            //{ 
                FiltroAvanzado = false;
                DiscoNegocio negocio = new DiscoNegocio();
                Session.Add("listaDiscos", negocio.listarConSP()); // Uso el Procedimiento.
                dgvDiscos.DataSource = Session["listaDiscos"];     // Guardo lista en Session para el Filtro por ej.
                dgvDiscos.DataBind();
            //} 
        }

        protected void dgvDiscos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvDiscos.PageIndex = e.NewPageIndex;         // Evento del Paginado.
            dgvDiscos.DataBind();
        }

        protected void dgvDiscos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvDiscos.SelectedDataKey.Value.ToString(); // Evento para mandar Id por URL.
            Response.Redirect("FormularioDisco.aspx?id=" + id);
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e) // Filtra por Titulo.
        {
            List<Disco> lista = (List<Disco>)Session["listaDiscos"]; // Casteo.
            List<Disco> listaFiltrada = lista.FindAll(x => x.Titulo.ToUpper().Contains(txtFiltro.Text.ToUpper()));
            dgvDiscos.DataSource = listaFiltrada;
            dgvDiscos.DataBind();
        }

        protected void chkAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            FiltroAvanzado = chkAvanzado.Checked;
            txtFiltro.Enabled = ! FiltroAvanzado; // Esto maneja si se Activa o No el FiltroAvanzado.
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();
            if (ddlCampo.SelectedItem.ToString() == "Cantidad de Canciones")
            {
                ddlCriterio.Items.Add("Igual a");
                ddlCriterio.Items.Add("Mayor a");
                ddlCriterio.Items.Add("Menor a");
            }
            else
            {
                ddlCriterio.Items.Add("Contiene");
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                DiscoNegocio negocio = new DiscoNegocio();
                dgvDiscos.DataSource = negocio.filtrar(ddlCampo.SelectedItem.ToString(),
                    ddlCriterio.SelectedItem.ToString(), 
                    txtFiltroAvanzado.Text, 
                    ddlEstado.SelectedItem.ToString());
                dgvDiscos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }
    }
}