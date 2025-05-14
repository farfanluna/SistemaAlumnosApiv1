using System.ComponentModel.DataAnnotations;

namespace SistemaAlumnosApi.DTOs
{
    /// <summary>
    /// DTO para representar un examen dentro del sistema académico.
    /// Contiene solo los datos esenciales, evitando exponer relaciones completas.
    /// </summary>
    public class ExamenDTO
    {
        /// <summary>
        /// Identificador único del examen.
        /// </summary>
        public int ExamenID { get; set; }

        /// <summary>
        /// Título del examen. Es un campo obligatorio y no puede ser nulo.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Titulo { get; set; } = string.Empty;

        /// <summary>
        /// Identificador de la materia asociada a este examen.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int MateriaID { get; set; }
    }
}
