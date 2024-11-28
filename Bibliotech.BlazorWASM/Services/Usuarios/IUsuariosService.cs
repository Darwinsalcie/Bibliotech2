using Bibliotech.Shared.Usuarios;

namespace Bibliotech.BlazorWASM.Services.Usuarios
{
    public interface IUsuariosService
    {
        Task<List<UsuarioDTO>> GetUsuarios();
        Task<UsuarioDTO> GetUsuarioById(int id);
        Task<int> Guardar(UsuarioDTO usuario);
        Task<int> Actualizar(UsuarioDTO usuario);
        Task<bool> Eliminar(int id);
    }
}
