
using Bibliotech.Shared.Notificaciones;

namespace Bibliotech.BlazorWASM.Services.Notificaciones
{
    public interface INotificacionesService
    {
        Task<List<NotificacionDTO>> GetNotificaciones();
        Task<NotificacionDTO> GetNotificacionesById(int id);
        Task<List<NotificacionDTO>> GetNotificacionesByUserId(int userId);
        Task<int> Guardar(NotificacionDTO notificacion);
        Task<int> Actualizar(NotificacionDTO notificacion);
        Task<bool> Eliminar(int id);
    }
}
