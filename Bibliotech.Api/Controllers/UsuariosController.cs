using Bibliotech.Api.Models;                  // Para el DbContext y modelos relacionados
using Bibliotech.Shared.Usuarios;            // Para UsuarioDTO y otras entidades compartidas
using Bibliotech.Shared;                     // Para ResponseApi u otras clases compartidas
using Microsoft.AspNetCore.Mvc;              // Para controladores y atributos como [ApiController], IActionResult
using Microsoft.EntityFrameworkCore;         // Para consultas asincrónicas sobre DbContext (e.g., ToListAsync, FirstOrDefaultAsync)
using Microsoft.AspNetCore.Identity;         // Si usas Identity para gestión de usuarios y roles
using System.Linq;                           // Para consultas LINQ como Select o Where
using System.Threading.Tasks;                // Para operaciones asincrónicas como Task<IActionResult>


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bibliotech.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{

    private readonly BibliotecaDbContext _dbContext;

    public UsuariosController(BibliotecaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: api/<UsuariosController>
    [HttpGet]
    [Route("GetAllUsers")]
    public async Task<IActionResult> GetAll()
    {
        var responseApi = new ResponseApi<List<UsuarioDTO>>();

        try
        {
            // Obtiene la lista de libros y mapea directamente a DTO usando LINQ
            var usuariosListDTO = await _dbContext.Usuarios
                .Select(item => new UsuarioDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    EMail = item.EMail,
                    RoleName = item.RoleName,
                    Password = item.Password,
                    RoleId = item.RoleId,
                })
                .ToListAsync();

            // Establece la respuesta como exitosa
            responseApi.Success = true;
            responseApi.Value = usuariosListDTO;
        }
        catch (Exception ex)
        {
            // Manejo de errores
            responseApi.Success = false;
            responseApi.Message = $"Error al obtener la lista de usuarios: {ex.Message}";
        }

        // Retorna la respuesta en formato JSON
        return Ok(responseApi);
    }

    // GET api/<UsuariosController>/5
    [HttpGet]
    [Route("GetUserById/{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var ResponseApi = new ResponseApi<UsuarioDTO>();
        var UsuariosDTO = new UsuarioDTO();

        try
        {
            var dbUsuario = await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

            if (dbUsuario != null)
            {
                UsuariosDTO.Id = dbUsuario.Id;
                UsuariosDTO.Name = dbUsuario.Name;
                UsuariosDTO.EMail = dbUsuario.EMail;
                UsuariosDTO.RoleName = dbUsuario.RoleName;
                UsuariosDTO.Password = dbUsuario.Password;
                UsuariosDTO.RoleId = dbUsuario.RoleId;

                ResponseApi.Success = true;
                ResponseApi.Value = UsuariosDTO;
            }
            else
            {
                ResponseApi.Success = false;
                ResponseApi.Message = "Usuario no encontrado";
            }

        }
        catch (Exception ex)
        {
            ResponseApi.Success = false;
            ResponseApi.Message = ex.Message;
        }
        return Ok(ResponseApi);
    }



    // GET api/<UsuariosController>/byemail/{email}
    [HttpGet]
    [Route("GetUserByMail/{email}")]
    public async Task<IActionResult> GetUserByMail(string email)
    {
        var responseApi = new ResponseApi<UsuarioDTO>();

        try
        {
            var dbUsuario = await _dbContext.Usuarios
                .Where(x => x.EMail == email)
                .Select(item => new UsuarioDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    EMail = item.EMail,
                    RoleName = item.RoleName,
                    Password = item.Password,
                    RoleId = item.RoleId,
                })
                .FirstOrDefaultAsync();

            if (dbUsuario != null)
            {
                responseApi.Success = true;
                responseApi.Value = dbUsuario;
            }
            else
            {
                responseApi.Success = false;
                responseApi.Message = "Usuario no encontrado";
            }
        }
        catch (Exception ex)
        {
            responseApi.Success = false;
            responseApi.Message = $"Error al obtener el usuario: {ex.Message}";
        }

        return Ok(responseApi);
    }


    [HttpPost]
    [Route("GuardarUsuario")]
    public async Task<IActionResult> Guardar(UsuarioDTO usuario)
    {
        var ResponseApi = new ResponseApi<int>();

        try
        {
            // Verificar si el correo electrónico ya está en uso
            var correoEnUso = await _dbContext.Usuarios.AnyAsync(u => u.EMail == usuario.EMail);

            if (correoEnUso)
            {
                ResponseApi.Success = false;
                ResponseApi.Message = "El correo electrónico ya está registrado.";
                return Ok(ResponseApi);
            }

            var dbUsuario = new Usuario
            {
                Name = usuario.Name,
                EMail = usuario.EMail,
                Password = usuario.Password,
                RoleName = usuario.RoleName,
                RoleId = usuario.RoleId,
            };

            _dbContext.Add(dbUsuario);
            await _dbContext.SaveChangesAsync();

            ResponseApi.Success = true;
            ResponseApi.Value = dbUsuario.Id;
        }
        catch (Exception ex)
        {
            ResponseApi.Success = false;
            ResponseApi.Message = ex.Message;
        }

        return Ok(ResponseApi);
    }


    // POST api/<UsuariosController>
    //[HttpPost]
    //[Route("GuardarUsuario")]
    //public async Task<IActionResult> Guardar(UsuarioDTO usuario)
    //{
    //    var ResponseApi = new ResponseApi<int>();

    //    try
    //    {
    //        // Verificar si el correo electrónico ya está en uso
    //        var existingUser = await _dbContext.Usuarios
    //            .FirstOrDefaultAsync(u => u.EMail == usuario.EMail);

    //        if (existingUser != null)
    //        {
    //            ResponseApi.Success = false;
    //            ResponseApi.Message = "El correo electrónico ya está registrado.";
    //            return Ok(ResponseApi);
    //        }

    //        var dbUsuario = new Usuario
    //        {
    //            Name = usuario.Name,
    //            EMail = usuario.EMail,
    //            Password = usuario.Password,
    //            RoleName = usuario.RoleName,
    //        };

    //        _dbContext.Add(dbUsuario);
    //        await _dbContext.SaveChangesAsync();

    //        if (dbUsuario.Id != 0)
    //        {
    //            ResponseApi.Success = true;
    //            ResponseApi.Value = dbUsuario.Id;
    //        }
    //        else
    //        {
    //            ResponseApi.Success = false;
    //            ResponseApi.Message = "No se ha guardado el usuario";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ResponseApi.Success = false;
    //        ResponseApi.Message = ex.Message;
    //    }

    //    return Ok(ResponseApi);
    //}

    [HttpPut]
    [Route("ActualizarUsuario/{id}")]
    public async Task<IActionResult> Actualizar(UsuarioDTO usuario, int id)
    {
        var ResponseApi = new ResponseApi<int>();

        try
        {
            // Obtener el usuario actual en la base de datos
            var dbUsuario = await _dbContext.Usuarios.FirstOrDefaultAsync(e => e.Id == id);

            if (dbUsuario == null)
            {
                ResponseApi.Success = false;
                ResponseApi.Message = "Usuario no encontrado.";
                return Ok(ResponseApi);
            }

            // Verificar si el nuevo correo ya está en uso por otro usuario
            var correoEnUso = await _dbContext.Usuarios
                .AnyAsync(u => u.EMail == usuario.EMail && u.Id != id);

            if (correoEnUso)
            {
                ResponseApi.Success = false;
                ResponseApi.Message = "El correo electrónico ya está en uso por otro usuario.";
                return Ok(ResponseApi);
            }

            // Actualizar los datos del usuario
            dbUsuario.Name = usuario.Name;
            dbUsuario.EMail = usuario.EMail;
            dbUsuario.RoleId = usuario.RoleId;
            dbUsuario.RoleName = usuario.RoleName;
            dbUsuario.Password = usuario.Password;

            _dbContext.Update(dbUsuario);
            await _dbContext.SaveChangesAsync();

            ResponseApi.Success = true;
            ResponseApi.Value = dbUsuario.Id;
        }
        catch (Exception ex)
        {
            ResponseApi.Success = false;
            ResponseApi.Message = ex.Message;
        }

        return Ok(ResponseApi);
    }


    [HttpDelete]
    [Route("EliminarUsuario/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var ResponseApi = new ResponseApi<int>();

        try
        {
            var dbUsuario = await _dbContext.Usuarios.FirstOrDefaultAsync(e => e.Id == id);

            if (dbUsuario == null)
            {
                ResponseApi.Success = false; // Mejor indicar fallo si el usuario no se encuentra.
                ResponseApi.Message = "Usuario no encontrado.";
                return Ok(ResponseApi);
            }

            // Eliminar dependencias relacionadas: reservas
            var reservas = await _dbContext.Reservas
                .Where(r => r.UserId == id).ToListAsync();
            _dbContext.Reservas.RemoveRange(reservas);

            // Eliminar dependencias relacionadas: notificaciones
            var notificaciones = await _dbContext.Notificaciones
                .Where(n => n.UserId == id).ToListAsync();
            _dbContext.Notificaciones.RemoveRange(notificaciones);

            // Guardar cambios tras eliminar dependencias
            await _dbContext.SaveChangesAsync();

            // Eliminar el usuario
            _dbContext.Remove(dbUsuario);
            await _dbContext.SaveChangesAsync();

            ResponseApi.Success = true;
            ResponseApi.Message = "Usuario eliminado correctamente.";
        }
        catch (Exception ex)
        {
            ResponseApi.Success = false;
            ResponseApi.Message = ex.Message;
        }

        return Ok(ResponseApi);
    }



    // PUT api/<UsuariosController>/5
    //[HttpPut]
    //[Route("ActualizarUsuario/{id}")]
    //public async Task<IActionResult> Actualizar(UsuarioDTO usuario, int id)
    //{
    //    var ResponseApi = new ResponseApi<int>();

    //    try
    //    {
    //        var dbUsuario = await _dbContext.Usuarios.FirstOrDefaultAsync(e => e.Id == id);



    //        if (dbUsuario.Id != null)
    //        {
    //            dbUsuario.Name = usuario.Name;
    //            dbUsuario.EMail = usuario.EMail;
    //            dbUsuario.RoleId = usuario.RoleId;
    //            dbUsuario.RoleName = usuario.RoleName;
    //            dbUsuario.Password = usuario.Password;

    //            _dbContext.Update(dbUsuario);
    //            await _dbContext.SaveChangesAsync();

    //            ResponseApi.Success = true;
    //            ResponseApi.Value = dbUsuario.Id;
    //        }
    //        else
    //        {
    //            ResponseApi.Success = true;
    //            ResponseApi.Message = "No se ha actualizado el usuario";
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        ResponseApi.Success = false;
    //        ResponseApi.Message = ex.Message;
    //    }
    //    return Ok(ResponseApi);
    //}

    // DELETE api/<UsuariosController>/5
    //[HttpDelete]
    //[Route("EliminarUsuario/{id}")]
    //public async Task<IActionResult> Eliminar(int id)
    //{
    //    var ResponseApi = new ResponseApi<int>();

    //    try
    //    {
    //        var dbUsuario = await _dbContext.Usuarios.FirstOrDefaultAsync(e => e.Id == id);




    //        if (dbUsuario.Id != null)
    //        {
    //            var reservas = await _dbContext.Reservas
    //                .Where(r => r.UserId == id).ToListAsync();
    //            _dbContext.Reservas.RemoveRange(reservas);
    //            await _dbContext.SaveChangesAsync();

    //            var notificaciones = await _dbContext.Notificaciones
    //                .Where(n => n.UserId == id).ToListAsync();

    //            _dbContext.Notificaciones.RemoveRange(notificaciones);
    //            await _dbContext.SaveChangesAsync();

    //            _dbContext.Remove(dbUsuario);
    //            await _dbContext.SaveChangesAsync();

    //            ResponseApi.Success = true;

    //        }
    //        else
    //        {
    //            ResponseApi.Success = true;
    //            ResponseApi.Message = "No se ha actualizado el usuario";
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        ResponseApi.Success = false;
    //        ResponseApi.Message = ex.Message;
    //    }
    //    return Ok(ResponseApi);
    //}


    [HttpPost("login")]
    public IActionResult Login([FromBody] UsuarioDTO request)
    {
        if (IsValidUser(request.EMail, request.Password))
        {
            var token = GenerateJwtToken(request.EMail); // O request.Name si es más relevante.
            return Ok(new { Token = token });
        }
        return Unauthorized("Invalid credentials");
    }

    private bool IsValidUser(string email, string password)
    {
        // Implementa la lógica para validar un usuario real.
        return _dbContext.Usuarios.Any(u => u.EMail == email && u.Password == password);
    }

    private string GenerateJwtToken(string email)
    {
        // Implementación de generación de token JWT.
        return "jwt_token_example"; // Debes sustituirlo por la lógica real.
    }


}

