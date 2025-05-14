using SistemaAlumnosApi.Models;

namespace SistemaAlumnosApi.Mappers
{
    /// <summary>
    /// Clase que convierte entre Alumno (Entidad), AlumnoDTO, AlumnoCreateDTO y AlumnoUpdateDTO.
    /// </summary>
    public static class AlumnoMapper
    {
        /// <summary>
        /// Convierte una entidad Alumno a un DTO AlumnoDTO.
        /// </summary>
        /// <param name="alumno">Instancia de Alumno.</param>
        /// <returns>AlumnoDTO con los datos esenciales.</returns>
        public static AlumnoDTO ToDTO(Alumno alumno)
        {
            return new AlumnoDTO
            {
                AlumnoID = alumno.AlumnoID,
                Nombre = alumno.Nombre,
                Email = alumno.Email,
                Creditos = alumno.Creditos
            };
        }

        /// <summary>
        /// Convierte un DTO AlumnoCreateDTO a una entidad Alumno.
        /// Se utiliza para la creación de nuevos registros.
        /// </summary>
        /// <param name="dto">Instancia de AlumnoCreateDTO.</param>
        /// <returns>Alumno con los valores del DTO.</returns>
        public static Alumno ToEntity(AlumnoCreateDTO dto)
        {
            return new Alumno
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Password = dto.Password, // 🔹 Guarda la contraseña solo en la creación
                Creditos = dto.Creditos
            };
        }

        /// <summary>
        /// Convierte un DTO AlumnoUpdateDTO a una entidad Alumno.
        /// Se utiliza para actualizar registros existentes.
        /// </summary>
        /// <param name="dto">Instancia de AlumnoUpdateDTO.</param>
        /// <returns>Alumno con los valores del DTO.</returns>
        public static Alumno ToEntity(AlumnoUpdateDTO dto)
        {
            return new Alumno
            {
                AlumnoID = dto.AlumnoID, // 🔹 Asegura que el ID sea el mismo al actualizar
                Nombre = dto.Nombre,
                Email = dto.Email,
                Password = dto.Password,
                Creditos = dto.Creditos
            };
        }
    }
}
