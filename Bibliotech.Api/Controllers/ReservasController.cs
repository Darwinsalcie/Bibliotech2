using Bibliotech.Api.Models;
using Bibliotech.Shared;
using Microsoft.AspNetCore.Mvc;
using Bibliotech.Shared.Reserva;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservasController : ControllerBase
{
    private readonly BibliotecaDbContext _dbContext;

    public ReservasController(BibliotecaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Buscar todos los libros
    [HttpGet]
    [Route("GetAllReservas")]
    public async Task<IActionResult> GetAll()
    {
        var responseApi = new ResponseApi<List<ReservaDTO>>();

        try
        {
            // Obtiene la lista de libros y mapea directamente a DTO usando LINQ
            var reservasListDTO = await _dbContext.Reservas
                .Select(item => new ReservaDTO
                {
                    Id = item.Id,
                    BookName = item.BookName,
                    UserId = item.UserId,
                    BookId = item.BookId,
                    ReservationDate = item.ReservationDate,
                    Status = item.Status,

                }).ToListAsync();


            // Establece la respuesta como exitosa
            responseApi.Success = true;
            responseApi.Value = reservasListDTO;
        }
        catch (Exception ex)
        {
            // Manejo de errores
            responseApi.Success = false;
            responseApi.Message = $"Error al obtener la lista de Reservas: {ex.Message}";
        }

        // Retorna la respuesta en formato JSON
        return Ok(responseApi);
    }


    //Buscar por titulo
    [HttpGet]
    [Route("GetReservaById/{id}")]
    public async Task<IActionResult> GetReservaById(int id)
    {
        var ResponseApi = new ResponseApi<ReservaDTO>();
        var ReservasDTO = new ReservaDTO();

        try
        {
            var dbReserva = await _dbContext.Reservas.FirstOrDefaultAsync(x => x.Id == id);

            if (dbReserva != null)
            {
                ReservasDTO.Id = dbReserva.Id;
                ReservasDTO.BookName = dbReserva.BookName;
                ReservasDTO.UserId = dbReserva.UserId;
                ReservasDTO.BookId = dbReserva.BookId;
                ReservasDTO.ReservationDate = dbReserva.ReservationDate;
                ReservasDTO.Status = dbReserva.Status;

                ResponseApi.Success = true;
                ResponseApi.Value = ReservasDTO;
            }
            else
            {
                ResponseApi.Success = false;
                ResponseApi.Message = "Libro no encontrado";
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
    [Route("GetReservasByUserId/{userId}")]
    public async Task<IActionResult> GetReservasByUserId(int userId)
    {
        var responseApi = new ResponseApi<List<ReservaDTO>>();

        try
        {
            // Filtra las notificaciones por UserId
            var reservasListDTO = await _dbContext.Reservas
                .Where(n => n.UserId == userId)
                .Select(n => new ReservaDTO
                {
                    Id = n.Id,
                    BookName = n.BookName,
                    UserId = n.UserId,
                    BookId = n.BookId,
                    ReservationDate = n.ReservationDate,
                    Status = n.Status,
                })
                .ToListAsync();

            if (reservasListDTO.Any())
            {
                responseApi.Success = true;
                responseApi.Value = reservasListDTO;
            }
            else
            {
                responseApi.Success = false;
                responseApi.Message = "No se encontraron reservas para este usuario.";
            }
        }
        catch (Exception ex)
        {
            responseApi.Success = false;
            responseApi.Message = $"Error al obtener las reservas: {ex.Message}";
        }

        return Ok(responseApi);
    }
    //Guardar Libro

    [HttpPost]
    [Route("GuardarReserva")]
    public async Task<IActionResult> Guardar(ReservaDTO reserva)
    {
        var ResponseApi = new ResponseApi<int>();

        try
        {
            var dbReserva = new Reserva
            {
                BookName = reserva.BookName,
                UserId = reserva.UserId,
                BookId = reserva.BookId,
                ReservationDate = reserva.ReservationDate,
            };

            _dbContext.Add(dbReserva);
            await _dbContext.SaveChangesAsync();

            if (dbReserva.Id != 0)
            {
                ResponseApi.Success = true;
                ResponseApi.Value = dbReserva.Id;
            }
            else
            {
                ResponseApi.Success = true;
                ResponseApi.Message = "No se ha guardado la reserva";
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
    [Route("ActualizarReserva/{id}")]
    public async Task<IActionResult> Actualizar(ReservaDTO reserva, int id)
    {
        var ResponseApi = new ResponseApi<int>();

        try
        {
            var dbReserva = await _dbContext.Reservas.FirstOrDefaultAsync(e => e.Id == id);



            if (dbReserva.Id != null)
            {
                dbReserva.UserId = reserva.UserId;
                dbReserva.BookName = reserva.BookName;
                dbReserva.BookId = reserva.BookId;
                dbReserva.ReservationDate = reserva.ReservationDate;

                _dbContext.Update(dbReserva);
                await _dbContext.SaveChangesAsync();

                ResponseApi.Success = true;
                ResponseApi.Value = dbReserva.Id;
            }
            else
            {
                ResponseApi.Success = true;
                ResponseApi.Message = "No se ha actualizado la reserva";
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
    [Route("EliminarReserva/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var ResponseApi = new ResponseApi<int>();

        try
        {
            var dbReserva = await _dbContext.Reservas.FirstOrDefaultAsync(e => e.Id == id);


            if (dbReserva.Id != null)
            {

                _dbContext.Remove(dbReserva);
                await _dbContext.SaveChangesAsync();

                ResponseApi.Success = true;

            }
            else
            {
                ResponseApi.Success = true;
                ResponseApi.Message = "No se ha actualizado la reserva";
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

