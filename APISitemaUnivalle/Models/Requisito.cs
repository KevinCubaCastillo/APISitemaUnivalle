using System;
using System.Collections.Generic;

namespace APISitemaUnivalle.Models
{
    public partial class Requisito
    {
        public Requisito()
        {
            PasosRequisitos = new HashSet<PasosRequisito>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public int? ServiciosId { get; set; }
        public bool Estado { get; set; }
        public int? IdModulo { get; set; }

        public virtual Modulo? IdModuloNavigation { get; set; }
        public virtual Servicio Servicios { get; set; } = null!;
        public virtual ICollection<PasosRequisito> PasosRequisitos { get; set; }
    }
}
