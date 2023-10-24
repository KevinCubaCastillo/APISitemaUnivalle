using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APISitemaUnivalle.Models
{
    public partial class dbUnivalleContext : DbContext
    {
        public dbUnivalleContext()
        {
        }

        public dbUnivalleContext(DbContextOptions<dbUnivalleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cargo> Cargos { get; set; } = null!;
        public virtual DbSet<Carrera> Carreras { get; set; } = null!;
        public virtual DbSet<Categorium> Categoria { get; set; } = null!;
        public virtual DbSet<DescripcionPublicacion> DescripcionPublicacions { get; set; } = null!;
        public virtual DbSet<Modificacione> Modificaciones { get; set; } = null!;
        public virtual DbSet<Modulo> Modulos { get; set; } = null!;
        public virtual DbSet<PasosRequisito> PasosRequisitos { get; set; } = null!;
        public virtual DbSet<Publicacion> Publicacions { get; set; } = null!;
        public virtual DbSet<Referencium> Referencia { get; set; } = null!;
        public virtual DbSet<Requisito> Requisitos { get; set; } = null!;
        public virtual DbSet<Servicio> Servicios { get; set; } = null!;
        public virtual DbSet<Tramite> Tramites { get; set; } = null!;
        public virtual DbSet<Ubicacione> Ubicaciones { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=buzk2uterw6mfakgkyig-postgresql.services.clever-cloud.com;Database=buzk2uterw6mfakgkyig;Username=u7hypr2dfcfjvdrcdyb5;Password=e6mJGvcYuLMsoDyiv5xNp5xppUwKh0;Port=5432;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pg_catalog", "adminpack")
                .HasPostgresExtension("autoinc")
                .HasPostgresExtension("btree_gin")
                .HasPostgresExtension("btree_gist")
                .HasPostgresExtension("citext")
                .HasPostgresExtension("cube")
                .HasPostgresExtension("dblink")
                .HasPostgresExtension("dict_int")
                .HasPostgresExtension("dict_xsyn")
                .HasPostgresExtension("earthdistance")
                .HasPostgresExtension("file_fdw")
                .HasPostgresExtension("fuzzystrmatch")
                .HasPostgresExtension("hstore")
                .HasPostgresExtension("insert_username")
                .HasPostgresExtension("intagg")
                .HasPostgresExtension("intarray")
                .HasPostgresExtension("isn")
                .HasPostgresExtension("lo")
                .HasPostgresExtension("ltree")
                .HasPostgresExtension("moddatetime")
                .HasPostgresExtension("pageinspect")
                .HasPostgresExtension("pg_buffercache")
                .HasPostgresExtension("pg_freespacemap")
                .HasPostgresExtension("pg_stat_statements")
                .HasPostgresExtension("pg_trgm")
                .HasPostgresExtension("pgcrypto")
                .HasPostgresExtension("pgrowlocks")
                .HasPostgresExtension("pgstattuple")
                .HasPostgresExtension("postgis")
                .HasPostgresExtension("refint")
                .HasPostgresExtension("seg")
                .HasPostgresExtension("sslinfo")
                .HasPostgresExtension("tablefunc")
                .HasPostgresExtension("tcn")
                .HasPostgresExtension("timetravel")
                .HasPostgresExtension("unaccent")
                .HasPostgresExtension("uuid-ossp")
                .HasPostgresExtension("xml2")
                .HasPostgresExtension("tiger", "postgis_tiger_geocoder");

            modelBuilder.Entity<Cargo>(entity =>
            {
                entity.ToTable("cargo");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('cargo_id_seq1'::regclass)");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Nombrecargo)
                    .HasMaxLength(50)
                    .HasColumnName("nombrecargo");
            });

            modelBuilder.Entity<Carrera>(entity =>
            {
                entity.ToTable("carrera");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('carrera_id_seq1'::regclass)");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.ServiciosId).HasColumnName("servicios_id");

                entity.HasOne(d => d.Servicios)
                    .WithMany(p => p.Carreras)
                    .HasForeignKey(d => d.ServiciosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("carrera_servicios_fk");
            });

            modelBuilder.Entity<Categorium>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("categoria_pkey");

                entity.ToTable("categoria");

                entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");

                entity.Property(e => e.Descripcion).HasColumnName("descripcion");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.NombreCategoria).HasColumnName("nombre_categoria");
            });

            modelBuilder.Entity<DescripcionPublicacion>(entity =>
            {
                entity.HasKey(e => e.IdDescripcion)
                    .HasName("descripcion_publicacion_pkey");

                entity.ToTable("descripcion_publicacion");

                entity.Property(e => e.IdDescripcion).HasColumnName("id_descripcion");

                entity.Property(e => e.Contenido).HasColumnName("contenido");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.IdPublicacion).HasColumnName("id_publicacion");

                entity.HasOne(d => d.IdPublicacionNavigation)
                    .WithMany(p => p.DescripcionPublicacions)
                    .HasForeignKey(d => d.IdPublicacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("descripcion_publicacion_id_publicacion_fkey");
            });

            modelBuilder.Entity<Modificacione>(entity =>
            {
                entity.ToTable("modificaciones");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('modificaciones_id_seq1'::regclass)");

                entity.Property(e => e.FechaMod)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("fecha_mod");

                entity.Property(e => e.IdMod).HasColumnName("id_mod");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.NombreReg).HasColumnName("nombre_reg");

                entity.Property(e => e.NombreTabla).HasColumnName("nombre_tabla");
            });

            modelBuilder.Entity<Modulo>(entity =>
            {
                entity.ToTable("modulo");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('modulo_id_seq1'::regclass)");

                entity.Property(e => e.CiUsuario).HasColumnName("ci_usuario");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Nombremodulo)
                    .HasMaxLength(50)
                    .HasColumnName("nombremodulo");

                entity.HasOne(d => d.CiUsuarioNavigation)
                    .WithMany(p => p.Modulos)
                    .HasForeignKey(d => d.CiUsuario)
                    .HasConstraintName("ci_usuario");
            });

            modelBuilder.Entity<PasosRequisito>(entity =>
            {
                entity.ToTable("pasos_requisitos");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('pasos_requisitos_id_seq1'::regclass)");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.RequisitosId).HasColumnName("requisitos_id");

                entity.HasOne(d => d.Requisitos)
                    .WithMany(p => p.PasosRequisitos)
                    .HasForeignKey(d => d.RequisitosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pasos_requisitos_requisitos_fk");
            });

            modelBuilder.Entity<Publicacion>(entity =>
            {
                entity.ToTable("publicacion");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('publicacion_id_seq1'::regclass)");

                entity.Property(e => e.Archivo)
                    .HasColumnType("character varying")
                    .HasColumnName("archivo");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.ServiciosId).HasColumnName("servicios_id");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(50)
                    .HasColumnName("titulo");

                entity.HasOne(d => d.Servicios)
                    .WithMany(p => p.Publicacions)
                    .HasForeignKey(d => d.ServiciosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("publicacion_servicios_fk");
            });

            modelBuilder.Entity<Referencium>(entity =>
            {
                entity.ToTable("referencia");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('referencia_id_seq1'::regclass)");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.Numerocel)
                    .HasMaxLength(25)
                    .HasColumnName("numerocel");

                entity.Property(e => e.ServiciosId).HasColumnName("servicios_id");

                entity.HasOne(d => d.Servicios)
                    .WithMany(p => p.Referencia)
                    .HasForeignKey(d => d.ServiciosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("referencia_servicios_fk");
            });

            modelBuilder.Entity<Requisito>(entity =>
            {
                entity.ToTable("requisitos");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('requisitos_id_seq1'::regclass)");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.ServiciosId).HasColumnName("servicios_id");

                entity.HasOne(d => d.Servicios)
                    .WithMany(p => p.Requisitos)
                    .HasForeignKey(d => d.ServiciosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("requisitos_servicios_fk");
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.ToTable("servicios");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('servicios_id_seq1'::regclass)");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");

                entity.Property(e => e.ImagenUrl)
                    .HasMaxLength(255)
                    .HasColumnName("imagen_url");

                entity.Property(e => e.ModuloId).HasColumnName("modulo_id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Servicios)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("id_categoria");

                entity.HasOne(d => d.Modulo)
                    .WithMany(p => p.Servicios)
                    .HasForeignKey(d => d.ModuloId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("servicios_modulo_fk");
            });

            modelBuilder.Entity<Tramite>(entity =>
            {
                entity.ToTable("tramites");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('tramites_id_seq1'::regclass)");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.ServiciosId).HasColumnName("servicios_id");

                entity.Property(e => e.Tiempotramite)
                    .HasMaxLength(30)
                    .HasColumnName("tiempotramite");

                entity.HasOne(d => d.Servicios)
                    .WithMany(p => p.Tramites)
                    .HasForeignKey(d => d.ServiciosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tramites_servicios_fk");
            });

            modelBuilder.Entity<Ubicacione>(entity =>
            {
                entity.ToTable("ubicaciones");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('ubicaciones_id_seq1'::regclass)");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Imagen)
                    .HasMaxLength(256)
                    .HasColumnName("imagen");

                entity.Property(e => e.ServiciosId).HasColumnName("servicios_id");

                entity.Property(e => e.Video)
                    .HasMaxLength(256)
                    .HasColumnName("video");

                entity.HasOne(d => d.Servicios)
                    .WithMany(p => p.Ubicaciones)
                    .HasForeignKey(d => d.ServiciosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ubicaciones_servicios_fk");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.CiUsuario)
                    .HasName("usuarios_pkey");

                entity.ToTable("usuarios");

                entity.Property(e => e.CiUsuario)
                    .HasColumnName("ci_usuario")
                    .HasDefaultValueSql("nextval('usuarios_id_seq1'::regclass)");

                entity.Property(e => e.Apellidos).HasColumnName("apellidos");

                entity.Property(e => e.CargoId).HasColumnName("cargo_id");

                entity.Property(e => e.Clave)
                    .HasMaxLength(256)
                    .HasColumnName("clave");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Nombres).HasColumnName("nombres");

                entity.HasOne(d => d.Cargo)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.CargoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cargo_id");
            });

            modelBuilder.HasSequence("cargo_id_seq");

            modelBuilder.HasSequence("carrera_id_seq");

            modelBuilder.HasSequence("modificaciones_id_seq");

            modelBuilder.HasSequence("modulo_id_seq");

            modelBuilder.HasSequence("pasos_requisitos_id_seq");

            modelBuilder.HasSequence("personal_id_seq");

            modelBuilder.HasSequence("publicacion_id_seq");

            modelBuilder.HasSequence("referencia_id_seq");

            modelBuilder.HasSequence("requisitos_id_seq");

            modelBuilder.HasSequence("servicios_id_seq");

            modelBuilder.HasSequence("tramites_id_seq");

            modelBuilder.HasSequence("ubicaciones_id_seq");

            modelBuilder.HasSequence("usuarios_id_seq");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
