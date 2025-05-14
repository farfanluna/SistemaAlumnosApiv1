using SistemaAlumnosApi.Models;
using SistemaAlumnosApi.DTOs;

namespace SistemaAlumnosApi.Mappers
{
    public static class RespuestaMapper
    {
        /// <summary>
        /// Convierte una entidad Respuesta a un DTO.
        /// </summary>
        public static RespuestaDTO ToDTO(Respuesta respuesta)
        {
            return new RespuestaDTO
            {
                RespuestaID = respuesta.RespuestaID,
                Texto = respuesta.Texto,
                EsCorrecta = respuesta.EsCorrecta,
                PreguntaID = respuesta.PreguntaID
            };
        }

        /// <summary>
        /// Convierte un DTO de creación a una entidad Respuesta.
        /// </summary>
        public static Respuesta FromCreateDTO(RespuestaCreateDTO dto)
        {
            return new Respuesta
            {
                Texto = dto.Texto,
                EsCorrecta = dto.EsCorrecta,
                PreguntaID = dto.PreguntaID
            };
        }

        /// <summary>
        /// Convierte un DTO de actualización a una entidad Respuesta.
        /// </summary>
        public static Respuesta FromUpdateDTO(RespuestaUpdateDTO dto)
        {
            return new Respuesta
            {
                RespuestaID = dto.RespuestaID,
                Texto = dto.Texto,
                EsCorrecta = dto.EsCorrecta,
                PreguntaID = dto.PreguntaID
            };
        }

        /// <summary>
        /// Convierte una entidad Respuesta a un DTO de actualización.
        /// Útil cuando necesitas llenar un formulario con datos existentes.
        /// </summary>
        public static RespuestaUpdateDTO ToUpdateDTO(Respuesta respuesta)
        {
            return new RespuestaUpdateDTO
            {
                RespuestaID = respuesta.RespuestaID,
                Texto = respuesta.Texto,
                EsCorrecta = respuesta.EsCorrecta,
                PreguntaID = respuesta.PreguntaID
            };
        }
    }
}
