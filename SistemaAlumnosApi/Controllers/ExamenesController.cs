using Microsoft.AspNetCore.Mvc;
using SistemaAlumnosApi.DTOs;
using SistemaAlumnosApi.Mappers;
using SistemaAlumnosApi.Repositories.Interfaces;
using System.Threading.Tasks;

namespace SistemaAlumnosApi.Controllers
{
    /// <summary>
    /// Controlador para la gestión de exámenes.
    /// Expone operaciones CRUD mediante endpoints REST.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ExamenesController : ControllerBase
    {
        private readonly IExamenRepository _repo;

        /// <summary>
        /// Constructor que inicializa el controlador con el repositorio de exámenes.
        /// </summary>
        /// <param name="repo">Instancia del repositorio de exámenes.</param>
        public ExamenesController(IExamenRepository repo)
            => _repo = repo;

        /// <summary>
        /// Obtiene todos los exámenes almacenados en la base de datos.
        /// Devuelve solo los datos esenciales en formato DTO.
        /// </summary>
        /// <returns>Lista de exámenes en formato JSON.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _repo.GetAllAsync();
            return Ok(lista);
        }

        /// <summary>
        /// Obtiene un examen específico por su identificador.
        /// Devuelve solo los datos esenciales en formato DTO.
        /// </summary>
        /// <param name="id">Identificador único del examen.</param>
        /// <returns>El examen encontrado en formato DTO o un código HTTP 404 si no existe.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var examen = await _repo.GetByIdAsync(id);
            return examen is null ? NotFound() : Ok(examen);
        }

        /// <summary>
        /// Crea un nuevo examen en la base de datos.
        /// </summary>
        /// <param name="dto">Objeto ExamenCreateDTO con los datos a insertar.</param>
        /// <returns>El examen creado con su identificador asignado.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExamenCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _repo.CreateAsync(dto);
            var resultado = new ExamenDTO
            {
                ExamenID = id,
                Titulo = dto.Titulo,
                MateriaID = dto.MateriaID,
                FechaAplicacion = dto.FechaAplicacion
            };

            return CreatedAtAction(nameof(GetById), new { id }, resultado);
        }

        /// <summary>
        /// Actualiza los datos de un examen existente en la base de datos.
        /// </summary>
        /// <param name="id">Identificador único del examen.</param>
        /// <param name="dto">Objeto ExamenUpdateDTO con los datos actualizados.</param>
        /// <returns>Código HTTP 204 si la actualización fue exitosa, 404 si no se encuentra el examen.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExamenUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.ExamenID)
                return BadRequest("El ID en la URL no coincide con el ID en el cuerpo.");

            var updated = await _repo.UpdateAsync(dto);
            return updated ? NoContent() : NotFound();
        }

        /// <summary>
        /// Elimina un examen de la base de datos.
        /// </summary>
        /// <param name="id">Identificador único del examen.</param>
        /// <returns>Código HTTP 204 si la eliminación fue exitosa, 404 si no se encuentra el examen.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
