using Bibliotech.Api.Models;
using Bibliotech.Shared.Role;
using Bibliotech.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech.Api.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
    private readonly BibliotecaDbContext _dbContext;

    public RolesController(BibliotecaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Buscar todos los libros
    [HttpGet]
    [Route("GetAllRoles")]
    public async Task<IActionResult> GetAll()
    {
        var responseApi = new ResponseApi<List<RoleDTO>>();

        try
        {
            // Obtiene la lista de libros y mapea directamente a DTO usando LINQ
            var rolesListDTO = await _dbContext.Roles
                .Select(item => new RoleDTO
                {
                    Id = item.Id,
                    Role1 = item.Role1,

                }).ToListAsync();

            // Establece la respuesta como exitosa
            responseApi.Success = true;
            responseApi.Value = rolesListDTO;
        }
        catch (Exception ex)
        {
            // Manejo de errores
            responseApi.Success = false;
            responseApi.Message = $"Error al obtener la lista de roles: {ex.Message}";
        }

        // Retorna la respuesta en formato JSON
        return Ok(responseApi);
    }


    //Buscar por titulo
    [HttpGet]
    [Route("GetRoleById/{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var ResponseApi = new ResponseApi<RoleDTO>();
        var RolesDTO = new RoleDTO();

        try
        {
            var dbRole = await _dbContext.Roles.FirstOrDefaultAsync(x => x.Id == id);

            if (dbRole != null)
            {
                RolesDTO.Id = dbRole.Id;
                RolesDTO.Role1 = dbRole.Role1;

                ResponseApi.Success = true;
                ResponseApi.Value = RolesDTO;
            }
            else
            {
                ResponseApi.Success = false;
                ResponseApi.Message = "rol no encontrado";
            }

        }
        catch (Exception ex)
        {
            ResponseApi.Success = false;
            ResponseApi.Message = ex.Message;
        }
        return Ok(ResponseApi);
    }

    //Guardar Libro

    [HttpPost]
    [Route("GuardarRole")]
    public async Task<IActionResult> Guardar(RoleDTO role)
    {
        var ResponseApi = new ResponseApi<int>();

        try
        {
            var dbRole = new Role
            {
                Role1 = role.Role1,
            };

            _dbContext.Add(dbRole);
            await _dbContext.SaveChangesAsync();

            if (dbRole.Id != 0)
            {
                ResponseApi.Success = true;
                ResponseApi.Value = dbRole.Id;
            }
            else
            {
                ResponseApi.Success = true;
                ResponseApi.Message = "No se ha guardado el rol";
            }


        }
        catch (Exception ex)
        {
            ResponseApi.Success = false;
            ResponseApi.Message = ex.Message;
        }
        return Ok(ResponseApi);
    }

    //ActualizarLibro
    [HttpPut]
    [Route("ActualizarRole/{id}")]
    public async Task<IActionResult> Actualizar(RoleDTO role, int id)
    {
        var ResponseApi = new ResponseApi<int>();

        try
        {
            var dbRole = await _dbContext.Roles.FirstOrDefaultAsync(e => e.Id == id);



            if (dbRole.Id != null)
            {
                dbRole.Role1 = role.Role1;

                _dbContext.Update(dbRole);
                await _dbContext.SaveChangesAsync();

                ResponseApi.Success = true;
                ResponseApi.Value = dbRole.Id;
            }
            else
            {
                ResponseApi.Success = true;
                ResponseApi.Message = "No se ha actualizado el rol";
            }


        }
        catch (Exception ex)
        {
            ResponseApi.Success = false;
            ResponseApi.Message = ex.Message;
        }
        return Ok(ResponseApi);
    }

    //Eliminar Libros
    [HttpDelete]
    [Route("EliminarRole/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var ResponseApi = new ResponseApi<int>();

        try
        {
            var dbRole = await _dbContext.Roles.FirstOrDefaultAsync(e => e.Id == id);



            if (dbRole.Id != null)
            {

                _dbContext.Remove(dbRole);
                await _dbContext.SaveChangesAsync();

                ResponseApi.Success = true;

            }
            else
            {
                ResponseApi.Success = true;
                ResponseApi.Message = "No se ha actualizado el rol";
            }


        }
        catch (Exception ex)
        {
            ResponseApi.Success = false;
            ResponseApi.Message = ex.Message;
        }
        return Ok(ResponseApi);
    }



}

