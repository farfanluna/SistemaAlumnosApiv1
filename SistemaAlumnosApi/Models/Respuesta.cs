using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SistemaAlumnosApi.Models
{
    /// <summary>
    /// Representa una respuesta dentro del sistema de preguntas y exámenes.
    /// </summary>
    public class Respuesta
    {
        /// <summary>
        /// Identificador único de la respuesta.
        /// </summary>
        [Key]
        public int RespuestaID { get; set; }

        /// <summary>
        /// Texto de la respuesta. No puede ser nulo.
        /// </summary>
        [Required]
        public string Texto { get; set; } = string.Empty; // Inicialización para evitar valores nulos

        /// <summary>
        /// Indica si la respuesta es correcta (true) o incorrecta (false).
        /// </summary>
        [Required]
        public bool EsCorrecta { get; set; }

        /// <summary>
        /// Identificador de la pregunta asociada a esta respuesta.
        /// </summary>
        [Required]
        public int PreguntaID { get; set; }

        /// <summary>
        /// Referencia a la entidad Pregunta asociada.
        /// </summary>
        public Pregunta Pregunta { get; set; } = new Pregunta();

    }
}
