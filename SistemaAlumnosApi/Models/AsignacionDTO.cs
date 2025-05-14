using System.ComponentModel.DataAnnotations;

namespace SistemaAlumnosApi.DTOs
{
    /// <summary>
    /// DTO para representar una asignación dentro del sistema académico.
    /// Contiene solo los datos esenciales, evitando exponer relaciones completas.
    /// </summary>
    public class AsignacionDTO
    {
        /// <summary>
        /// Identificador único de la asignación.
        /// </summary>
        public int AsignacionID { get; set; }

        /// <summary>
        /// Identificador del alumno asociado a la asignación.
        /// </summary>
        public int AlumnoID { get; set; }

        /// <summary>
        /// Identificador de la materia asociada a la asignación.
        /// </summary>
        public int MateriaID { get; set; }
    }

    /// <summary>
    /// DTO para la creación de una nueva asignación.
    /// No requiere AsignacionID, ya que se genera automáticamente en la base de datos.
    /// </summary>
    public class AsignacionCreateDTO
    {
        /// <summary>
        /// Identificador del alumno asociado a la asignación.
        /// Es un campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int AlumnoID { get; set; }

        /// <summary>
        /// Identificador de la materia asociada a la asignación.
        /// Es un campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int MateriaID { get; set; }
    }

    /// <summary>
    /// DTO para la actualización de una asignación existente.
    /// Requiere AsignacionID para identificar el registro a modificar.
    /// </summary>
    public class AsignacionUpdateDTO
    {
        /// <summary>
        /// Identificador único de la asignación que se actualizará.
        /// Es un campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int AsignacionID { get; set; }

        /// <summary>
        /// Identificador del alumno asociado a la asignación.
        /// Es un campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int AlumnoID { get; set; }

        /// <summary>
        /// Identificador de la materia asociada a la asignación.
        /// Es un campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int MateriaID { get; set; }
    }
}
