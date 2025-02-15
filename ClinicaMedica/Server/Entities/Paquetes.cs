using System;
using System.Collections.Generic;

namespace ClinicaMedica.Server.Entities;

public partial class Paquetes
{
    public int PaqueteId { get; set; }

    public int Nombre { get; set; }

    public decimal Precio { get; set; }

    public virtual ICollection<Servicios> Servicio { get; set; } = new List<Servicios>();
}
