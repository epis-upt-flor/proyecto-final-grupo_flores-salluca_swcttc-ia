using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SistemaArtemis.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Calificacion> Calificacion { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Estado_Servicio> Estado_Servicio { get; set; }
        public virtual DbSet<Estado_Tecnico> Estado_Tecnico { get; set; }
        public virtual DbSet<Modelo_Ia> Modelo_Ia { get; set; }
        public virtual DbSet<Problema> Problema { get; set; }
        public virtual DbSet<Servicio> Servicio { get; set; }
        public virtual DbSet<Tecnico> Tecnico { get; set; }
        public virtual DbSet<Tecnico_TipoEspecialidad> Tecnico_TipoEspecialidad { get; set; }
        public virtual DbSet<Tipo_Usuario> Tipo_Usuario { get; set; }
        public virtual DbSet<TipoEspecialidad> TipoEspecialidad { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Calificacion>()
                .Property(e => e.Comentario)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Problema)
                .WithRequired(e => e.Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Servicio)
                .WithRequired(e => e.Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Estado_Servicio>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Estado_Servicio>()
                .HasMany(e => e.Servicio)
                .WithRequired(e => e.Estado_Servicio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Estado_Tecnico>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Modelo_Ia>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Modelo_Ia>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<Modelo_Ia>()
                .Property(e => e.precision)
                .HasPrecision(5, 4);

            modelBuilder.Entity<Problema>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Problema>()
                .Property(e => e.Documento)
                .IsUnicode(false);

            modelBuilder.Entity<Problema>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<Problema>()
                .HasMany(e => e.Servicio)
                .WithRequired(e => e.Problema)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Servicio>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Tecnico>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Tecnico>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Tecnico>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Tecnico>()
                .Property(e => e.Especialidad)
                .IsUnicode(false);

            modelBuilder.Entity<Tecnico>()
                .HasMany(e => e.Servicio)
                .WithRequired(e => e.Tecnico)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tecnico>()
                .HasMany(e => e.Tecnico_TipoEspecialidad)
                .WithRequired(e => e.Tecnico)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tipo_Usuario>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<TipoEspecialidad>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<TipoEspecialidad>()
                .HasMany(e => e.Servicio)
                .WithRequired(e => e.TipoEspecialidad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoEspecialidad>()
                .HasMany(e => e.Tecnico_TipoEspecialidad)
                .WithRequired(e => e.TipoEspecialidad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Correo)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Cliente)
                .WithRequired(e => e.Usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Tecnico)
                .WithRequired(e => e.Usuario)
                .WillCascadeOnDelete(false);
        }
    }
}
