using ClinicaMedica.Shared.DTOs.Basic;
using System.Net.Http.Json;
using System.Net.Http;

namespace ClinicaMedica.Client.Services
{
    public class HorariosService
    {
        private readonly HttpClient _httpClient;

        public HorariosService(HttpClient httpClientFactory)
        {
            _httpClient = httpClientFactory;
        }

        public async Task<List<HorariosDTO>> ObtenerHorariosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<HorariosDTO>>("api/Horarios");
        }
    }
}
