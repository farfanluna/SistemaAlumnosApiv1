using Microsoft.AspNetCore.Mvc;
using SistemaAlumnosApi.DTOs;
using SistemaAlumnosApi.Repositories.Interfaces;
using System.Threading.Tasks;

namespace SistemaAlumnosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespuestasController : ControllerBase
    {
        private readonly IRespuestaRepository _repo;

        public RespuestasController(IRespuestaRepository repo)
            => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _repo.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var r = await _repo.GetByIdAsync(id);
            return r is null ? NotFound() : Ok(r);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RespuestaCreateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var id = await _repo.CreateAsync(dto);

            // Opcionalmente devolver el DTO con ID (solo si lo necesitas mostrar al cliente)
            var created = new
            {
                RespuestaID = id,
                dto.Texto,
                dto.EsCorrecta,
                dto.PreguntaID
            };

            return CreatedAtAction(nameof(GetById), new { id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RespuestaUpdateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != dto.RespuestaID) return BadRequest("ID mismatch");

            var ok = await _repo.UpdateAsync(dto);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => await _repo.DeleteAsync(id) ? NoContent() : NotFound();
    }
}
