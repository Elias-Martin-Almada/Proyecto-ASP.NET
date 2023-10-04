using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Disco 
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        [DisplayName("Fecha de Lanzamiento")]          
        public DateTime FechaLanzamiento { get; set; } 
        [DisplayName("Cantidad de Canciones")]
        public int CantidadCanciones { get; set; }
        public string UrlImagen { get; set; }
        public Estilo Genero { get; set; }
        public TiposEdicion Edicion { get; set; }
        public bool Activo { get; set; } // Agrego para ver si esta Activo o no con CheckBox.

    }
}
    
    


