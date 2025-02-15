using ClinicaMedica.Shared.DTOs.Basic;
using System.Net.Http.Json;

namespace ClinicaMedica.Client.Services
{
    public class MedicosService
    {
        private readonly HttpClient _httpClient;

        public MedicosService(HttpClient httpClientFactory)
        {
            _httpClient = httpClientFactory;
        }

        public async Task<List<MedicosDTO>> ObtenerMedicosAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<MedicosDTO>>("api/Medicos"); 

            }
            catch (Exception ex)
            {
                return null;
                
            }
        }
    }
}
