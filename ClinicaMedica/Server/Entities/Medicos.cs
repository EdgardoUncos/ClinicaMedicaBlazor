using System;
using System.Collections.Generic;

namespace ClinicaMedica.Server.Entities;

public partial class Medicos
{
    public int MedicoId { get; set; }

    public int PersonaId { get; set; }

    public int EspecialidadId { get; set; }

    public decimal Sueldo { get; set; }

    public virtual ICollection<CitasMedicas> CitasMedicas { get; set; } = new List<CitasMedicas>();

    public virtual Especialidades Especialidad { get; set; } = null!;

    public virtual Personas Persona { get; set; } = null!;

    public virtual ICollection<Turnos> Turnos { get; set; } = new List<Turnos>();
}
