using System;
using System.Collections.Generic;

namespace APISitemaUnivalle.Models
{
    public partial class Publicacion
    {
        public int Id { get; set; }
        public string Archivo { get; set; } = null!;
        public string? Descripcion1 { get; set; }
        public int ServiciosId { get; set; }
        public string Titulo { get; set; } = null!;
        public string? Descripcion2 { get; set; }
        public bool Estado { get; set; }

        public virtual Servicio Servicios { get; set; } = null!;
    }
}
