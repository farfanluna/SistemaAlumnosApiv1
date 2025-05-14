using SistemaAlumnosApi.DTOs;
using SistemaAlumnosApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAlumnosApi.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para la gestión de calificaciones en el repositorio.
    /// Define las operaciones CRUD (Create, Read, Update, Delete).
    /// </summary>
    public interface ICalificacionRepository
    {
        /// <summary>
        /// Obtiene todas las calificaciones almacenadas en la base de datos.
        /// Devuelve solo información esencial en formato DTO.
        /// </summary>
        /// <returns>Lista de calificaciones en formato DTO.</returns>
        Task<IEnumerable<CalificacionDTO>> GetAllAsync();


        /// <summary>
        /// Obtiene una calificación específica por su identificador único.
        /// Devuelve solo los datos esenciales en formato DTO.
        /// </summary>
        /// <param name="id">Identificador de la calificación.</param>
        /// <returns>La calificación en formato DTO si existe, null en caso contrario.</returns>
        Task<CalificacionDTO?> GetByIdAsync(int id);

        /// <summary>
        /// Crea una nueva calificación en la base de datos.
        /// </summary>
        /// <param name="entity">Instancia de Calificacion que se va a agregar.</param>
        /// <returns>El identificador de la nueva calificación creada.</returns>
        Task<int> CreateAsync(Calificacion entity);

        /// <summary>
        /// Actualiza los datos de una calificación existente en la base de datos.
        /// </summary>
        /// <param name="entity">Instancia de Calificacion con los valores actualizados.</param>
        /// <returns>True si la actualización fue exitosa, False en caso contrario.</returns>
        Task<bool> UpdateAsync(Calificacion entity);

        /// <summary>
        /// Elimina una calificación de la base de datos.
        /// </summary>
        /// <param name="id">Identificador de la calificación que se desea eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
