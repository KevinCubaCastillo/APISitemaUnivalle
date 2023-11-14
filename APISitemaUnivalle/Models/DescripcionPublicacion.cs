using System;
using System.Collections.Generic;

namespace APISitemaUnivalle.Models
{
    public partial class DescripcionPublicacion
    {
        public int IdDescripcion { get; set; }
        public string Contenido { get; set; } = null!;
        public bool Estado { get; set; }
        public int IdPublicacion { get; set; }

        public virtual Publicacion IdPublicacionNavigation { get; set; } = null!;
    }
}
