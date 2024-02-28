using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IO.Swagger.LaboratorioDB;

public partial class LaboratorioDbContext : DbContext
{
    public LaboratorioDbContext()
    {
    }

    public LaboratorioDbContext(DbContextOptions<LaboratorioDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ExamenesMuestra> ExamenesMuestras { get; set; }

    public virtual DbSet<Laboratoristum> Laboratorista { get; set; }

    public virtual DbSet<Muestra> Muestras { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-75JRR1C; Database=LaboratorioDB; Trusted_Connection=True; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        modelBuilder.Entity<ExamenesMuestra>(entity =>
        {
            entity.HasKey(e => e.IdExamenMuestra).HasName("PK__Examenes__F11D60121459CD9E");

            entity.ToTable("ExamenesMuestra");

            entity.Property(e => e.IdExamenMuestra).ValueGeneratedNever();
            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.Estado).HasMaxLength(20);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.UsuarioValidacion).HasMaxLength(50);
            entity.Property(e => e.Valor).HasMaxLength(50);

            entity.HasOne(d => d.Muestra).WithMany(p => p.ExamenesMuestras)
                .HasForeignKey(d => d.MuestraId)
                .HasConstraintName("FK__ExamenesM__Muest__5812160E");
        });

        modelBuilder.Entity<Laboratoristum>(entity =>
        {
            entity.HasKey(e => e.IdLaboratorista).HasName("PK__Laborato__4FE85BC0483DAB99");

            entity.Property(e => e.IdLaboratorista).ValueGeneratedNever();
            entity.Property(e => e.Apellido).HasMaxLength(50);
            entity.Property(e => e.Especialidad).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(50);

            entity.HasOne(d => d.Sucursal).WithMany(p => p.Laboratorista)
                .HasForeignKey(d => d.SucursalId)
                .HasConstraintName("FK__Laborator__Sucur__4CA06362");
        });

        modelBuilder.Entity<Muestra>(entity =>
        {
            entity.HasKey(e => e.IdMuestra).HasName("PK__Muestra__C3A7DA6F324D3275");

            entity.ToTable("Muestra");

            entity.Property(e => e.IdMuestra).ValueGeneratedNever();
            entity.Property(e => e.CodigoBarras).HasMaxLength(20);
            entity.Property(e => e.Estado).HasMaxLength(20);

            entity.HasOne(d => d.Paciente).WithMany(p => p.Muestras)
                .HasForeignKey(d => d.PacienteId)
                .HasConstraintName("FK__Muestra__Pacient__5441852A");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.Muestras)
                .HasForeignKey(d => d.SucursalId)
                .HasConstraintName("FK__Muestra__Sucursa__5535A963");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.IdPaciente).HasName("PK__Paciente__C93DB49B5825791F");

            entity.ToTable("Paciente");

            entity.Property(e => e.IdPaciente).ValueGeneratedNever();
            entity.Property(e => e.ApellidoPaciente).HasMaxLength(50);
            entity.Property(e => e.Direccion).HasMaxLength(100);
            entity.Property(e => e.Genero).HasMaxLength(10);
            entity.Property(e => e.NombrePaciente).HasMaxLength(50);
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.IdSucursal).HasName("PK__Sucursal__BFB6CD99013D51F6");

            entity.ToTable("Sucursal");

            entity.Property(e => e.IdSucursal).ValueGeneratedNever();
            entity.Property(e => e.subdominio).HasMaxLength(100);
            entity.Property(e => e.Encargado).HasMaxLength(50);
            entity.Property(e => e.NombreSucursal).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(15);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
