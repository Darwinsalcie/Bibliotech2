using Bibliotech.Shared;
using Bibliotech.Shared.Libros;
using Bibliotech.Shared.Role;
using System.Net.Http.Json;

namespace Bibliotech.BlazorWASM.Services.Roles;

public class RolesService : IRolesService
{
    private readonly HttpClient _http;

    public RolesService(HttpClient http)
    {
        _http = http;
    }
    public async Task<List<RoleDTO>> GetRoles()
    {
        var result = await _http.GetFromJsonAsync<ResponseApi<List<RoleDTO>>>("api/Roles/GetAllRoles");
        if (result!.Success)
            return result.Value;
        else
            throw new Exception(result.Message);
    }
    public async Task<RoleDTO> GetRolesById(int id)
    {
        var result = await _http.GetFromJsonAsync<ResponseApi<RoleDTO>>($"api/Roles/GetRoleById/{id}");
        if (result!.Success)
            return result.Value;
        else
            throw new Exception(result.Message);
    }
    public async Task<int> Guardar(RoleDTO role)
    {
        var result = await _http.PostAsJsonAsync("api/Roles/GuardarRole", role);
        var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

        if (response!.Success)
            return response.Value;
        else
            throw new Exception(response.Message);
    }

    public async Task<int> Actualizar(RoleDTO role)
    {
        var result = await _http.PutAsJsonAsync($"api/Roles/ActualizarRole/{role.Id}", role);
        var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

        if (response!.Success)
            return response.Value;
        else
            throw new Exception(response.Message);
    }

    public async Task<bool> Eliminar(int id)
    {
        var result = await _http.DeleteAsync($"api/Roles/EliminarRole/{id}");
        var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

        if (response!.Success)
            return response.Success;
        else
            throw new Exception(response.Message);
    }
}

