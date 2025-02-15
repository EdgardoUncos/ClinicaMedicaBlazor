using System;
using System.Collections.Generic;

namespace ClinicaMedica.Server.Entities;

public partial class DetalleCitas
{
    public int CitaMedicaId { get; set; }

    public int ServicioId { get; set; }

    public int Cantidad { get; set; }

    public virtual CitasMedicas CitaMedica { get; set; } = null!;

    public virtual Servicios Servicio { get; set; } = null!;
}
