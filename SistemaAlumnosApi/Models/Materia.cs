using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SistemaAlumnosApi.Models
{
    /// <summary>
    /// Representa una materia dentro del sistema académico.
    /// </summary>
    public class Materia
    {
        /// <summary>
        /// Identificador único de la materia.
        /// </summary>
        [Key]
        public int MateriaID { get; set; }

        /// <summary>
        /// Nombre de la materia. Es un campo obligatorio con un límite de 100 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string Nombre { get; set; } = string.Empty; // Inicialización para evitar valores nulos

        /// <summary>
        /// Cantidad de créditos que otorga la materia. Es un campo obligatorio.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Creditos { get; set; }

        /// <summary>
        /// Lista de asignaciones asociadas a la materia.
        /// Se inicializa como una lista vacía para evitar valores nulos.
        /// </summary>
        public ICollection<Asignacion> Asignaciones { get; set; } = new List<Asignacion>();

        /// <summary>
        /// Lista de exámenes asociados a la materia.
        /// Se inicializa como una lista vacía para evitar valores nulos.
        /// </summary>
        public ICollection<Examen> Examenes { get; set; } = new List<Examen>();
    }
}
