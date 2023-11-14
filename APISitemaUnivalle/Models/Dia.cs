using System;
using System.Collections.Generic;

namespace APISitemaUnivalle.Models
{
    public partial class Dia
    {
        public Dia()
        {
            Atencions = new HashSet<Atencion>();
        }

        public int IdDias { get; set; }
        public string NombreDia { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual ICollection<Atencion> Atencions { get; set; }
    }
}
