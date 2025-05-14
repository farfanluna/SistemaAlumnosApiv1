using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SistemaAlumnosApi.Models
{
    /// <summary>
    /// Representa un alumno dentro del sistema académico.
    /// </summary>
    public class Alumno
    {
        /// <summary>
        /// Identificador único del alumno.
        /// </summary>
        [Key]
        public int AlumnoID { get; set; }

        /// <summary>
        /// Nombre del alumno. Es un campo obligatorio con un máximo de 50 caracteres.
        /// No acepta valores nulos.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string Nombre { get; set; } = string.Empty; // Inicialización para evitar valores nulos

        /// <summary>
        /// Edad del alumno. Debe estar en el rango de 0 a 100 años.
        /// Es un campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(0, 100, ErrorMessage = "El campo {0} debe estar entre {1} y {2}.")]
        public int Edad { get; set; }

        /// <summary>
        /// Correo electrónico del alumno. Debe ser válido y es un campo obligatorio.
        /// No acepta valores nulos.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un email válido.")]
        public string Email { get; set; } = string.Empty; // Inicialización para evitar valores nulos

        /// <summary>
        /// Contraseña del alumno. Debe tener entre 6 y 100 caracteres y contener al menos una letra y un número.
        /// No acepta valores nulos.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(100, ErrorMessage = "El {0} debe tener al menos {2} y máximo {1} caracteres.", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$", ErrorMessage = "El {0} debe tener al menos una letra y un número.")]
        public string Password { get; set; } = string.Empty; // Inicialización para evitar valores nulos

        /// <summary>
        /// Número de créditos acumulados por el alumno. Debe estar entre 0 y 300.
        /// Es un campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(0, 300, ErrorMessage = "El campo {0} debe estar entre {1} y {2} créditos.")]
        public int Creditos { get; set; }

        /// <summary>
        /// Lista de asignaciones asociadas al alumno.
        /// Se inicializa como una lista vacía para evitar valores nulos.
        /// </summary>
        public ICollection<Asignacion> Asignaciones { get; set; } = new List<Asignacion>();

        /// <summary>
        /// Lista de calificaciones asociadas al alumno.
        /// Se inicializa como una lista vacía para evitar valores nulos.
        /// </summary>
        public ICollection<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();
    }
}
