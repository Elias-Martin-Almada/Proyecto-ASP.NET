using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio; // Agrego negocio y dominio.
using dominio;

namespace App_Discos_Web
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Disco> ListaDisco { get; set; } // Creo una lista para usar en el front Default.
        protected void Page_Load(object sender, EventArgs e)
        {
            DiscoNegocio negocio = new DiscoNegocio();
            ListaDisco = negocio.listarConSP();

            if (!IsPostBack)
            {
                repRepetidor.DataSource = ListaDisco;
                repRepetidor.DataBind();
            }
        }

        protected void btnEjemplo_Click(object sender, EventArgs e)
        {
            string valor = ((Button)sender).CommandArgument; // Transformo el sender y capturo el Evento del Boton.
        }
    }
}