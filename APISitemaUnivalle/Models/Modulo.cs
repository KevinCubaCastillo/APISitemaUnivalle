﻿using System;
using System.Collections.Generic;

namespace APISitemaUnivalle.Models
{
    public partial class Modulo
    {
        public Modulo()
        {
            Carreras = new HashSet<Carrera>();
            Horarios = new HashSet<Horario>();
            Publicacions = new HashSet<Publicacion>();
            Referencia = new HashSet<Referencium>();
            Requisitos = new HashSet<Requisito>();
            Servicios = new HashSet<Servicio>();
            Tramites = new HashSet<Tramite>();
            Ubicaciones = new HashSet<Ubicacione>();
            UsuarioModulos = new HashSet<UsuarioModulo>();
        }

        public int Id { get; set; }
        public string Nombremodulo { get; set; } = null!;
        public bool Estado { get; set; }

        public virtual ICollection<Carrera> Carreras { get; set; }
        public virtual ICollection<Horario> Horarios { get; set; }
        public virtual ICollection<Publicacion> Publicacions { get; set; }
        public virtual ICollection<Referencium> Referencia { get; set; }
        public virtual ICollection<Requisito> Requisitos { get; set; }
        public virtual ICollection<Servicio> Servicios { get; set; }
        public virtual ICollection<Tramite> Tramites { get; set; }
        public virtual ICollection<Ubicacione> Ubicaciones { get; set; }
        public virtual ICollection<UsuarioModulo> UsuarioModulos { get; set; }
    }
}
