using System;
using System.Collections.Generic;

namespace APISitemaUnivalle.Models
{
    public partial class Carrera
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int ServiciosId { get; set; }
        public bool Estado { get; set; }

        public virtual Servicio Servicios { get; set; } = null!;
    }
}
