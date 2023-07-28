using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WsCinemaOc.Models
{
    public partial class Cinema_OC_CloudContext : DbContext
    {
        public Cinema_OC_CloudContext()
        {
        }

        public Cinema_OC_CloudContext(DbContextOptions<Cinema_OC_CloudContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Genero> Generos { get; set; } = null!;
        public virtual DbSet<Pelicula> Peliculas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genero>(entity =>
            {
                entity.Property(e => e.Nombre).HasMaxLength(255);
            });

            modelBuilder.Entity<Pelicula>(entity =>
            {
                entity.Property(e => e.Titulo).HasMaxLength(255);

                entity.HasOne(d => d.Genero)
                    .WithMany(p => p.Peliculas)
                    .HasForeignKey(d => d.GeneroId)
                    .HasConstraintName("FK__Peliculas__Gener__5EBF139D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
