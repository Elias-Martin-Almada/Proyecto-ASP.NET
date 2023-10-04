using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dominio
{
    public class Estilo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public override string ToString() // Sobreescribo el metodo ToString.
        {
            return Descripcion;           // Para que me retorne el Genero del Disco.
        }

    }
}
