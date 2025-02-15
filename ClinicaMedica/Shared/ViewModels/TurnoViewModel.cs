using ClinicaMedica.Shared.DTOs.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaMedica.Shared.ViewModels
{
    public class TurnoViewModel
    {
        public DateTime Fecha { get; set; }

        public int IdMedico { get; set; }

        public List<MedicosDTO> Medicos { get; set; }

        public List<HorariosDTO> Horarios { get; set; }
    }
}
