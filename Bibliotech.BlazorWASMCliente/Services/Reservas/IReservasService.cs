using Bibliotech.Shared;
using Bibliotech.Shared.Libros;
using Bibliotech.Shared.Reserva;

namespace Bibliotech.BlazorWASMCliente.Services.Reservas
{
    public interface IReservasService
    {
        Task<List<ReservaDTO>> GetReservas();
        Task<ReservaDTO> GetReservaById(int id);
        Task<List<ReservaDTO>> GetReservasByUserId(int userId);
        Task<ResponseApi<int>> Guardar(ReservaDTO reserva);
        Task<int> Actualizar(ReservaDTO reserva);
        Task<bool> Eliminar(int id);
    }
}
