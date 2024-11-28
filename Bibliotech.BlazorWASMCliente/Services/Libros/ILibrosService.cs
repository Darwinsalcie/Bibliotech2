using Bibliotech.Shared.Libros;

namespace Bibliotech.BlazorWASMCliente.Services.Libros
{
    public interface ILibrosService
    {
        Task<List<LibroDTO>> GetLibros();
        Task<LibroDTO> GetBookById(int id);
        Task<List<LibroDTO>> GetBookByTittle(string tittle);  // Nuevo método agregado

        Task<int> Guardar(LibroDTO libro);
        Task<int> Actualizar(LibroDTO libro);
        Task<bool> Eliminar(int id);
    }
}
