using System;
using System.Collections.Generic;

namespace ClinicaMedica.Server.Entities;

public partial class Servicios
{
    public int ServicioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public decimal Precio { get; set; }

    public virtual ICollection<DetalleCitas> DetalleCitas { get; set; } = new List<DetalleCitas>();

    public virtual ICollection<Paquetes> Paquete { get; set; } = new List<Paquetes>();
}
