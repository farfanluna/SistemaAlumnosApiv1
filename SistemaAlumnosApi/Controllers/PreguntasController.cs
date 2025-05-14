using Microsoft.AspNetCore.Mvc;
using SistemaAlumnosApi.Models;
using SistemaAlumnosApi.Repositories.Interfaces;
using System.Threading.Tasks;

namespace SistemaAlumnosApi.Controllers
{
    /// <summary>
    /// Controlador para gestionar las operaciones CRUD de las preguntas.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntasController : ControllerBase
    {
        private readonly IPreguntaRepository _repo;

        /// <summary>
        /// Constructor que inicializa el controlador con el repositorio de preguntas.
        /// </summary>
        /// <param name="repo">Instancia del repositorio de preguntas.</param>
        public PreguntasController(IPreguntaRepository repo)
            => _repo = repo;

        /// <summary>
        /// Obtiene todas las preguntas almacenadas en la base de datos.
        /// </summary>
        /// <returns>Lista de preguntas en formato JSON.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _repo.GetAllAsync());

        /// <summary>
        /// Obtiene una pregunta específica por su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la pregunta.</param>
        /// <returns>La pregunta encontrada o un código HTTP 404 si no existe.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            return p is null ? NotFound() : Ok(p);
        }

        /// <summary>
        /// Crea una nueva pregunta en la base de datos.
        /// </summary>
        /// <param name="dto">Objeto Pregunta con los datos a insertar.</param>
        /// <returns>La pregunta creada con su identificador asignado.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Pregunta dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var id = await _repo.CreateAsync(dto);
            dto.PreguntaID = id;

            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }

        /// <summary>
        /// Actualiza los datos de una pregunta existente en la base de datos.
        /// </summary>
        /// <param name="id">Identificador único de la pregunta.</param>
        /// <param name="dto">Objeto Pregunta con los datos actualizados.</param>
        /// <returns>Código HTTP 204 si la actualización fue exitosa, 404 si no se encuentra la pregunta.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Pregunta dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != dto.PreguntaID) return BadRequest("ID mismatch");

            var ok = await _repo.UpdateAsync(dto);
            return ok ? NoContent() : NotFound();
        }

        /// <summary>
        /// Elimina una pregunta de la base de datos.
        /// </summary>
        /// <param name="id">Identificador único de la pregunta.</param>
        /// <returns>Código HTTP 204 si la eliminación fue exitosa, 404 si no se encuentra la pregunta.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => await _repo.DeleteAsync(id) ? NoContent() : NotFound();
    }
}
