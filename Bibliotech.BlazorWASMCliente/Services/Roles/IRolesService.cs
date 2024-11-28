using Bibliotech.Shared.Role;

namespace Bibliotech.BlazorWASMCliente.Services.Roles
{
    public interface IRolesService
    {
        Task<List<RoleDTO>> GetRoles();
        Task<RoleDTO> GetRolesById(int id);
        Task<int> Guardar(RoleDTO role);
        Task<int> Actualizar(RoleDTO role);
        Task<bool> Eliminar(int id);
    }
}
