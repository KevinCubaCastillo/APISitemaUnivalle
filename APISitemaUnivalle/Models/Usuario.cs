using System;
using System.Collections.Generic;

namespace APISitemaUnivalle.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Modulos = new HashSet<Modulo>();
        }

        public int Id { get; set; }
        public string? Clave { get; set; } = null!;
        public bool Estado { get; set; }
        public int PersonalId { get; set; }

        public virtual ICollection<Modulo> Modulos { get; set; }
    }
}
