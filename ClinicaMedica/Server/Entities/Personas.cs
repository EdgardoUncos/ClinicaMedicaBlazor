using System;
using System.Collections.Generic;

namespace ClinicaMedica.Server.Entities;

public partial class Personas
{
    public int PersonaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public int Dni { get; set; }

    public string Telefono { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public virtual ICollection<Medicos> Medicos { get; set; } = new List<Medicos>();

    public virtual ICollection<Pacientes> Pacientes { get; set; } = new List<Pacientes>();
}
