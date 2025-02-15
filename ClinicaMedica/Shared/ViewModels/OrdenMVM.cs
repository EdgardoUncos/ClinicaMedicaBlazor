﻿using ClinicaMedica.Shared.DTOs.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaMedica.Shared.ViewModels
{
    public class OrdenMVM
    {
        public PacientesDTO Pacientes { get; set; }

        public List<ServiciosDTO> Servicios { get; set; }

        public List<DetallesVM> Detalles { get; set; }
    }
}
