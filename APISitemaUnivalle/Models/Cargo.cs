using System;
using System.Collections.Generic;

namespace APISitemaUnivalle.Models
{
    public partial class Cargo
    {
        public int Id { get; set; }
        public string Nombrecargo { get; set; } = null!;
        public int PersonalId { get; set; }
        public bool Estado { get; set; }

        public virtual Personal Personal { get; set; } = null!;
    }
}
