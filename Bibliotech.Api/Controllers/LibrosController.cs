using Bibliotech.Api.Models;
using Bibliotech.Shared;
using Bibliotech.Shared.Libros;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LibrosController : ControllerBase
{
    private readonly BibliotecaDbContext _dbContext;

    public LibrosController(BibliotecaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Buscar todos los libros
    [HttpGet]
    [Route("GetAllBooks")]
    public async Task<IActionResult> GetAll()
    {
        var responseApi = new ResponseApi<List<LibroDTO>>();

        try
        {
            // Obtiene la lista de libros y mapea directamente a DTO usando LINQ
            var booksListDTO = await _dbContext.Libros
                .Select(item => new LibroDTO
                {
                    Id = item.Id,
                    PictureLink = item.PictureLink,
                    Tittle = item.Tittle,
                    Autor = item.Autor,
                    Genero = item.Genero,
                    Isbn = item.Isbn,
                    PublicationDate = item.PublicationDate,
                    ExpireDate = item.ExpireDate,
                    Status = item.Status
                })
                .ToListAsync();

            // Establece la respuesta como exitosa
            responseApi.Success = true;
            responseApi.Value = booksListDTO;
        }
        catch (Exception ex)
        {
            // Manejo de errores
            responseApi.Success = false;
            responseApi.Message = $"Error al obtener la lista de libros: {ex.Message}";
        }

        // Retorna la respuesta en formato JSON
        return Ok(responseApi);
    }


    //Buscar por titulo
    [HttpGet]
    [Route("GetBookById/{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var ResponseApi = new ResponseApi<LibroDTO>();
        var BooksDTO = new LibroDTO();

        try
        {
            var dbLibro = await _dbContext.Libros.FirstOrDefaultAsync(x => x.Id == id);

            if (dbLibro != null) 
            {
                BooksDTO.PictureLink = dbLibro.PictureLink;
                BooksDTO.Id = dbLibro.Id;
                BooksDTO.Tittle = dbLibro.Tittle;
                BooksDTO.Autor = dbLibro.Autor;
                BooksDTO.Genero = dbLibro.Genero;
                BooksDTO.Isbn = dbLibro.Isbn;
                BooksDTO.PublicationDate = dbLibro.PublicationDate;
                BooksDTO.Disponibilidad = dbLibro.Disponibilidad;
                BooksDTO.ExpireDate = dbLibro.ExpireDate;
                BooksDTO.Status = dbLibro.Status;

                ResponseApi.Success = true;
                ResponseApi.Value = BooksDTO;
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
    [Route("GetBookByTittle/{tittle}")]
    public async Task<IActionResult> GetBookByTittle(string tittle)
    {
        var responseApi = new ResponseApi<List<LibroDTO>>();

        try
        {
            var dbLibros = await _dbContext.Libros
                .Where(x => x.Tittle.ToLower().Contains(tittle.ToLower()))
                .Select(libro => new LibroDTO
                {
                    Id = libro.Id,
                    PictureLink = libro.PictureLink,
                    Tittle = libro.Tittle,
                    Autor = libro.Autor,
                    Genero = libro.Genero,
                    Isbn = libro.Isbn,
                    PublicationDate = libro.PublicationDate,
                    Disponibilidad = libro.Disponibilidad,
                    ExpireDate = libro.ExpireDate,
                    Status = libro.Status
                })
                .ToListAsync();

            if (dbLibros.Any())
            {
                responseApi.Success = true;
                responseApi.Value = dbLibros;
            }
            else
            {
                responseApi.Success = false;
                responseApi.Message = "No se encontraron libros con ese título";
            }
        }
        catch (Exception ex)
        {
            responseApi.Success = false;
            responseApi.Message = $"Error al buscar libros: {ex.Message}";
        }

        return Ok(responseApi);
    }

    //Guardar Libro

    [HttpPost]
    [Route("GuardarLibro")]
    public async Task<IActionResult> Guardar(LibroDTO libro)
    {
        var ResponseApi = new ResponseApi<int>();

        try
        {
            var dbLibro = new Libro
            {
                Tittle = libro.Tittle,
                PictureLink = libro.PictureLink,
                Autor = libro.Autor,
                Genero = libro.Genero,
                Isbn = libro.Isbn,
                PublicationDate = libro.PublicationDate,
                ExpireDate = libro.ExpireDate,
                Status = libro.Status,
            };

            _dbContext.Add(dbLibro);
            await _dbContext.SaveChangesAsync();

            if (dbLibro.Id != 0)
            {
                ResponseApi.Success = true;
                ResponseApi.Value = dbLibro.Id;
            }
            else 
            {
                ResponseApi.Success = true;
                ResponseApi.Message = "No se ha guardado el libro";
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
    [Route("ActualizarLibro/{id}")]
    public async Task<IActionResult> Actualizar(LibroDTO libro, int id)
    {
        var ResponseApi = new ResponseApi<int>();

        try
        {
            var dbLibro = await _dbContext.Libros.FirstOrDefaultAsync( e => e.Id == id);



            if (dbLibro.Id != null)
            {
                dbLibro.PictureLink = libro.PictureLink;
                dbLibro.Tittle = libro.Tittle;
                dbLibro.Autor = libro.Autor;
                dbLibro.Genero = libro.Genero;
                dbLibro.Isbn = libro.Isbn;
                dbLibro.PublicationDate = libro.PublicationDate;
                dbLibro.Disponibilidad = libro.Disponibilidad;
                dbLibro.ExpireDate = libro.ExpireDate;
                dbLibro.Status = libro.Status;

                _dbContext.Update(dbLibro);
                await _dbContext.SaveChangesAsync();

                ResponseApi.Success = true;
                ResponseApi.Value = dbLibro.Id;
            }
            else
            {
                ResponseApi.Success = true;
                ResponseApi.Message = "No se ha actualizado el libro";
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
    [Route("EliminarLibro/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var ResponseApi = new ResponseApi<int>();

        try
        {
            var dbLibro = await _dbContext.Libros.FirstOrDefaultAsync(e => e.Id == id);



            if (dbLibro.Id != null)
            {

                _dbContext.Remove(dbLibro);
                await _dbContext.SaveChangesAsync();

                ResponseApi.Success = true;
  
            }
            else
            {
                ResponseApi.Success = true;
                ResponseApi.Message = "No se ha actualizado el libro";
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




