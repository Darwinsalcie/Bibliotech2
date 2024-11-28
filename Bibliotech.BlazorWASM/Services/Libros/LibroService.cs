using Bibliotech.Shared;
using Bibliotech.Shared.Libros;
using System.Net.Http.Json;

namespace Bibliotech.BlazorWASM.Services.Libros
{
    public class LibroService : ILibrosService
    {
        private readonly HttpClient _http;

        public LibroService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<LibroDTO>> GetLibros()
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<List<LibroDTO>>>("api/Libros/GetAllBooks");
            if (result!.Success)
                return result.Value;
            else
                throw new Exception(result.Message);
        }
        public async Task<LibroDTO> GetBookById(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<LibroDTO>>($"api/Libros/GetBookById/{id}");
            if (result!.Success)
                return result.Value;
            else
                throw new Exception(result.Message);
        }
        public async Task<int> Guardar(LibroDTO libro)
        {
            var result = await _http.PostAsJsonAsync("api/Libros/GuardarLibro", libro);
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response!.Success)
                return response.Value;
            else
                throw new Exception(response.Message);
        }
        public async Task<int> Actualizar(LibroDTO libro)
        {
            var result = await _http.PutAsJsonAsync($"api/Libros/ActualizarLibro/{libro.Id}", libro);
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response!.Success)
                return response.Value;
            else
                throw new Exception(response.Message);
        }
        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/Libros/EliminarLibro/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response!.Success)
                return response.Success;
            else
                throw new Exception(response.Message);
        }


    }
}
