using System;
using System.Collections.Generic;

namespace ClinicaMedica.Server.Entities;

public partial class Especialidades
{
    public int EspecialidadId { get; set; }

    public string Detalle { get; set; } = null!;

    public virtual ICollection<Medicos> Medicos { get; set; } = new List<Medicos>();
}
