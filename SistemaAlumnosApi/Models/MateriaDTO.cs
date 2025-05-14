using System.ComponentModel.DataAnnotations;

namespace SistemaAlumnosApi.DTOs
{
    /// <summary>
    /// DTO para representar una materia dentro del sistema académico.
    /// Contiene solo los datos esenciales, evitando exponer relaciones completas.
    /// </summary>
    public class MateriaDTO
    {
        /// <summary>
        /// Identificador único de la materia.
        /// </summary>
        public int MateriaID { get; set; }

        /// <summary>
        /// Nombre de la materia. Es un campo obligatorio con un límite de 100 caracteres.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Cantidad de créditos que otorga la materia.
        /// </summary>
        public int Creditos { get; set; }
    }

    /// <summary>
    /// DTO para la creación de una nueva materia.
    /// No requiere MateriaID, ya que la base de datos lo genera automáticamente.
    /// </summary>
    public class MateriaCreateDTO
    {
        /// <summary>
        /// Nombre de la materia. Es un campo obligatorio con un límite de 100 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Cantidad de créditos que otorga la materia.
        /// Es un campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Creditos { get; set; }
    }

    /// <summary>
    /// DTO para la actualización de una materia existente.
    /// Requiere MateriaID para identificar el registro a modificar.
    /// </summary>
    public class MateriaUpdateDTO
    {
        /// <summary>
        /// Identificador único de la materia que se actualizará.
        /// Es un campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int MateriaID { get; set; }

        /// <summary>
        /// Nombre de la materia. Es un campo obligatorio con un límite de 100 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Cantidad de créditos que otorga la materia.
        /// Es un campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Creditos { get; set; }
    }
}
