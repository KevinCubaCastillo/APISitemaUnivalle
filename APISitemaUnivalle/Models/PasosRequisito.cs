using System;
using System.Collections.Generic;

namespace APISitemaUnivalle.Models
{
    public partial class PasosRequisito
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int RequisitosId { get; set; }
        public bool Estado { get; set; }

        public virtual Requisito Requisitos { get; set; } = null!;
    }
}
