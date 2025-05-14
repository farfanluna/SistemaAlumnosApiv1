using SistemaAlumnosApi.Models;
using SistemaAlumnosApi.DTOs;

namespace SistemaAlumnosApi.Mappers
{
    /// <summary>
    /// Clase que convierte entre Pregunta (Entidad), PreguntaDTO, PreguntaCreateDTO y PreguntaUpdateDTO.
    /// </summary>
    public static class PreguntaMapper
    {
        /// <summary>
        /// Convierte una entidad Pregunta a un DTO PreguntaDTO.
        /// </summary>
        /// <param name="pregunta">Instancia de Pregunta.</param>
        /// <returns>PreguntaDTO con los datos esenciales.</returns>
        public static PreguntaDTO ToDTO(Pregunta pregunta)
        {
            return new PreguntaDTO
            {
                PreguntaID = pregunta.PreguntaID,
                Texto = pregunta.Texto,
                ExamenID = pregunta.ExamenID
            };
        }

        /// <summary>
        /// Convierte un DTO PreguntaCreateDTO a una entidad Pregunta.
        /// Se utiliza para la creación de nuevos registros.
        /// </summary>
        /// <param name="dto">Instancia de PreguntaCreateDTO.</param>
        /// <returns>Pregunta con los valores del DTO.</returns>
        public static Pregunta ToEntity(PreguntaCreateDTO dto)
        {
            return new Pregunta
            {
                Texto = dto.Texto,
                ExamenID = dto.ExamenID
            };
        }

        /// <summary>
        /// Convierte un DTO PreguntaUpdateDTO a una entidad Pregunta.
        /// Se utiliza para actualizar registros existentes.
        /// </summary>
        /// <param name="dto">Instancia de PreguntaUpdateDTO.</param>
        /// <returns>Pregunta con los valores del DTO.</returns>
        public static Pregunta ToEntity(PreguntaUpdateDTO dto)
        {
            return new Pregunta
            {
                PreguntaID = dto.PreguntaID, // 🔹 Se conserva el ID para la actualización
                Texto = dto.Texto,
                ExamenID = dto.ExamenID
            };
        }
    }
}
