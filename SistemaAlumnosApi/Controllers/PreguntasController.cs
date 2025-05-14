using Microsoft.AspNetCore.Mvc;
using SistemaAlumnosApi.DTOs;
using SistemaAlumnosApi.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace SistemaAlumnosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PreguntaController : ControllerBase
    {
        private readonly IPreguntaRepository _repo;

        /// <summary>
        /// Constructor que inyecta el repositorio de preguntas.
        /// </summary>
        /// <param name="repo">Repositorio que implementa IPreguntaRepository</param>
        public PreguntaController(IPreguntaRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Obtiene todas las preguntas.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var preguntas = await _repo.GetAllAsync();
            return Ok(preguntas);
        }

        /// <summary>
        /// Obtiene una pregunta por su ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pregunta = await _repo.GetByIdAsync(id);
            if (pregunta is null)
                return NotFound($"No se encontró la pregunta con ID {id}");

            return Ok(pregunta);
        }

        /// <summary>
        /// Crea una nueva pregunta.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PreguntaCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var id = await _repo.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id }, new { id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la pregunta: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza una pregunta existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PreguntaUpdateDTO dto)
        {
            if (id != dto.PreguntaID)
                return BadRequest("El ID del parámetro no coincide con el ID del cuerpo.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exists = await _repo.GetByIdAsync(id);
            if (exists is null)
                return NotFound($"No se encontró la pregunta con ID {id}");

            try
            {
                var result = await _repo.UpdateAsync(dto);
                if (!result)
                    return StatusCode(500, "Error al actualizar la pregunta.");
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar la pregunta: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina una pregunta por su ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _repo.GetByIdAsync(id);
            if (exists is null)
                return NotFound($"No se encontró la pregunta con ID {id}");

            try
            {
                var result = await _repo.DeleteAsync(id);
                if (!result)
                    return StatusCode(500, "Error al eliminar la pregunta.");
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la pregunta: {ex.Message}");
            }
        }
    }
}
