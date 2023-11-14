using System;
using System.Collections.Generic;

namespace APISitemaUnivalle.Models
{
    public partial class Publicacion
    {
        public Publicacion()
        {
            DescripcionPublicacions = new HashSet<DescripcionPublicacion>();
        }

        public int Id { get; set; }
        public string Archivo { get; set; } = null!;
        public int? ServiciosId { get; set; }
        public string Titulo { get; set; } = null!;
        public bool Estado { get; set; }
        public int? IdModulo { get; set; }

        public virtual Modulo? IdModuloNavigation { get; set; }
        public virtual Servicio? Servicios { get; set; }
        public virtual ICollection<DescripcionPublicacion> DescripcionPublicacions { get; set; }
    }
}
