using Microsoft.AspNetCore.Mvc;
using SistemaAlumnosApi.Mappers;
using SistemaAlumnosApi.Models;
using SistemaAlumnosApi.Repositories.Interfaces;
using System.Threading.Tasks;

namespace SistemaAlumnosApi.Controllers
{
    /// <summary>
    /// Controlador para la gestión de alumnos.
    /// Expone operaciones CRUD mediante endpoints REST.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        private readonly IAlumnoRepository _repo;

        /// <summary>
        /// Constructor que inicializa el controlador con el repositorio de alumnos.
        /// </summary>
        /// <param name="repo">Instancia del repositorio de alumnos.</param>
        public AlumnosController(IAlumnoRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Obtiene todos los alumnos almacenados en la base de datos.
        /// Devuelve solo información esencial sin datos sensibles como la contraseña.
        /// </summary>
        /// <returns>Lista de alumnos en formato JSON.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _repo.GetAllAsync();
            return Ok(lista);
        }

        /// <summary>
        /// Obtiene un alumno específico por su identificador.
        /// Devuelve datos protegidos utilizando AlumnoDTO.
        /// </summary>
        /// <param name="id">Identificador único del alumno.</param>
        /// <returns>El alumno en formato DTO o un código HTTP 404 si no existe.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var alumno = await _repo.GetByIdAsync(id);
            if (alumno == null)
                return NotFound();
            return Ok(alumno);
        }

        /// <summary>
        /// Crea un nuevo alumno en la base de datos.
        /// Asegura que los datos sean válidos antes de insertarlos.
        /// </summary>
        /// <param name="dto">Objeto Alumno con los datos a insertar.</param>
        /// <returns>El alumno creado con su identificador asignado.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AlumnoCreateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entidad = AlumnoMapper.ToEntity(dto); // 🔹 Usa el mapeador en lugar de instanciar manualmente

            var newId = await _repo.CreateAsync(entidad);
            var resultado = AlumnoMapper.ToDTO(entidad); // 🔹 Convierte la entidad a DTO para la respuesta
            resultado.AlumnoID = newId; // Asigna el ID generado

            return CreatedAtAction(nameof(GetById), new { id = newId }, resultado);
        }


        /// <summary>
        /// Actualiza los datos de un alumno existente en la base de datos.
        /// Asegura que el ID en la URL coincida con el ID en el cuerpo del request.
        /// </summary>
        /// <param name="id">Identificador único del alumno.</param>
        /// <param name="dto">Objeto Alumno con los datos actualizados.</param>
        /// <returns>Código HTTP 204 si la actualización fue exitosa, 404 si no se encuentra el alumno.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AlumnoUpdateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != dto.AlumnoID) return BadRequest("El ID en la URL no coincide con el ID en el cuerpo.");

            var entidad = AlumnoMapper.ToEntity(dto); // 🔹 Usa el mapeador para convertir DTO a entidad

            var updated = await _repo.UpdateAsync(entidad);
            return updated ? NoContent() : NotFound();
        }


        /// <summary>
        /// Elimina un alumno de la base de datos.
        /// </summary>
        /// <param name="id">Identificador único del alumno.</param>
        /// <returns>Código HTTP 204 si la eliminación fue exitosa, 404 si no se encuentra el alumno.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}