using Microsoft.AspNetCore.Mvc;
using SistemaAlumnosApi.DTOs;
using SistemaAlumnosApi.Mappers;
using SistemaAlumnosApi.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAlumnosApi.Controllers
{
    /// <summary>
    /// Controlador para la gestión de asignaciones.
    /// Expone operaciones CRUD mediante endpoints REST.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AsignacionesController : ControllerBase
    {
        private readonly IAsignacionRepository _repo;

        /// <summary>
        /// Constructor que inicializa el controlador con el repositorio de asignaciones.
        /// </summary>
        /// <param name="repo">Instancia del repositorio de asignaciones.</param>
        public AsignacionesController(IAsignacionRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Obtiene todas las asignaciones almacenadas en la base de datos.
        /// Devuelve solo información esencial sin datos innecesarios.
        /// </summary>
        /// <returns>Lista de asignaciones en formato JSON.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _repo.GetAllAsync();
            var dtos = lista.Select(AsignacionMapper.ToDTO);
            return Ok(dtos);
        }

        /// <summary>
        /// Obtiene una asignación específica por su identificador.
        /// Devuelve solo los datos esenciales en formato DTO.
        /// </summary>
        /// <param name="id">Identificador único de la asignación.</param>
        /// <returns>AsignacionDTO si existe, código HTTP 404 si no.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var asignacion = await _repo.GetByIdAsync(id);
            if (asignacion == null)
                return NotFound();

            return Ok(AsignacionMapper.ToDTO(asignacion));
        }

        /// <summary>
        /// Crea una nueva asignación en la base de datos.
        /// </summary>
        /// <param name="dto">Objeto AsignacionCreateDTO con los datos a insertar.</param>
        /// <returns>AsignacionDTO con su identificador asignado.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AsignacionCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entidad = AsignacionMapper.ToEntity(dto);
            var newId = await _repo.CreateAsync(entidad);
            var resultado = AsignacionMapper.ToDTO(entidad);
            resultado.AsignacionID = newId;

            return CreatedAtAction(nameof(GetById), new { id = newId }, resultado);
        }

        /// <summary>
        /// Actualiza los datos de una asignación existente en la base de datos.
        /// </summary>
        /// <param name="id">Identificador único de la asignación.</param>
        /// <param name="dto">Objeto AsignacionUpdateDTO con los datos actualizados.</param>
        /// <returns>Código HTTP 204 si la actualización fue exitosa, 404 si no se encuentra la asignación.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AsignacionUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.AsignacionID)
                return BadRequest("El ID en la URL no coincide con el ID en el cuerpo.");

            var entidad = AsignacionMapper.ToEntity(dto);
            var updated = await _repo.UpdateAsync(entidad);
            return updated ? NoContent() : NotFound();
        }

        /// <summary>
        /// Elimina una asignación de la base de datos.
        /// </summary>
        /// <param name="id">Identificador único de la asignación.</param>
        /// <returns>Código HTTP 204 si la eliminación fue exitosa, 404 si no se encuentra la asignación.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
