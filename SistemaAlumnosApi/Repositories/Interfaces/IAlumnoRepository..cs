using SistemaAlumnosApi.Models;

public interface IAlumnoRepository
{
    /// <summary>
    /// Obtiene la lista completa de alumnos almacenados en la base de datos (sin datos sensibles).
    /// </summary>
    /// <returns>Una colección de objetos AlumnoDTO.</returns>
    Task<IEnumerable<AlumnoDTO>> GetAllAsync();

    /// <summary>
    /// Obtiene un alumno específico por su identificador único (sin datos sensibles).
    /// </summary>
    /// <param name="id">Identificador del alumno.</param>
    /// <returns>El alumno en formato DTO si existe, null en caso contrario.</returns>
    Task<AlumnoDTO?> GetByIdAsync(int id);

    /// <summary>
    /// Busca un alumno por su dirección de correo electrónico (puede necesitar validación adicional).
    /// </summary>
    /// <param name="email">Correo electrónico del alumno.</param>
    /// <returns>El alumno si existe, null en caso contrario.</returns>
    Task<Alumno?> GetByEmailAsync(string email);

    /// <summary>
    /// Crea un nuevo alumno en la base de datos.
    /// </summary>
    /// <param name="entity">Instancia del alumno que se va a agregar.</param>
    /// <returns>El identificador del nuevo alumno creado.</returns>
    Task<int> CreateAsync(Alumno entity);

    /// <summary>
    /// Actualiza los datos de un alumno existente en la base de datos.
    /// </summary>
    /// <param name="entity">Instancia del alumno con los valores actualizados.</param>
    /// <returns>True si la actualización fue exitosa, False en caso contrario.</returns>
    Task<bool> UpdateAsync(Alumno entity);

    /// <summary>
    /// Elimina un alumno de la base de datos.
    /// </summary>
    /// <param name="id">Identificador del alumno que se desea eliminar.</param>
    /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
    Task<bool> DeleteAsync(int id);
}
