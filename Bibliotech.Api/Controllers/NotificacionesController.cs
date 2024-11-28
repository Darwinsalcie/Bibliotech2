using Bibliotech.Api.Models;
using Bibliotech.Shared;
using Bibliotech.Shared.Notificaciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificacionesController : ControllerBase
{
    private readonly BibliotecaDbContext _dbContext;

    public NotificacionesController(BibliotecaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Buscar todos los libros
    [HttpGet]
    [Route("GetAllNotificaciones")]
    public async Task<IActionResult> GetAll()
    {
        var responseApi = new ResponseApi<List<NotificacionDTO>>();

        try
        {
            // Obtiene la lista de libros y mapea directamente a DTO usando LINQ
            var notificacionListDTO = await _dbContext.Notificaciones
                .Select(item => new NotificacionDTO
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    Type = item.Type,
                    Message = item.Message,
                    FechaEnvio = item.FechaEnvio,
                })
                .ToListAsync();

            // Establece la respuesta como exitosa
            responseApi.Success = true;
            responseApi.Value = notificacionListDTO;
        }
        catch (Exception ex)
        {
            // Manejo de errores
            responseApi.Success = false;
            responseApi.Message = $"Error al obtener la lista de notificaciones: {ex.Message}";
        }

        // Retorna la respuesta en formato JSON
        return Ok(responseApi);
    }


    //Buscar por titulo
    [HttpGet]

    [Route("GetNotificacionesById/{id}")]
    public async Task<IActionResult> GetNotifiacionesById(int id)
    {
        var ResponseApi = new ResponseApi<NotificacionDTO>();
        var NotificacionDTO = new NotificacionDTO();

        try
        {
            var dbNotificacion = await _dbContext.Notificaciones.FirstOrDefaultAsync(x => x.Id == id);

            if (dbNotificacion != null)
            {
                NotificacionDTO.Id = dbNotificacion.Id;
                NotificacionDTO.UserId = dbNotificacion.UserId;
                NotificacionDTO.Type = dbNotificacion.Type;
                NotificacionDTO.Message = dbNotificacion.Message;
                NotificacionDTO.FechaEnvio = dbNotificacion.FechaEnvio;

                ResponseApi.Success = true;
                ResponseApi.Value = NotificacionDTO;
            }
            else
            {
                ResponseApi.Success = false;
                ResponseApi.Message = "Notificación no encontrada";
            }

        }
        catch (Exception ex)
        {
            ResponseApi.Success = false;
            ResponseApi.Message = ex.Message;
        }
        return Ok(ResponseApi);
    }

    [HttpGet]
    [Route("GetNotificacionesByUserId/{userId}")]
    public async Task<IActionResult> GetNotificacionesByUserId(int userId)
    {
        var responseApi = new ResponseApi<List<NotificacionDTO>>();

        try
        {
            // Filtra las notificaciones por UserId
            var notificacionListDTO = await _dbContext.Notificaciones
                .Where(n => n.UserId == userId)
                .Select(n => new NotificacionDTO
                {
                    Id = n.Id,
                    UserId = n.UserId,
                    Type = n.Type,
                    Message = n.Message,
                    FechaEnvio = n.FechaEnvio
                })
                .ToListAsync();

            if (notificacionListDTO.Any())
            {
                responseApi.Success = true;
                responseApi.Value = notificacionListDTO;
            }
            else
            {
                responseApi.Success = false;
                responseApi.Message = "No se encontraron notificaciones para este usuario.";
            }
        }
        catch (Exception ex)
        {
            responseApi.Success = false;
            responseApi.Message = $"Error al obtener las notificaciones: {ex.Message}";
        }

        return Ok(responseApi);
    }

    //Guardar Libro

    [HttpPost]
    [Route("GuardarNotificacion")]
    public async Task<IActionResult> Guardar(NotificacionDTO notificacion)
    {
        var ResponseApi = new ResponseApi<int>();

        try
        {
            var dbNotificacion = new Notificacion
            {
                
                UserId = notificacion.UserId,
                Type = notificacion.Type,
                Message = notificacion.Message,
                FechaEnvio = notificacion.FechaEnvio,


            };

            _dbContext.Add(dbNotificacion);
            await _dbContext.SaveChangesAsync();

            if (dbNotificacion.Id != 0)
            {
                ResponseApi.Success = true;
                ResponseApi.Value = dbNotificacion.Id;
            }
            else
            {
                ResponseApi.Success = true;
                ResponseApi.Message = "No se ha guardado la notificacion";
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
    [Route("ActualizarNotificacion/{id}")]
    public async Task<IActionResult> Actualizar(NotificacionDTO notificacion, int id)
    {
        var ResponseApi = new ResponseApi<int>();

        try
        {
            var dbNotificacion = await _dbContext.Notificaciones.FirstOrDefaultAsync(e => e.Id == id);



            if (dbNotificacion.Id != null)
            {
                dbNotificacion.UserId = notificacion.UserId;
                dbNotificacion.Type = notificacion.Type;
                dbNotificacion.Message = notificacion.Message;
                dbNotificacion.FechaEnvio = notificacion.FechaEnvio;

                _dbContext.Update(dbNotificacion);
                await _dbContext.SaveChangesAsync();

                ResponseApi.Success = true;
                ResponseApi.Value = dbNotificacion.Id;
            }
            else
            {
                ResponseApi.Success = true;
                ResponseApi.Message = "No se ha actualizado la notificacion";
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
    [Route("EliminarNotificacion/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var ResponseApi = new ResponseApi<int>();

        try
        {
            var dbNotificacion = await _dbContext.Notificaciones.FirstOrDefaultAsync(e => e.Id == id);



            if (dbNotificacion.Id != null)
            {

                _dbContext.Remove(dbNotificacion);
                await _dbContext.SaveChangesAsync();

                ResponseApi.Success = true;

            }
            else
            {
                ResponseApi.Success = true;
                ResponseApi.Message = "No se ha actualizado la notificacion";
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






