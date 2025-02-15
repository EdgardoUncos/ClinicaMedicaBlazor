using System;
using System.Collections.Generic;

namespace ClinicaMedica.Server.Entities;

public partial class Turnos
{
    public int TurnoId { get; set; }

    public int HorarioId { get; set; }

    public int MedicoId { get; set; }

    public DateTime Fecha { get; set; }

    public bool Asistencia { get; set; }

    public int? PacienteId { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Horarios Horario { get; set; } = null!;

    public virtual Medicos Medico { get; set; } = null!;

    public virtual Pacientes? Paciente { get; set; }
}
