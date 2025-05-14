using Microsoft.AspNetCore.Mvc;
using SistemaAlumnosApi.Models;
using SistemaAlumnosApi.Repositories.Interfaces;
using System.Threading.Tasks;

namespace SistemaAlumnosApi.Controllers
{
    /// <summary>
    /// Controlador para gestionar las operaciones CRUD de las respuestas.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RespuestasController : ControllerBase
    {
        private readonly IRespuestaRepository _repo;

        /// <summary>
        /// Constructor que inicializa el controlador con el repositorio de respuestas.
        /// </summary>
        /// <param name="repo">Instancia del repositorio de respuestas.</param>
        public RespuestasController(IRespuestaRepository repo)
            => _repo = repo;

        /// <summary>
        /// Obtiene todas las respuestas almacenadas en la base de datos.
        /// </summary>
        /// <returns>Lista de respuestas en formato JSON.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _repo.GetAllAsync());

        /// <summary>
        /// Obtiene una respuesta específica por su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la respuesta.</param>
        /// <returns>La respuesta encontrada o un código HTTP 404 si no existe.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var r = await _repo.GetByIdAsync(id);
            return r is null ? NotFound() : Ok(r);
        }

        /// <summary>
        /// Crea una nueva respuesta en la base de datos.
        /// </summary>
        /// <param name="dto">Objeto Respuesta con los datos a insertar.</param>
        /// <returns>La respuesta creada con su identificador asignado.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Respuesta dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var id = await _repo.CreateAsync(dto);
            dto.RespuestaID = id;

            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }

        /// <summary>
        /// Actualiza los datos de una respuesta existente en la base de datos.
        /// </summary>
        /// <param name="id">Identificador único de la respuesta.</param>
        /// <param name="dto">Objeto Respuesta con los datos actualizados.</param>
        /// <returns>Código HTTP 204 si la actualización fue exitosa, 404 si no se encuentra la respuesta.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Respuesta dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != dto.RespuestaID) return BadRequest("ID mismatch");

            var ok = await _repo.UpdateAsync(dto);
            return ok ? NoContent() : NotFound();
        }

        /// <summary>
        /// Elimina una respuesta de la base de datos.
        /// </summary>
        /// <param name="id">Identificador único de la respuesta.</param>
        /// <returns>Código HTTP 204 si la eliminación fue exitosa, 404 si no se encuentra la respuesta.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => await _repo.DeleteAsync(id) ? NoContent() : NotFound();
    }
}
