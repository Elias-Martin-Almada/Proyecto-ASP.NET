using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;

namespace App_Discos_Web
{
    public partial class FormularioDisco : System.Web.UI.Page
    {
        public bool ConfirmaEliminacion { get; set; } // Esto es para el If del front, Checked.
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            ConfirmaEliminacion = false;
            try
            {   // Configuración inicial de la pantalla.
                if (!IsPostBack)
                {   // Cargo los Desplegables:
                    EstiloNegocio negocio = new EstiloNegocio();
                    List<Estilo> lista = negocio.listar();

                    ddlGenero.DataSource = lista;
                    ddlGenero.DataValueField = "Id";
                    ddlGenero.DataTextField = "Descripcion";
                    ddlGenero.DataBind();

                    TiposEdicionNegocio negocioEdicion = new TiposEdicionNegocio();
                    List<TiposEdicion> listaEdicion = negocioEdicion.listar();

                    ddlEdicion.DataSource = listaEdicion;
                    ddlEdicion.DataValueField = "Id";
                    ddlEdicion.DataTextField = "Descripcion";
                    ddlEdicion.DataBind();
                }

                // Configuracion si estamos modificando.
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";

                if (id != "" && !IsPostBack) // Atencion!!! El PostBack me pisa con datos viejos al hacer click en Aceptar.
                {   // Opcion 1):
                    //List<Disco> lista = negocio.listar(id);
                    //Disco seleccionado = lista[0];
                    // Opcion 2):
                    DiscoNegocio negocio = new DiscoNegocio();
                    Disco seleccionado = (negocio.listar(id))[0]; // En esta lista tengo solo un Disco.

                    // Guardo Disco seleccionado en Session: por ej para usar en el Inactivar.
                    Session.Add("discoSeleccionado", seleccionado);

                    // Precargar todos los campos:
                    txtId.Text = id;
                    txtTitulo.Text = seleccionado.Titulo;
                    txtFecha.Text = seleccionado.FechaLanzamiento.ToString("yyyy-MM-dd");
                    txtCantidadCanciones.Text = seleccionado.CantidadCanciones.ToString();
                    txtUrlImagen.Text = seleccionado.UrlImagen;
                    // Precargo desplegables:
                    ddlGenero.SelectedValue = seleccionado.Genero.Id.ToString();
                    ddlEdicion.SelectedValue = seleccionado.Edicion.Id.ToString();
                    txtUrlImagen_TextChanged(sender, e); // Fuerzo el evento de la imagen.

                    //configurar Acciones:
                    if (!seleccionado.Activo)
                    {
                        btnInactivar.Text = "Reactivar"; 
                    }

                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");

            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                // Genero un Disco nuevo para cargarlo con los controles.
                Disco nuevo = new Disco();
                DiscoNegocio negocio = new DiscoNegocio();

                nuevo.Titulo = txtTitulo.Text;
                nuevo.FechaLanzamiento = DateTime.Parse(txtFecha.Text);
                nuevo.CantidadCanciones = int.Parse(txtCantidadCanciones.Text);
                nuevo.UrlImagen = txtUrlImagen.Text;

                nuevo.Genero = new Estilo();
                nuevo.Genero.Id = int.Parse(ddlGenero.SelectedValue);
                nuevo.Edicion = new TiposEdicion();
                nuevo.Edicion.Id = int.Parse(ddlEdicion.SelectedValue);

                if (Request.QueryString["id"] != null) // Pregunto si Modifica o Agrega.
                {
                    nuevo.Id = int.Parse(txtId.Text);  // Atencion!!! Tengo que mandar el ID para modificar.
                    negocio.modificarConSP(nuevo);
                }
                else
                {
                    negocio.agregarConSP(nuevo); // Agrego con Procedimiento almacenado.                  
                }
                Response.Redirect("DiscosLista.aspx", false);

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void txtUrlImagen_TextChanged(object sender, EventArgs e)
        {
            imgDisco.ImageUrl = txtUrlImagen.Text;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ConfirmaEliminacion = true;
        }

        protected void btnConfirmaEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmaEliminacion.Checked) // Pregunto si esta chequeado. 
                {
                    DiscoNegocio negocio = new DiscoNegocio();
                    negocio.eliminar(int.Parse(txtId.Text));
                    Response.Redirect("DiscosLista.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                DiscoNegocio negocio = new DiscoNegocio();
                Disco seleccionado = (Disco)Session["discoSeleccionado"]; // Recupero el Disco de Session.

                negocio.eliminarLogico(seleccionado.Id, ! seleccionado.Activo); // Mando los parametros, y cambio el estado de Activo. 
                Response.Redirect("DiscosLista.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}