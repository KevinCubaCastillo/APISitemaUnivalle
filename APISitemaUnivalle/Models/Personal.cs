using System;
using System.Collections.Generic;

namespace APISitemaUnivalle.Models
{
    public partial class Personal
    {
        public Personal()
        {
            Cargos = new HashSet<Cargo>();
        }

        public int Id { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public int ServiciosId { get; set; }
        public bool Estado { get; set; }
        public int UsuariosId { get; set; }

        public virtual Servicio Servicios { get; set; } = null!;
        public virtual ICollection<Cargo> Cargos { get; set; }
    }
}
