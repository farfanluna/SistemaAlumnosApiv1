using System.ComponentModel.DataAnnotations;

namespace SistemaAlumnosApi.DTOs
{
    /// <summary>
    /// DTO para representar una respuesta dentro del sistema.
    /// Contiene los campos básicos sin relaciones completas.
    /// </summary>
    public class RespuestaDTO
    {
        /// <summary>
        /// Identificador único de la respuesta.
        /// </summary>
        public int RespuestaID { get; set; }

        /// <summary>
        /// Texto de la respuesta.
        /// </summary>
        public string Texto { get; set; } = string.Empty;

        /// <summary>
        /// Indica si la respuesta es correcta (true) o incorrecta (false).
        /// </summary>
        public bool EsCorrecta { get; set; }

        /// <summary>
        /// Identificador de la pregunta asociada a esta respuesta.
        /// </summary>
        public int PreguntaID { get; set; }
    }

    /// <summary>
    /// DTO para la creación de una nueva respuesta.
    /// No incluye el ID porque lo genera la base de datos.
    /// </summary>
    public class RespuestaCreateDTO
    {
        /// <summary>
        /// Texto de la nueva respuesta.
        /// Campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Texto { get; set; } = string.Empty;

        /// <summary>
        /// Indica si la respuesta es correcta (true) o incorrecta (false).
        /// Campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public bool EsCorrecta { get; set; }

        /// <summary>
        /// Identificador del examen al que pertenece la pregunta asociada.
        /// Campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int PreguntaID { get; set; }
    }

    /// <summary>
    /// DTO para la actualización de una respuesta existente.
    /// Requiere el ID de la respuesta para identificarla.
    /// </summary>
    public class RespuestaUpdateDTO
    {
        /// <summary>
        /// Identificador único de la respuesta que se actualizará.
        /// Campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int RespuestaID { get; set; }

        /// <summary>
        /// Nuevo texto para la respuesta.
        /// Campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Texto { get; set; } = string.Empty;

        /// <summary>
        /// Indicador de si la respuesta es correcta (true) o incorrecta (false).
        /// Campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public bool EsCorrecta { get; set; }

        /// <summary>
        /// Identificador del examen al que pertenece la pregunta asociada.
        /// Campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int PreguntaID { get; set; }
    }
}
