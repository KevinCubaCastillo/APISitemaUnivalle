using System;
using System.Collections.Generic;

namespace APISitemaUnivalle.Models
{
    public partial class Modificacione
    {
        public int Id { get; set; }
        public int IdMod { get; set; }
        public string NombreTabla { get; set; } = null!;
        public string NombreReg { get; set; } = null!;
        public int IdUsuario { get; set; }
        public DateTime FechaMod { get; set; }
    }
}
