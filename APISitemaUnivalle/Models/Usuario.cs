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

        public string CiUsuario { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public bool Estado { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public int CargoId { get; set; }

        public virtual Cargo Cargo { get; set; } = null!;
        public virtual ICollection<UsuarioModulo> UsuarioModulos { get; set; }
    }
}
