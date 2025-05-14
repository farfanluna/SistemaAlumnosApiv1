using SistemaAlumnosApi.DTOs;
using SistemaAlumnosApi.Models;

namespace SistemaAlumnosApi.Mappers
{
    /// <summary>
    /// Clase que convierte entre Examen (Entidad) y ExamenDTO.
    /// </summary>
    public static class ExamenMapper
    {
        /// <summary>
        /// Convierte una entidad Examen a un DTO ExamenDTO.
        /// </summary>
        /// <param name="examen">Instancia de Examen.</param>
        /// <returns>ExamenDTO con los datos esenciales.</returns>
        public static ExamenDTO ToDTO(Examen examen)
        {
            return new ExamenDTO
            {
                ExamenID = examen.ExamenID,
                Titulo = examen.Titulo,
                MateriaID = examen.MateriaID
            };
        }

        /// <summary>
        /// Convierte un DTO ExamenDTO a una entidad Examen.
        /// </summary>
        /// <param name="dto">Instancia de ExamenDTO.</param>
        /// <returns>Examen con los valores del DTO.</returns>
        public static Examen ToEntity(ExamenDTO dto)
        {
            return new Examen
            {
                ExamenID = dto.ExamenID,
                Titulo = dto.Titulo,
                MateriaID = dto.MateriaID
            };
        }
    }
}
