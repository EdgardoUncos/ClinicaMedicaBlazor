using System;
using System.Collections.Generic;

namespace ClinicaMedica.Server.Entities;

public partial class Horarios
{
    public int HorarioId { get; set; }

    public TimeSpan HorarioInicio { get; set; }

    public TimeSpan HorarioFin { get; set; }

    public virtual ICollection<Turnos> Turnos { get; set; } = new List<Turnos>();
}
