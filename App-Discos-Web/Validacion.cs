using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;                // Atencion!! Agregar las referencias para TextBox.
using System.Web.UI;
using System.Web.UI.WebControls;

namespace negocio
{
    public static class Validacion
    {
        public static bool validaTextoVacio(object control)
        {
            if(control is TextBox texto)  // Si Email es de tipo Text entra:
            {
                if (string.IsNullOrEmpty(texto.Text))  // Pregunto si esta Null o Vacio:
                {
                    return true;                       // Si es true entra en el If Validacion de btnLogin_Click.
                }
                else                                   
                {
                    return false;                      // Sino carga sigue.
                }
            }

            return false;                               // Si el control no es TextBox NO entra en el If Validacion de btnLogin_Click.
        }
    }
}
