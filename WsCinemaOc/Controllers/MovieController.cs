using Microsoft.AspNetCore.Mvc;
using WsCinemaOc.Interfaces;
using WsCinemaOc.Models;

namespace WsCinemaOc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IRepository<Pelicula> _movieRepository;

        public MovieController(IRepository<Pelicula> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        /// <summary>
        /// Obtiene todas las peliculas.
        /// </summary>
        /// <returns>Una lista de peliculas.</returns>
        [HttpGet]
        [Route("GetAllMovies")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Pelicula>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _movieRepository.GetAllMoviesAsync();
            return Ok(movies);
        }

        /// <summary>
        /// Agrega una nueva película.
        /// </summary>
        /// <param name="movie">Los datos de la película a agregar.</param>
        /// <returns>La película agregada.</returns>
        [HttpPost]
        [Route("AddMovie")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pelicula))]
        public async Task<IActionResult> AddMovie([FromBody] Pelicula movie)
         {// Verificamos si ImagenPortadaBase64 tiene algún valor
            if (!string.IsNullOrEmpty(movie.ImagenPortadaBase64))
            {
                try
                {
                    // Convertir la imagen base64 a un arreglo de bytes
                    movie.ImagenPortada = Convert.FromBase64String(movie.ImagenPortadaBase64);
                }
                catch (FormatException)
                {
                    return BadRequest("La imagen de portada no está en formato base64 válido.");
                }
            }

            await _movieRepository.AddAsync(movie);
            return Ok(movie);
        }

        /// <summary>
        /// Actualiza una película existente.
        /// </summary>
        /// <param name="id">El ID de la película a actualizar.</param>
        /// <param name="movie">Los datos actualizados de la película.</param>
        /// <returns>Respuesta sin contenido (204) si se actualiza correctamente.</returns>
        [HttpPost("UpdateMovie/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pelicula))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMovie(int id, Pelicula movie)
        {
            if (id != movie.Id)
                return BadRequest();

            await _movieRepository.UpdateAsync(movie);
            return Ok();
        }

        /// <summary>
        /// Borra una película existente.
        /// </summary>
        /// <param name="id">El ID de la película a borrar.</param>
        /// <param name="movie">Los datos a borrar de la película.</param>
        /// <returns>Respuesta sin contenido (204) si se actualiza correctamente.</returns>

        [HttpDelete("DeleteMovie/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var pelicula = await _movieRepository.GetByIdAsync(id);
                 if (pelicula == null)
                {
                    return NotFound(); 
                }

                await _movieRepository.DeleteAsync(pelicula);

                return NoContent(); 
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Error al eliminar la película: {ex.Message}");
            }
        }
    }

}

