using Bibliotech.Shared;
using System.Net.Http.Json;
using Bibliotech.Shared.Notificaciones;

namespace Bibliotech.BlazorWASMCliente.Services.Notificaciones;

public class NotificacionesService : INotificacionesService
{
    private readonly HttpClient _http;

    public NotificacionesService(HttpClient http)
    {
        _http = http;
    }
    public async Task<List<NotificacionDTO>> GetNotificaciones()
    {
        var result = await _http.GetFromJsonAsync<ResponseApi<List<NotificacionDTO>>>("api/Notificaciones/GetAllNotificaciones");
        if (result!.Success)
            return result.Value;
        else
            throw new Exception(result.Message);
    }
    public async Task<NotificacionDTO> GetNotificacionesById(int id)
    {
        var result = await _http.GetFromJsonAsync<ResponseApi<NotificacionDTO>>($"api/Notificaciones/GetNotificacionesById/{id}");
        if (result!.Success)
            return result.Value;
        else
            throw new Exception(result.Message);
    }
    public async Task<List<NotificacionDTO>> GetNotificacionesByUserId(int userId)
    {
        var result = await _http.GetFromJsonAsync<ResponseApi<List<NotificacionDTO>>>($"api/Notificaciones/GetNotificacionesByUserId/{userId}");
        if (result!.Success)
            return result.Value;
        else
            throw new Exception(result.Message);
    }
    public async Task<int> Guardar(NotificacionDTO notificacion)
    {
        var result = await _http.PostAsJsonAsync("api/Notificaciones/GuardarNotificacion", notificacion);
        var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

        if (response!.Success)
            return response.Value;
        else
            throw new Exception(response.Message);
    }

    public async Task<int> Actualizar(NotificacionDTO notificacion)
    {
        var result = await _http.PutAsJsonAsync($"api/Notificaciones/ActualizarNotificacion/{notificacion.Id}", notificacion);
        var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

        if (response!.Success)
            return response.Value;
        else
            throw new Exception(response.Message);
    }

    public async Task<bool> Eliminar(int id)
    {
        var result = await _http.DeleteAsync($"api/Notificaciones/EliminarNotificacion/{id}");
        var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

        if (response!.Success)
            return response.Success;
        else
            throw new Exception(response.Message);
    }



}

