using System;
using System.Collections.Generic;

namespace APISitemaUnivalle.Models
{
    public partial class Tramite
    {
        public int Id { get; set; }
        public string Tiempotramite { get; set; } = null!;
        public bool Estado { get; set; }
        public int? ServiciosId { get; set; }
        public int? IdModulo { get; set; }

        public virtual Modulo? IdModuloNavigation { get; set; }
        public virtual Servicio? Servicios { get; set; }
    }
}
