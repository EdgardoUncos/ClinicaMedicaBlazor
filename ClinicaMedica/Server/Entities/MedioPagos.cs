using System;
using System.Collections.Generic;

namespace ClinicaMedica.Server.Entities;

public partial class MedioPagos
{
    public int MedioPagoId { get; set; }

    public string Nombre { get; set; } = null!;

    public bool EstaActivo { get; set; }

    public virtual ICollection<Pagos> Pagos { get; set; } = new List<Pagos>();
}
