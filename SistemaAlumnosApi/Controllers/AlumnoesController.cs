using Microsoft.AspNetCore.Mvc;
using SistemaAlumnosApi.Mappers;
using SistemaAlumnosApi.Models;
using SistemaAlumnosApi.Repositories.Interfaces;
using System.Threading.Tasks;


using Microsoft.Data.SqlClient;


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
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AlumnoCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entidad = AlumnoMapper.ToEntity(dto);
            var newId = await _repo.CreateAsync(entidad);

            var resultado = AlumnoMapper.ToDTO(entidad);
            resultado.AlumnoID = newId;

            return CreatedAtAction(nameof(GetById), new { id = newId }, resultado);
        }

        /// <summary>
        /// Actualiza los datos de un alumno existente en la base de datos.
        /// El password solo se aplicará si viene en el DTO; de lo contrario, se conserva el anterior.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AlumnoUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.AlumnoID)
                return BadRequest("El ID en la URL no coincide con el ID en el cuerpo.");

            // Convertimos el DTO a entidad. dto.Password puede ser null o empty
            var entidad = AlumnoMapper.ToEntity(dto);

            // El repositorio sabrá omitir el password si dto.Password es null/empty
            var updated = await _repo.UpdateAsync(entidad);

            return updated ? NoContent() : NotFound();
        }

        /// <summary>
        /// Elimina un alumno de la base de datos.
        /// </summary>
        [HttpDelete("{id}")]
      
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _repo.DeleteAsync(id);
                return deleted ? NoContent() : NotFound();
            }
            catch (SqlException ex) when (ex.Message.Contains("No se puede eliminar el alumno"))
            {
                // Código 400 con detalle del mensaje de negocio
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
