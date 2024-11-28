using Bibliotech.Shared;
using Bibliotech.Shared.Notificaciones;
using Bibliotech.Shared.Reserva;
using System.Net.Http.Json;

namespace Bibliotech.BlazorWASM.Services.Reservas
{
    public class ReservasService : IReservasService
    {
        private readonly HttpClient _http;

        public ReservasService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<ReservaDTO>> GetReservas()
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<List<ReservaDTO>>>("api/Reservas/GetAllReservas");
            if (result!.Success)
                return result.Value;
            else
                throw new Exception(result.Message);
        }
        public async Task<ReservaDTO> GetReservaById(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<ReservaDTO>>($"api/Reservas/GetReservaById/{id}");
            if (result!.Success)
                return result.Value;
            else
                throw new Exception(result.Message);
        }

        public async Task<List<ReservaDTO>> GetReservasByUserId(int userId)
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<List<ReservaDTO>>>($"api/Reservas/GetReservasByUserId/{userId}");
            if (result!.Success)
                return result.Value;
            else
                throw new Exception(result.Message);
        }
        public async Task<int> Guardar(ReservaDTO reserva)
        {
            var result = await _http.PostAsJsonAsync("api/Reservas/GuardarReserva", reserva);
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response!.Success)
                return response.Value;
            else
                throw new Exception(response.Message);
        }
        public async Task<int> Actualizar(ReservaDTO reserva)
        {
            var result = await _http.PutAsJsonAsync($"api/Reservas/ActualizarReserva/{reserva.Id}", reserva);
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response!.Success)
                return response.Value;
            else
                throw new Exception(response.Message);
        }
        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/Reservas/EliminarReserva/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response!.Success)
                return response.Success;
            else
                throw new Exception(response.Message);
        }
    }
}
