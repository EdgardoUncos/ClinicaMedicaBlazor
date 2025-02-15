using System;
using System.Collections.Generic;
using ClinicaMedica.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicaMedica.Server.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CitasMedicas> CitasMedicas { get; set; }

    public virtual DbSet<DetalleCitas> DetalleCitas { get; set; }

    public virtual DbSet<Especialidades> Especialidades { get; set; }

    public virtual DbSet<Horarios> Horarios { get; set; }

    public virtual DbSet<Medicos> Medicos { get; set; }

    public virtual DbSet<MedioPagos> MedioPagos { get; set; }

    public virtual DbSet<Pacientes> Pacientes { get; set; }

    public virtual DbSet<Pagos> Pagos { get; set; }

    public virtual DbSet<Paquetes> Paquetes { get; set; }

    public virtual DbSet<Personas> Personas { get; set; }

    public virtual DbSet<Servicios> Servicios { get; set; }

    public virtual DbSet<Turnos> Turnos { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=SQL1002.site4now.net;Initial Catalog=db_ab1ffb_hackacode;User Id=db_ab1ffb_hackacode_admin;Password=nelectronica2216");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CitasMedicas>(entity =>
        {
            entity.HasKey(e => e.CitaMedicaId);

            entity.HasIndex(e => e.MedicoId, "IX_CitasMedicas_MedicoId");

            entity.HasIndex(e => e.PacienteId, "IX_CitasMedicas_PacienteId");

            entity.HasOne(d => d.Medico).WithMany(p => p.CitasMedicas)
                .HasForeignKey(d => d.MedicoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Paciente).WithMany(p => p.CitasMedicas)
                .HasForeignKey(d => d.PacienteId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<DetalleCitas>(entity =>
        {
            entity.HasKey(e => new { e.CitaMedicaId, e.ServicioId });

            entity.HasIndex(e => e.ServicioId, "IX_DetalleCitas_ServicioId");

            entity.Property(e => e.Cantidad).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.CitaMedica).WithMany(p => p.DetalleCitas)
                .HasForeignKey(d => d.CitaMedicaId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Servicio).WithMany(p => p.DetalleCitas)
                .HasForeignKey(d => d.ServicioId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Especialidades>(entity =>
        {
            entity.HasKey(e => e.EspecialidadId);

            entity.Property(e => e.Detalle).HasMaxLength(300);
        });

        modelBuilder.Entity<Horarios>(entity =>
        {
            entity.HasKey(e => e.HorarioId);
        });

        modelBuilder.Entity<Medicos>(entity =>
        {
            entity.HasKey(e => e.MedicoId);

            entity.HasIndex(e => e.EspecialidadId, "IX_Medicos_EspecialidadId");

            entity.HasIndex(e => e.PersonaId, "IX_Medicos_PersonaId");

            entity.Property(e => e.Sueldo).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Especialidad).WithMany(p => p.Medicos)
                .HasForeignKey(d => d.EspecialidadId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Persona).WithMany(p => p.Medicos)
                .HasForeignKey(d => d.PersonaId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<MedioPagos>(entity =>
        {
            entity.HasKey(e => e.MedioPagoId);

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Pacientes>(entity =>
        {
            entity.HasKey(e => e.PacienteId);

            entity.HasIndex(e => e.PersonaId, "IX_Pacientes_PersonaId");

            entity.HasOne(d => d.Persona).WithMany(p => p.Pacientes).HasForeignKey(d => d.PersonaId);
        });

        modelBuilder.Entity<Pagos>(entity =>
        {
            entity.HasKey(e => e.PagoId);

            entity.HasIndex(e => e.MedioPagoId, "IX_Pagos_MedioPagoId");

            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MedioPago).WithMany(p => p.Pagos).HasForeignKey(d => d.MedioPagoId);
        });

        modelBuilder.Entity<Paquetes>(entity =>
        {
            entity.HasKey(e => e.PaqueteId);

            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasMany(d => d.Servicio).WithMany(p => p.Paquete)
                .UsingEntity<Dictionary<string, object>>(
                    "PaquetesServicios",
                    r => r.HasOne<Servicios>().WithMany().HasForeignKey("ServicioId"),
                    l => l.HasOne<Paquetes>().WithMany().HasForeignKey("PaqueteId"),
                    j =>
                    {
                        j.HasKey("PaqueteId", "ServicioId");
                        j.HasIndex(new[] { "ServicioId" }, "IX_PaquetesServicios_ServicioId");
                    });
        });

        modelBuilder.Entity<Personas>(entity =>
        {
            entity.HasKey(e => e.PersonaId);

            entity.Property(e => e.Apellido).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<Servicios>(entity =>
        {
            entity.HasKey(e => e.ServicioId);

            entity.Property(e => e.Descripcion).HasMaxLength(300);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Turnos>(entity =>
        {
            entity.HasKey(e => e.TurnoId);

            entity.HasIndex(e => e.MedicoId, "IX_Turnos_MedicoId");

            entity.HasIndex(e => e.PacienteId, "IX_Turnos_PacienteId");

            entity.HasIndex(e => new { e.HorarioId, e.MedicoId, e.Fecha }, "UQ_Turnos_Horario_Medico_Fecha").IsUnique();

            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("(N'Disponible')");
            entity.Property(e => e.Fecha).HasColumnType("date");

            entity.HasOne(d => d.Horario).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.HorarioId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Medico).WithMany(p => p.Turnos).HasForeignKey(d => d.MedicoId);

            entity.HasOne(d => d.Paciente).WithMany(p => p.Turnos).HasForeignKey(d => d.PacienteId);
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.UsuarioId);

            entity.Property(e => e.NombreUsuario).HasMaxLength(50);
            entity.Property(e => e.Rol).HasMaxLength(20);
            entity.Property(e => e.Token).HasDefaultValueSql("(N'')");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
