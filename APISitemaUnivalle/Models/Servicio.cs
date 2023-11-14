using System;
using System.Collections.Generic;

namespace APISitemaUnivalle.Models
{
    public partial class Servicio
    {
        public Servicio()
        {
            Carreras = new HashSet<Carrera>();
            Horarios = new HashSet<Horario>();
            Publicacions = new HashSet<Publicacion>();
            Referencia = new HashSet<Referencium>();
            Requisitos = new HashSet<Requisito>();
            Tramites = new HashSet<Tramite>();
            Ubicaciones = new HashSet<Ubicacione>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int ModuloId { get; set; }
        public bool Estado { get; set; }
        public string? ImagenUrl { get; set; }
        public int? IdCategoria { get; set; }

        public virtual Categorium? IdCategoriaNavigation { get; set; }
        public virtual Modulo Modulo { get; set; } = null!;
        public virtual ICollection<Carrera> Carreras { get; set; }
        public virtual ICollection<Horario> Horarios { get; set; }
        public virtual ICollection<Publicacion> Publicacions { get; set; }
        public virtual ICollection<Referencium> Referencia { get; set; }
        public virtual ICollection<Requisito> Requisitos { get; set; }
        public virtual ICollection<Tramite> Tramites { get; set; }
        public virtual ICollection<Ubicacione> Ubicaciones { get; set; }
    }
}
