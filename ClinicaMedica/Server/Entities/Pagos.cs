using System;
using System.Collections.Generic;

namespace ClinicaMedica.Server.Entities;

public partial class Pagos
{
    public int PagoId { get; set; }

    public int MedioPagoId { get; set; }

    public decimal Monto { get; set; }

    public virtual MedioPagos MedioPago { get; set; } = null!;
}
