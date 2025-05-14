using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SistemaAlumnosApi.Models
{
    /// <summary>
    /// Representa una asignación de un alumno a una materia dentro del sistema académico.
    /// </summary>
    public class Asignacion
    {
        /// <summary>
        /// Identificador único de la asignación.
        /// </summary>
        [Key]
        public int AsignacionID { get; set; }

        /// <summary>
        /// Identificador del alumno asociado a la asignación.
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
        /// Identificador de la materia asociada a la asignación.
        /// Es un campo obligatorio.
        /// </summary>
        [Required]
        public int MateriaID { get; set; }

        /// <summary>
        /// Referencia al objeto Materia asociado.
        /// Puede ser nulo si la relación no siempre existe.
        /// </summary>
        public Materia? Materia { get; set; } // Permitir valores nulos en Materia
    }
}
