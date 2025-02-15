using System;
using System.Collections.Generic;

namespace ClinicaMedica.Server.Entities;

public partial class Pacientes
{
    public int PacienteId { get; set; }

    public int PersonaId { get; set; }

    public bool ObraSocial { get; set; }

    public virtual ICollection<CitasMedicas> CitasMedicas { get; set; } = new List<CitasMedicas>();

    public virtual Personas Persona { get; set; } = null!;

    public virtual ICollection<Turnos> Turnos { get; set; } = new List<Turnos>();
}
