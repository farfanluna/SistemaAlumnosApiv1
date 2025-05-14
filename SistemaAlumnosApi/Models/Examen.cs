using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SistemaAlumnosApi.Models
{
    /// <summary>
    /// Representa un examen dentro del sistema académico.
    /// </summary>
    public class Examen
    {
        /// <summary>
        /// Identificador único del examen.
        /// </summary>
        [Key]
        public int ExamenID { get; set; }

        /// <summary>
        /// Título del examen. Es un campo obligatorio y no puede ser nulo.
        /// </summary>
        [Required]
        public string Titulo { get; set; } = string.Empty;

        /// <summary>
        /// Identificador de la materia asociada a este examen.
        /// </summary>
        [Required]
        public int MateriaID { get; set; }

        /// <summary>
        /// Referencia a la entidad Materia asociada.
        /// Puede ser nulo si la relación no siempre existe.
        /// </summary>
        public Materia? Materia { get; set; }

        /// <summary>
        /// Lista de preguntas asociadas al examen.
        /// </summary>
        public ICollection<Pregunta> Preguntas { get; set; } = new List<Pregunta>();

        /// <summary>
        /// Lista de calificaciones asociadas al examen.
        /// </summary>
        public ICollection<Calificacion> Calificaciones { get; set; } = new List<Calificacion>();

        /// <summary>
        /// Fecha en la que se aplicará el examen.
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaAplicacion { get; set; } = DateTime.Now;
    }
}
