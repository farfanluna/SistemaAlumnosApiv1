using System;
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
        /// Título del examen. Es un campo obligatorio.
        /// </summary>
        public string Titulo { get; set; } = string.Empty;

        /// <summary>
        /// Identificador de la materia asociada al examen.
        /// </summary>
        public int MateriaID { get; set; }

        /// <summary>
        /// Fecha en la que se aplicará el examen.
        /// </summary>
        public DateTime FechaAplicacion { get; set; }
    }

    /// <summary>
    /// DTO para la creación de un nuevo examen.
    /// No requiere ExamenID, ya que la base de datos lo genera automáticamente.
    /// </summary>
    public class ExamenCreateDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int MateriaID { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Date)]
        public DateTime FechaAplicacion { get; set; } = DateTime.Now;
    }

    /// <summary>
    /// DTO para la actualización de un examen existente.
    /// Requiere ExamenID para identificar el registro a modificar.
    /// </summary>
    public class ExamenUpdateDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int ExamenID { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int MateriaID { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Date)]
        public DateTime FechaAplicacion { get; set; }
    }
}
