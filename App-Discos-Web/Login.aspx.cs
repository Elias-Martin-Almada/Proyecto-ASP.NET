using dominio;
using negocio;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App_Discos_Web
{
    public partial class Login : System.Web.UI.Page
    {
        // NOTAS:
        // ACCESO A LAS PANTALLAS:      CLASE "Seguridad"   
        // Home: Todos los Usuarios     Metodos:
        // Listado: Solo Admin          sessionActiva(Usuario usuario)
        // Mi Perfil: Logueado          esAdmin(Usuario usuario)
        // Favoritos: Logueado


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();
            try
            {   // Validacion: Le mando SOLO el control, que es tipo Text. 
                if (Validacion.validaTextoVacio(txtEmail) || Validacion.validaTextoVacio(txtPassword))
                {
                    Session.Add("error", "Debes completar ambos campos...");
                    Response.Redirect("Error.aspx");
                }

                // Cargo los datos para mandar por Login:
                usuario.Email = txtEmail.Text;
                usuario.Pass = txtPassword.Text;
                // Si recibe true, agrego usuario a Session:
                if (negocio.Login(usuario))
                {
                    Session.Add("usuario", usuario);
                    Response.Redirect("MiPerfil.aspx", false);
                }
                else
                {
                    Session.Add("error", "Usuario o Contraseña incorrectos");
                    Response.Redirect("Error.aspx", false);
                }
            }
            catch (System.Threading.ThreadAbortException ex) { }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        // Manejo de Error mas especifico, a nivel de Pagina:
        private void Page_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();

            Session.Add("error", exc.ToString());
            Server.Transfer("Error.aspx", true);
        }
    }
}