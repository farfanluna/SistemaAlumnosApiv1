using Microsoft.AspNetCore.Mvc;
using SistemaAlumnosApi.DTOs;
using SistemaAlumnosApi.Mappers;
using SistemaAlumnosApi.Repositories.Interfaces;
using System.Threading.Tasks;

namespace SistemaAlumnosApi.Controllers
{
    /// <summary>
    /// Controlador para la gestión de materias.
    /// Expone operaciones CRUD mediante endpoints REST.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MateriasController : ControllerBase
    {
        private readonly IMateriaRepository _repo;

        /// <summary>
        /// Constructor que inicializa el controlador con el repositorio de materias.
        /// </summary>
        /// <param name="repo">Instancia del repositorio de materias.</param>
        public MateriasController(IMateriaRepository repo)
            => _repo = repo;

        /// <summary>
        /// Obtiene todas las materias almacenadas en la base de datos.
        /// Devuelve solo información esencial en formato DTO.
        /// </summary>
        /// <returns>Lista de materias en formato JSON.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _repo.GetAllAsync();
            return Ok(lista);
        }

        /// <summary>
        /// Obtiene una materia específica por su identificador.
        /// Devuelve solo los datos esenciales en formato DTO.
        /// </summary>
        /// <param name="id">Identificador único de la materia.</param>
        /// <returns>La materia encontrada en formato DTO o código HTTP 404 si no existe.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var materia = await _repo.GetByIdAsync(id);
            return materia is null ? NotFound() : Ok(materia);
        }

        /// <summary>
        /// Crea una nueva materia en la base de datos.
        /// </summary>
        /// <param name="dto">Objeto MateriaCreateDTO con los datos a insertar.</param>
        /// <returns>La materia creada con su identificador asignado.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MateriaCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _repo.CreateAsync(dto);
            var resultado = new MateriaDTO
            {
                MateriaID = id,
                Nombre = dto.Nombre,
                Creditos = dto.Creditos
            };

            return CreatedAtAction(nameof(GetById), new { id }, resultado);
        }

        /// <summary>
        /// Actualiza los datos de una materia existente en la base de datos.
        /// </summary>
        /// <param name="id">Identificador único de la materia.</param>
        /// <param name="dto">Objeto MateriaUpdateDTO con los datos actualizados.</param>
        /// <returns>Código HTTP 204 si la actualización fue exitosa, 404 si no se encuentra la materia.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MateriaUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.MateriaID)
                return BadRequest("El ID en la URL no coincide con el ID en el cuerpo.");

            var updated = await _repo.UpdateAsync(dto);
            return updated ? NoContent() : NotFound();
        }

        /// <summary>
        /// Elimina una materia de la base de datos.
        /// </summary>
        /// <param name="id">Identificador único de la materia.</param>
        /// <returns>Código HTTP 204 si la eliminación fue exitosa, 404 si no se encuentra la materia.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
