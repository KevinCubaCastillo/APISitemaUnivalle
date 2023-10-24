using System;
using System.Collections.Generic;

namespace APISitemaUnivalle.Models
{
    public partial class Categorium
    {
        public Categorium()
        {
            Servicios = new HashSet<Servicio>();
        }

        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual ICollection<Servicio> Servicios { get; set; }
    }
}
