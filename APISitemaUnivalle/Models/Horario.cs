using System;
using System.Collections.Generic;

namespace APISitemaUnivalle.Models
{
    public partial class Horario
    {
        public Horario()
        {
            Atencions = new HashSet<Atencion>();
        }

        public int IdHorarios { get; set; }
        public string HoraInicio { get; set; } = null!;
        public string HoraFin { get; set; } = null!;
        public bool Estado { get; set; }
        public int? IdServicio { get; set; }
        public int? IdModulo { get; set; }

        public virtual Modulo? IdModuloNavigation { get; set; }
        public virtual Servicio? IdServicioNavigation { get; set; }
        public virtual ICollection<Atencion> Atencions { get; set; }
    }
}
