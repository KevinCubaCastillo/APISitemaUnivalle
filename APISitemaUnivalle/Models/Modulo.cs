using System;
using System.Collections.Generic;

namespace APISitemaUnivalle.Models
{
    public partial class Modulo
    {
        public Modulo()
        {
            Servicios = new HashSet<Servicio>();
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Nombremodulo { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual ICollection<Servicio> Servicios { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
