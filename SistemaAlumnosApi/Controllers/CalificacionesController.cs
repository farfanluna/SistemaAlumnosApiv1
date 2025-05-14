using Microsoft.AspNetCore.Mvc;
using SistemaAlumnosApi.DTOs;
using SistemaAlumnosApi.Mappers;
using SistemaAlumnosApi.Models;
using SistemaAlumnosApi.Repositories.Interfaces;
using System.Threading.Tasks;

namespace SistemaAlumnosApi.Controllers
{
    /// <summary>
    /// Controlador para la gestión de calificaciones.
    /// Expone operaciones CRUD mediante endpoints REST.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionesController : ControllerBase
    {
        private readonly ICalificacionRepository _repo;

        /// <summary>
        /// Constructor que inicializa el controlador con el repositorio de calificaciones.
        /// </summary>
        /// <param name="repo">Instancia del repositorio de calificaciones.</param>
        public CalificacionesController(ICalificacionRepository repo)
            => _repo = repo;

        /// <summary>
        /// Obtiene todas las calificaciones almacenadas en la base de datos.
        /// Devuelve solo información esencial en formato DTO.
        /// </summary>
        /// <returns>Lista de calificaciones en formato JSON.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _repo.GetAllAsync();
            return Ok(lista);
        }

        /// <summary>
        /// Obtiene una calificación específica por su identificador.
        /// Devuelve solo los datos esenciales en formato DTO.
        /// </summary>
        /// <param name="id">Identificador único de la calificación.</param>
        /// <returns>La calificación encontrada en formato DTO o código HTTP 404 si no existe.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var calificacion = await _repo.GetByIdAsync(id);
            return calificacion is null ? NotFound() : Ok(calificacion);
        }

        /// <summary>
        /// Crea una nueva calificación en la base de datos.
        /// </summary>
        /// <param name="dto">Objeto CalificacionCreateDTO con los datos a insertar.</param>
        /// <returns>La calificación creada con su identificador asignado.</returns>
        [HttpPost]
   
        public async Task<IActionResult> Create([FromBody] CalificacionCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var entidad = CalificacionMapper.ToEntity(dto);
                var id = await _repo.CreateAsync(entidad);
                var resultado = CalificacionMapper.ToDTO(entidad);
                resultado.CalificacionID = id;

                return CreatedAtAction(nameof(GetById), new { id }, resultado);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { Message = ex.Message }); // 🔹 Devuelve un código 409 en lugar de 500
            }
        }

        /// <summary>
        /// Actualiza los datos de una calificación existente en la base de datos.
        /// </summary>
        /// <param name="id">Identificador único de la calificación.</param>
        /// <param name="dto">Objeto Calificacion con los datos actualizados.</param>
        /// <returns>Código HTTP 204 si la actualización fue exitosa, 404 si no se encuentra la calificación.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CalificacionUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.CalificacionID)
                return BadRequest("El ID en la URL no coincide con el ID en el payload.");

            var entidad = CalificacionMapper.ToEntity(dto);
            var updated = await _repo.UpdateAsync(entidad);

            return updated ? NoContent() : NotFound();
        }


        /// <summary>
        /// Elimina una calificación de la base de datos.
        /// </summary>
        /// <param name="id">Identificador único de la calificación.</param>
        /// <returns>Código HTTP 204 si la eliminación fue exitosa, 404 si no se encuentra la calificación.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
