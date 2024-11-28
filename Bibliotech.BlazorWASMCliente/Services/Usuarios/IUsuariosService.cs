using Bibliotech.Shared;
using Bibliotech.Shared.Usuarios;

namespace Bibliotech.BlazorWASMCliente.Services.Usuarios
{
    public interface IUsuariosService
    {
        Task<List<UsuarioDTO>> GetUsuarios();
        Task<UsuarioDTO> GetUsuarioById(int id);
        Task<ResponseApi<UsuarioDTO>> GetUsuarioByMail(string email);

        Task<int> Guardar(UsuarioDTO usuario);
        Task<int> Actualizar(UsuarioDTO usuario);
        Task<bool> Eliminar(int id);
    }
}
