using System;
using System.Collections.Generic;

namespace APISitemaUnivalle.Models
{
    public partial class UsuarioModulo
    {
        public int Id { get; set; }
        public string CiUsuario { get; set; } = null!;
        public int IdModulo { get; set; }

        public virtual Usuario CiUsuarioNavigation { get; set; } = null!;
        public virtual Modulo IdModuloNavigation { get; set; } = null!;
    }
}
