using SistemaAlumnosApi.Models;
using SistemaAlumnosApi.DTOs;

namespace SistemaAlumnosApi.Mappers
{
    /// <summary>
    /// Clase que convierte entre Asignacion (Entidad), AsignacionDTO, AsignacionCreateDTO y AsignacionUpdateDTO.
    /// </summary>
    public static class AsignacionMapper
    {
        /// <summary>
        /// Convierte una entidad Asignacion a un DTO AsignacionDTO.
        /// </summary>
        /// <param name="asignacion">Instancia de Asignacion.</param>
        /// <returns>AsignacionDTO con los datos esenciales.</returns>
        public static AsignacionDTO ToDTO(Asignacion asignacion)
        {
            return new AsignacionDTO
            {
                AsignacionID = asignacion.AsignacionID,
                AlumnoID = asignacion.AlumnoID,
                MateriaID = asignacion.MateriaID
            };
        }

        /// <summary>
        /// Convierte un DTO AsignacionCreateDTO a una entidad Asignacion.
        /// Se utiliza para la creación de nuevos registros.
        /// </summary>
        /// <param name="dto">Instancia de AsignacionCreateDTO.</param>
        /// <returns>Asignacion con los valores del DTO.</returns>
        public static Asignacion ToEntity(AsignacionCreateDTO dto)
        {
            return new Asignacion
            {
                AlumnoID = dto.AlumnoID,
                MateriaID = dto.MateriaID
            };
        }

        /// <summary>
        /// Convierte un DTO AsignacionUpdateDTO a una entidad Asignacion.
        /// Se utiliza para actualizar registros existentes.
        /// </summary>
        /// <param name="dto">Instancia de AsignacionUpdateDTO.</param>
        /// <returns>Asignacion con los valores del DTO.</returns>
        public static Asignacion ToEntity(AsignacionUpdateDTO dto)
        {
            return new Asignacion
            {
                AsignacionID = dto.AsignacionID, // 🔹 Asegura que el ID se mantenga en la actualización
                AlumnoID = dto.AlumnoID,
                MateriaID = dto.MateriaID
            };
        }
    }
}
