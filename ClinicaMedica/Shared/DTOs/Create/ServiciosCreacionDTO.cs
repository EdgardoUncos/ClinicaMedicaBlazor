using System.ComponentModel.DataAnnotations;

namespace ClinicaMedica.Shared.DTOs.Create
{
    public class ServiciosCreacionDTO
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public float Precio { get; set; }
    }
}
