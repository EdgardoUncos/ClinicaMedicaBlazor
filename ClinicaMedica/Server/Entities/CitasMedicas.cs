using System;
using System.Collections.Generic;

namespace ClinicaMedica.Server.Entities;

public partial class CitasMedicas
{
    public int CitaMedicaId { get; set; }

    public int PacienteId { get; set; }

    public int MedicoId { get; set; }

    public float Descuento { get; set; }

    public float Total { get; set; }

    public virtual ICollection<DetalleCitas> DetalleCitas { get; set; } = new List<DetalleCitas>();

    public virtual Medicos Medico { get; set; } = null!;

    public virtual Pacientes Paciente { get; set; } = null!;
}
