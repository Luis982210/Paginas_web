using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ProyectoModels;


public partial class ProyectoPaWContext : DbContext
{
    public ProyectoPaWContext()
    {
    }

    public ProyectoPaWContext(DbContextOptions<ProyectoPaWContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Codigo> Codigos { get; set; }

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
        modelBuilder.Entity<Codigo>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Codigo1).HasColumnName("Codigo");
            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<Mobiliario>(entity =>
        {
            entity.ToTable("Mobiliario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CodigoMobiliario).HasColumnName("Codigo_Mobiliario");
            entity.Property(e => e.Descripcion).HasMaxLength(50);
            entity.Property(e => e.IdSala).HasColumnName("id_Sala");
            entity.Property(e => e.Mobiliario1)
                .HasMaxLength(50)
                .HasColumnName("Mobiliario");
            entity.Property(e => e.Precio).HasColumnType("money");
        });

        modelBuilder.Entity<Sala>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Departamento).HasMaxLength(50);
            entity.Property(e => e.Encargado).HasMaxLength(100);
            entity.Property(e => e.NombreSala)
                .HasMaxLength(50)
                .HasColumnName("Nombre_Sala");
            entity.Property(e => e.Ubicacion).HasMaxLength(150);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(50)
                .HasColumnName("contrasena");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
