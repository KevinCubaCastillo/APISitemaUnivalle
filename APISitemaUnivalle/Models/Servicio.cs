using System;
using System.Collections.Generic;

namespace APISitemaUnivalle.Models
{
    public partial class Servicio
    {
        public Servicio()
        {
            Carreras = new HashSet<Carrera>();
            Personals = new HashSet<Personal>();
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

        public virtual Modulo Modulo { get; set; } = null!;
        public virtual ICollection<Carrera> Carreras { get; set; }
        public virtual ICollection<Personal> Personals { get; set; }
        public virtual ICollection<Publicacion> Publicacions { get; set; }
        public virtual ICollection<Referencium> Referencia { get; set; }
        public virtual ICollection<Requisito> Requisitos { get; set; }
        public virtual ICollection<Tramite> Tramites { get; set; }
        public virtual ICollection<Ubicacione> Ubicaciones { get; set; }
    }
}
