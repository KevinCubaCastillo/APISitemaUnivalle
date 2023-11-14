using System;
using System.Collections.Generic;

namespace APISitemaUnivalle.Models
{
    public partial class Atencion
    {
        public int IdAtencion { get; set; }
        public int? IdHorarios { get; set; }
        public int? IdDia { get; set; }
        public bool Estado { get; set; }

        public virtual Dia? IdDiaNavigation { get; set; }
        public virtual Horario? IdHorariosNavigation { get; set; }
    }
}
