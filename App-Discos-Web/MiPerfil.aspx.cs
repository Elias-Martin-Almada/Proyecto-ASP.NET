using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace App_Discos_Web
{
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Seguridad.sesionActiva(Session["usuario"]))
                    {
                        Usuario user = (Usuario)Session["usuario"];
                        txtEmail.Text = user.Email;
                        txtEmail.ReadOnly = true;
                        txtNombre.Text = user.Nombre;
                        txtApellido.Text = user.Apellido;
                        txtFechaNacimiento.Text = user.FechaNacimiento.ToString("yyyy-MM-dd"); // Formato que espera el txtFecha.
                        if (!string.IsNullOrEmpty(user.ImagenPerfil))
                            imgNuevoPerfil.ImageUrl = "~/Images/" + user.ImagenPerfil;
                    }
                }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {   // Si no es valida la pantalla, no Guardo todo de nuevo.
                Page.Validate();
                if (!Page.IsValid)
                {
                    return;
                }

                // Si la página es válida, agregar clases is-valid a los campos.
                txtNombre.CssClass = "form-control is-valid";
                txtApellido.CssClass = "form-control is-valid";

                // Escribir Imagen:
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario user = (Usuario)Session["usuario"];      // Obtengo el "user" de la Session para usar el ID.
                // Escribir Img si se cargó algo:
                if (txtImagen.PostedFile.FileName != "")
                {
                    string ruta = Server.MapPath("./Images/");                         // Ruta fija donde voy a trabajar, /Perfil.jpg
                    txtImagen.PostedFile.SaveAs(ruta + "perfil-" + user.Id + ".jpg");  // PostedFile Me obtiene los datos del archivo, Save lo guarda  en el Servidor.
                    user.ImagenPerfil = "perfil-" + user.Id + ".jpg";                  // Guardo la foto al Perfil.
                }
                                                                                   
                user.Nombre = txtNombre.Text;
                user.Apellido = txtApellido.Text;
                user.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);

                // Guardo datos perfil:
                negocio.actualizar(user);

                // Leer Imagen:
                Image img = (Image)Master.FindControl("imgAvatar");    // Con FindControl busco el control que esta en la Master.
                img.ImageUrl = "~/Images/" + user.ImagenPerfil;        // Cargo Imagen al control asp:Image del Avatar.
            }                                                          //+ "?v=" + DateTime.Now.Ticks.ToString(); <-- Consultar: Actualizar Img.
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }
    }
}