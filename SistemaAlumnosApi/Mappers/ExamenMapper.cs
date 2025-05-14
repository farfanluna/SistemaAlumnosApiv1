using SistemaAlumnosApi.Models;
using SistemaAlumnosApi.DTOs;

namespace SistemaAlumnosApi.Mappers
{
    /// <summary>
    /// Clase que convierte entre Examen (Entidad), ExamenDTO, ExamenCreateDTO y ExamenUpdateDTO.
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
                MateriaID = examen.MateriaID,
                FechaAplicacion = examen.FechaAplicacion
            };
        }

        /// <summary>
        /// Convierte un DTO ExamenCreateDTO a una entidad Examen.
        /// Se utiliza para la creación de nuevos registros.
        /// </summary>
        /// <param name="dto">Instancia de ExamenCreateDTO.</param>
        /// <returns>Examen con los valores del DTO.</returns>
        public static Examen ToEntity(ExamenCreateDTO dto)
        {
            return new Examen
            {
                Titulo = dto.Titulo,
                MateriaID = dto.MateriaID,
                FechaAplicacion = dto.FechaAplicacion
            };
        }

        /// <summary>
        /// Convierte un DTO ExamenUpdateDTO a una entidad Examen.
        /// Se utiliza para actualizar registros existentes.
        /// </summary>
        /// <param name="dto">Instancia de ExamenUpdateDTO.</param>
        /// <returns>Examen con los valores del DTO.</returns>
        public static Examen ToEntity(ExamenUpdateDTO dto)
        {
            return new Examen
            {
                ExamenID = dto.ExamenID, // 🔹 Asegura que el ID se mantenga en la actualización
                Titulo = dto.Titulo,
                MateriaID = dto.MateriaID,
                FechaAplicacion = dto.FechaAplicacion
            };
        }
    }
}
