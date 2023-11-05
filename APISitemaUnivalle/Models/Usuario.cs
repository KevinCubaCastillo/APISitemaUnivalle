using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APISitemaUnivalle.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Modulos = new HashSet<Modulo>();
        }

        public string CiUsuario { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public bool Estado { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public int CargoId { get; set; }

        public virtual Cargo Cargo { get; set; } = null!;
        public virtual ICollection<Modulo> Modulos { get; set; }
    }
}
