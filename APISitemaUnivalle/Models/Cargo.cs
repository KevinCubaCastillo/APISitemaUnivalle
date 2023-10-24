using System;
using System.Collections.Generic;

namespace APISitemaUnivalle.Models
{
    public partial class Cargo
    {
        public Cargo()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Nombrecargo { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
