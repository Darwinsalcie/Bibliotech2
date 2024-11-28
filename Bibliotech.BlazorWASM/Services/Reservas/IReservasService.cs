using Bibliotech.Shared.Libros;
using Bibliotech.Shared.Reserva;

namespace Bibliotech.BlazorWASM.Services.Reservas
{
    public interface IReservasService
    {
        Task<List<ReservaDTO>> GetReservas();
        Task<ReservaDTO> GetReservaById(int id);
        Task<List<ReservaDTO>> GetReservasByUserId(int userId);
        Task<int> Guardar(ReservaDTO reserva);
        Task<int> Actualizar(ReservaDTO reserva);
        Task<bool> Eliminar(int id);
    }
}
