using System;
using System.Collections.Generic;

namespace APISitemaUnivalle.Models
{
    public partial class Ubicacione
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public string? Imagen { get; set; }
        public string? Video { get; set; }
        public int ServiciosId { get; set; }
        public bool Estado { get; set; }
        public int ModuloId { get; set; }

        public virtual Modulo Modulo { get; set; } = null!;
        public virtual Servicio Servicios { get; set; } = null!;
    }
}
