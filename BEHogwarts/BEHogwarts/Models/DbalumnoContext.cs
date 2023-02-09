using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BEHogwarts.Models;

public partial class DbalumnoContext : DbContext
{
    public DbalumnoContext()
    {
    }

    public DbalumnoContext(DbContextOptions<DbalumnoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__alumno__3213E83FEB310DA6");

            entity.ToTable("alumno");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Casa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("casa");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("identificacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Edad)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("edad");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
