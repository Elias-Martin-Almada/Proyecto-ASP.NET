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
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ImgAvatar.ImageUrl = "https://simg.nicepng.com/png/small/202-2022264_usuario-annimo-usuario-annimo-user-icon-png-transparent.png";
            if (!(Page is Login || Page is Registro || Page is Default || Page is Error)) //Exceptuo las paginas que si se pueden Ver sin Usuario.
            {             
                // Si NO tengo una session Activa redirijo:
                if (!Seguridad.sesionActiva(Session["usuario"]))
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    Usuario user = (Usuario)Session["usuario"];
                    lblUser.Text = user.Email;
                    if (!string.IsNullOrEmpty(user.ImagenPerfil))
                    {
                        ImgAvatar.ImageUrl = "~/Images/" + user.ImagenPerfil;
                    }
                }
            }
            // If para Mantener la ImgAvatar en todas las paginas.
            //if (Seguridad.sesionActiva(Session["usuario"])) 
            //{
            //    ImgAvatar.ImageUrl = "~/Images/" + ((Usuario)Session["usuario"]).ImagenPerfil;               
            //}
            //else
            //{   // ImgAvatar = url;            
            //}
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}