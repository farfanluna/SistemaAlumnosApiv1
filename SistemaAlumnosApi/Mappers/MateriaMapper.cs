using SistemaAlumnosApi.Models;
using SistemaAlumnosApi.DTOs;

namespace SistemaAlumnosApi.Mappers
{
    /// <summary>
    /// Clase que convierte entre Materia (Entidad), MateriaDTO, MateriaCreateDTO y MateriaUpdateDTO.
    /// </summary>
    public static class MateriaMapper
    {
        /// <summary>
        /// Convierte una entidad Materia a un DTO MateriaDTO.
        /// </summary>
        /// <param name="materia">Instancia de Materia.</param>
        /// <returns>MateriaDTO con los datos esenciales.</returns>
        public static MateriaDTO ToDTO(Materia materia)
        {
            return new MateriaDTO
            {
                MateriaID = materia.MateriaID,
                Nombre = materia.Nombre,
                Creditos = materia.Creditos
            };
        }

        /// <summary>
        /// Convierte un DTO MateriaCreateDTO a una entidad Materia.
        /// Se utiliza para la creación de nuevos registros.
        /// </summary>
        /// <param name="dto">Instancia de MateriaCreateDTO.</param>
        /// <returns>Materia con los valores del DTO.</returns>
        public static Materia ToEntity(MateriaCreateDTO dto)
        {
            return new Materia
            {
                Nombre = dto.Nombre,
                Creditos = dto.Creditos
            };
        }

        /// <summary>
        /// Convierte un DTO MateriaUpdateDTO a una entidad Materia.
        /// Se utiliza para actualizar registros existentes.
        /// </summary>
        /// <param name="dto">Instancia de MateriaUpdateDTO.</param>
        /// <returns>Materia con los valores del DTO.</returns>
        public static Materia ToEntity(MateriaUpdateDTO dto)
        {
            return new Materia
            {
                MateriaID = dto.MateriaID, // 🔹 Asegura que el ID se mantenga en la actualización
                Nombre = dto.Nombre,
                Creditos = dto.Creditos
            };
        }
    }
}
