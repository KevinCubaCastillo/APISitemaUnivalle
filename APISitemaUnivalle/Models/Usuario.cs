using System;
using System.Collections.Generic;

namespace APISitemaUnivalle.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            UsuarioModulos = new HashSet<UsuarioModulo>();
        }

        public int Id { get; set; }
        public string? Clave { get; set; } = null!;
        public bool Estado { get; set; }
        public int PersonalId { get; set; }

        public virtual Cargo Cargo { get; set; } = null!;
        public virtual ICollection<UsuarioModulo> UsuarioModulos { get; set; }
    }
}
