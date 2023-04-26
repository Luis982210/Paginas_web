using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ProyectoModels.Models;

public partial class ProyectoPaW2Context : DbContext
{
    public ProyectoPaW2Context()
    {
    }

    public ProyectoPaW2Context(DbContextOptions<ProyectoPaW2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Mobiliario> Mobiliarios { get; set; }

    public virtual DbSet<Sala> Salas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();
            var connectionString = configuration.GetConnectionString("conexion");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mobiliario>(entity =>
        {
            entity.HasKey(e => e.IdMobiliario);

            entity.ToTable("Mobiliario");

            entity.Property(e => e.IdMobiliario).HasColumnName("ID_Mobiliario");
            entity.Property(e => e.Descripcion).HasMaxLength(50);
            entity.Property(e => e.IdSala).HasColumnName("ID_Sala");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Precio).HasMaxLength(50);
        });

        modelBuilder.Entity<Sala>(entity =>
        {
            entity.HasKey(e => e.IdSala);

            entity.ToTable("Sala");

            entity.Property(e => e.IdSala).HasColumnName("ID_Sala");
            entity.Property(e => e.Departamento).HasMaxLength(50);
            entity.Property(e => e.Encargado).HasMaxLength(50);
            entity.Property(e => e.Ubicacion).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");
            entity.Property(e => e.Contrasena).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
