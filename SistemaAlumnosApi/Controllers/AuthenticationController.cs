using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SistemaAlumnosApi.Models;
using SistemaAlumnosApi.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaAlumnosApi.Controllers
{
    /// <summary>
    /// Controlador responsable de la autenticación de usuarios mediante JWT.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAlumnoRepository _repo;
        private readonly string _secretKey;

        /// <summary>
        /// Constructor que inicializa el controlador con el repositorio de alumnos y la clave secreta del JWT.
        /// </summary>
        /// <param name="repo">Repositorio de alumnos para validación de credenciales.</param>
        /// <param name="config">Configuración de la aplicación que contiene la clave secreta.</param>
        public AuthenticationController(
            IAlumnoRepository repo,
            IConfiguration config)
        {
            _repo = repo;
            _secretKey = config["Jwt:key"]!;
        }

        /// <summary>
        /// Endpoint para autenticar un usuario mediante correo y contraseña.
        /// </summary>
        /// <param name="req">Datos de inicio de sesión.</param>
        /// <returns>Un token JWT si la autenticación es exitosa, HTTP 401 si falla.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = await _repo.GetByEmailAsync(req.Email);
            if (usuario == null || !VerifyPassword(req.Password, usuario.Password))
                return Unauthorized(new { token = string.Empty });

            // Crear claims para incluir información relevante en el JWT
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, usuario.Email),
                new Claim(ClaimTypes.Name, usuario.Nombre)
            };

            // Generar la clave de firma para el token
            var keyBytes = Encoding.UTF8.GetBytes(_secretKey);
            var creds = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha512Signature);

            // Crear la estructura del token con datos y expiración
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30), // Token válido por 30 minutos
                SigningCredentials = creds
            };

            // Generar el token JWT y devolverlo en la respuesta
            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(handler.CreateToken(tokenDescriptor));

            return Ok(new { token });
        }

        /// <summary>
        /// Método auxiliar para verificar una contraseña.
        /// Si la contraseña está hasheada, se debe usar un mecanismo como BCrypt.
        /// </summary>
        /// <param name="plain">Contraseña en texto plano ingresada por el usuario.</param>
        /// <param name="hashed">Contraseña almacenada en la base de datos.</param>
        /// <returns>True si la contraseña es válida, False si no coincide.</returns>
        private bool VerifyPassword(string plain, string hashed)
        {
            // Si las contraseñas están hasheadas, usa un método de verificación como BCrypt.
            // return BCrypt.Net.BCrypt.Verify(plain, hashed);
            return plain == hashed;
        }
    }
}
