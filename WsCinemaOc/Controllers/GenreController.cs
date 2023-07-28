using Microsoft.AspNetCore.Mvc;
using WsCinemaOc.Interfaces;
using WsCinemaOc.Models;


namespace WsCinemaOc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IRepository<Genero> _GenreRepository;

        public GenreController(IRepository<Genero> genreRepository)
        {
            _GenreRepository = genreRepository;
        }

        /// <summary>
        /// Obtiene todo los generos.
        /// </summary>
        /// <returns>Una lista de genero.</returns>
        [HttpGet]
        [Route("GetAllGenre")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Genero>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var genre = await _GenreRepository.GetAllAsync();
            return Ok(genre);
        }

        /// <summary>
        /// Agrega un nuevo genero.
        /// </summary>
        /// <param name="movie">Los datos de el genero a agregar.</param>
        /// <returns>El genero agregado.</returns>
        [HttpPost]
        [Route("AddGenre")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Genero))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddGenre([FromBody] Genero genre)
        {
            try
            {
                var gen = await _GenreRepository.GetAllAsync();
                if (gen.Any(g => g.Nombre == genre.Nombre))
                {
                    return BadRequest();
                }

                await _GenreRepository.AddAsync(genre);
                return NoContent();
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error al crear el Genero: {ex.Message}");
            }

        }
        /// <summary>
        /// Borra un genero existente.
        /// </summary>
        /// <param name="id">El ID del genero a borrar.</param>
        /// <param name="genero">Los datos de el genero.</param>
        /// <returns>Respuesta sin contenido (204) si se actualiza correctamente.</returns>
        [HttpDelete("DeleteGenero/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var genero = await _GenreRepository.GetByIdAsync(id);
                if (genero == null)
                {
                    return NotFound();
                }

                await _GenreRepository.DeleteAsync(genero);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la película: {ex.Message}");
            }
        }
    }

}
