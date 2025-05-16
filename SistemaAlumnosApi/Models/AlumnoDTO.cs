using System.ComponentModel.DataAnnotations;

namespace SistemaAlumnosApi.Models
{
    /// <summary>
    /// DTO para representar un alumno con datos esenciales.
    /// </summary>
    public class AlumnoDTO
    {
        public int AlumnoID { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(0, 100, ErrorMessage = "El campo {0} debe estar entre {1} y {2}.")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un email válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(0, 300, ErrorMessage = "El campo {0} debe estar entre {1} y {2} créditos.")]
        public int Creditos { get; set; }
    }

    /// <summary>
    /// DTO para creación de alumnos (password obligatorio).
    /// </summary>
    public class AlumnoCreateDTO
    {
        public int AlumnoID { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un email válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "El {0} debe tener entre {2} y {1} caracteres.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$", ErrorMessage = "El {0} debe tener al menos una letra y un número.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(0, 100, ErrorMessage = "El campo {0} debe estar entre {1} y {2}.")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(0, 300, ErrorMessage = "El campo {0} debe estar entre {1} y {2} créditos.")]
        public int Creditos { get; set; }
    }

    /// <summary>
    /// DTO para actualización de alumnos (password opcional).
    /// </summary>
    public class AlumnoUpdateDTO
    {
        public int AlumnoID { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un email válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(0, 100, ErrorMessage = "El campo {0} debe estar entre {1} y {2}.")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(0, 300, ErrorMessage = "El campo {0} debe estar entre {1} y {2} créditos.")]
        public int Creditos { get; set; }

        // Ahora opcional: si viene null o cadena vacía, no se cambia la contraseña
        [StringLength(100, MinimumLength = 6, ErrorMessage = "El {0} debe tener entre {2} y {1} caracteres.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$", ErrorMessage = "El {0} debe tener al menos una letra y un número.")]
        public string? Password { get; set; }
    }
}
