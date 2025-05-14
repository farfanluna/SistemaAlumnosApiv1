using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SistemaAlumnosApi.DTOs
{
    /// <summary>
    /// DTO para representar una pregunta dentro del sistema.
    /// Contiene los campos básicos sin relaciones completas.
    /// </summary>
    public class PreguntaDTO
    {
        /// <summary>
        /// Identificador único de la pregunta.
        /// </summary>
        public int PreguntaID { get; set; }

        /// <summary>
        /// Texto de la pregunta.
        /// </summary>
        public string Texto { get; set; } = string.Empty;

        /// <summary>
        /// Identificador del examen al que pertenece la pregunta.
        /// </summary>
        public int ExamenID { get; set; }
    }

    /// <summary>
    /// DTO para la creación de una nueva pregunta.
    /// No incluye el ID porque lo genera la base de datos.
    /// </summary>
    public class PreguntaCreateDTO
    {
        /// <summary>
        /// Texto de la nueva pregunta.
        /// Campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Texto { get; set; } = string.Empty;

        /// <summary>
        /// Identificador del examen al que pertenece la pregunta.
        /// Campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int ExamenID { get; set; }
    }

    /// <summary>
    /// DTO para la actualización de una pregunta existente.
    /// Requiere el ID de la pregunta para identificarla.
    /// </summary>
    public class PreguntaUpdateDTO
    {
        /// <summary>
        /// Identificador único de la pregunta que se actualizará.
        /// Campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int PreguntaID { get; set; }

        /// <summary>
        /// Nuevo texto para la pregunta.
        /// Campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Texto { get; set; } = string.Empty;

        /// <summary>
        /// Identificador actualizado del examen al que pertenece la pregunta.
        /// Campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int ExamenID { get; set; }
    }
}
