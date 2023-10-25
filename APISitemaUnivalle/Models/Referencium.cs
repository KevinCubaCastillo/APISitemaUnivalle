using System;
using System.Collections.Generic;

namespace APISitemaUnivalle.Models
{
    public partial class Referencium
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Numerocel { get; set; } = null!;
        public int? ServiciosId { get; set; }
        public bool Estado { get; set; }
        public int? IdModulo { get; set; }

        public virtual Modulo? IdModuloNavigation { get; set; }
        public virtual Servicio Servicios { get; set; } = null!;
    }
}
