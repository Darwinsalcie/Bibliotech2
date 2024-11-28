using Bibliotech.Shared;
using Bibliotech.Shared.Usuarios;
using System.Net.Http.Json;

namespace Bibliotech.BlazorWASMCliente.Services.Usuarios
{
    public class UsuarioService : IUsuariosService
    {
        private readonly HttpClient _http;

        public UsuarioService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<UsuarioDTO>> GetUsuarios()
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<List<UsuarioDTO>>>("api/Usuarios/GetAllUsers");
            if (result!.Success)
                return result.Value;
            else
                throw new Exception(result.Message);
        }

        public async Task<UsuarioDTO> GetUsuarioById(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<UsuarioDTO>>($"api/Usuarios/GetUserById/{id}");
            if (result!.Success)
                return result.Value;
            else
                throw new Exception(result.Message);
        }

        //public async Task<int> Guardar(UsuarioDTO usuario)
        //{
        //    var result = await _http.PostAsJsonAsync("api/Usuarios/GuardarUsuario", usuario);
        //    var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

        //    if (response!.Success)
        //        return response.Value;
        //    else
        //        throw new Exception(response.Message);
        //}

        public async Task<ResponseApi<UsuarioDTO>> GetUsuarioByMail(string email)
        {
            var response = await _http.GetFromJsonAsync<ResponseApi<UsuarioDTO>>($"api/usuarios/GetUserByMail/{email}");
            return response;
        }

        public async Task<int> Actualizar(UsuarioDTO usuario)
        {
            var result = await _http.PutAsJsonAsync($"api/Usuarios/ActualizarUsuario/{usuario.Id}", usuario);
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response!.Success)
                return response.Value;
            else
                throw new Exception(response.Message);
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/Usuarios/EliminarUsuario/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response!.Success)
                return response.Success;
            else
                throw new Exception(response.Message);
        }

        public async Task<int> Guardar(UsuarioDTO usuario)
        {
            var result = await _http.PostAsJsonAsync("api/Usuarios/GuardarUsuario", usuario);
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response!.Success)
                return response.Value;
            else
                throw new Exception(response.Message); // Lanza el mensaje de error del servidor
        }




    }
}
