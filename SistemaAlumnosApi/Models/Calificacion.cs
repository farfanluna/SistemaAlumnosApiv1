using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SistemaAlumnosApi.Models
{
    /// <summary>
    /// Representa una calificación dentro del sistema académico.
    /// </summary>
    public class Calificacion
    {
        /// <summary>
        /// Identificador único de la calificación.
        /// </summary>
        [Key]
        public int CalificacionID { get; set; }

        /// <summary>
        /// Identificador del alumno que recibió la calificación.
        /// Es un campo obligatorio.
        /// </summary>
        [Required]
        public int AlumnoID { get; set; }

        /// <summary>
        /// Referencia al objeto Alumno asociado.
        /// Puede ser nulo si la relación no siempre existe.
        /// </summary>
        public Alumno? Alumno { get; set; } // Permitir valores nulos en Alumno

        /// <summary>
        /// Identificador del examen al que corresponde la calificación.
        /// Es un campo obligatorio.
        /// </summary>
        [Required]
        public int ExamenID { get; set; }

        /// <summary>
        /// Referencia al objeto Examen asociado.
        /// Puede ser nulo si la relación no siempre existe.
        /// </summary>
        public Examen? Examen { get; set; } // Permitir valores nulos en Examen

        /// <summary>
        /// Nota obtenida en el examen.
        /// Debe estar dentro del rango permitido (0-10).
        /// </summary>
        [Required]
        [Range(0, 10, ErrorMessage = "La calificación debe estar entre 0 y 10.")]
        public decimal Nota { get; set; }
    }
}
