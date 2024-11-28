using Bibliotech.Shared.Libros;

namespace Bibliotech.BlazorWASM.Services.Libros
{
    public interface ILibrosService
    {
        Task<List<LibroDTO>> GetLibros();
        Task<LibroDTO> GetBookById(int id);
        Task<int> Guardar(LibroDTO libro);
        Task<int> Actualizar(LibroDTO libro);
        Task<bool> Eliminar(int id);
    }
}
