using System.ComponentModel.DataAnnotations;

namespace SistemaAlumnosApi.DTOs
{
    /// <summary>
    /// DTO para representar una calificación dentro del sistema académico.
    /// Contiene solo los datos esenciales, sin exponer relaciones completas.
    /// </summary>
    public class CalificacionDTO
    {
        /// <summary>
        /// Identificador único de la calificación.
        /// </summary>
        public int CalificacionID { get; set; }

        /// <summary>
        /// Identificador del alumno que recibió la calificación.
        /// </summary>
        public int AlumnoID { get; set; }

        /// <summary>
        /// Identificador del examen asociado a la calificación.
        /// </summary>
        public int ExamenID { get; set; }

        /// <summary>
        /// Nota obtenida por el alumno en el examen.
        /// </summary>
        public decimal Nota { get; set; }
    }

    /// <summary>
    /// DTO para la creación de una nueva calificación.
    /// No requiere CalificacionID, ya que la base de datos lo genera automáticamente.
    /// </summary>
    public class CalificacionCreateDTO
    {
        /// <summary>
        /// Identificador del alumno que recibe la calificación.
        /// Es un campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int AlumnoID { get; set; }

        /// <summary>
        /// Identificador del examen al que corresponde la calificación.
        /// Es un campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int ExamenID { get; set; }

        /// <summary>
        /// Nota obtenida por el alumno en el examen.
        /// Debe estar en el rango de 0 a 100.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(0, 100, ErrorMessage = "El campo {0} debe estar entre {1} y {2}.")]
        public decimal Nota { get; set; }
    }

    /// <summary>
    /// DTO para la actualización de una calificación existente.
    /// Requiere CalificacionID para identificar el registro a modificar.
    /// </summary>
    public class CalificacionUpdateDTO
    {
        /// <summary>
        /// Identificador único de la calificación que se actualizará.
        /// Es un campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int CalificacionID { get; set; }

        /// <summary>
        /// Identificador del alumno que recibió la calificación.
        /// Es un campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int AlumnoID { get; set; }

        /// <summary>
        /// Identificador del examen asociado a la calificación.
        /// Es un campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int ExamenID { get; set; }

        /// <summary>
        /// Nota obtenida por el alumno en el examen.
        /// Debe estar en el rango de 0 a 100.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(0, 100, ErrorMessage = "El campo {0} debe estar entre {1} y {2}.")]
        public decimal Nota { get; set; } // 🔹 Cambiar de float a decimal

    }
}
