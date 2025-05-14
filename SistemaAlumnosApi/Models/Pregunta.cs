using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SistemaAlumnosApi.Models
{
    /// <summary>
    /// Representa una pregunta dentro del sistema de evaluación.
    /// </summary>
    public class Pregunta
    {
        /// <summary>
        /// Identificador único de la pregunta.
        /// </summary>
        [Key]
        public int PreguntaID { get; set; }

        /// <summary>
        /// Texto de la pregunta. No puede ser nulo.
        /// </summary>
        [Required]
        public string Texto { get; set; } = string.Empty; // Inicialización para evitar valores nulos

        /// <summary>
        /// Identificador del examen al que pertenece la pregunta.
        /// </summary>
        [Required]
        public int ExamenID { get; set; }

        /// <summary>
        /// Referencia al objeto Examen asociado a la pregunta.
        /// Puede ser nulo si la relación no siempre existe.
        /// </summary>
        public Examen? Examen { get; set; }

        /// <summary>
        /// Lista de respuestas asociadas a esta pregunta.
        /// Se inicializa como una lista vacía para evitar valores nulos.
        /// </summary>
        public ICollection<Respuesta> Respuestas { get; set; } = new List<Respuesta>();
    }
}
