using SistemaAlumnosApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAlumnosApi.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para la gestión de respuestas en el repositorio.
    /// Define las operaciones CRUD (Create, Read, Update, Delete).
    /// </summary>
    public interface IRespuestaRepository
    {
        /// <summary>
        /// Obtiene todas las respuestas almacenadas en la base de datos.
        /// </summary>
        /// <returns>Una colección de objetos Respuesta.</returns>
        Task<IEnumerable<Respuesta>> GetAllAsync();

        /// <summary>
        /// Busca una respuesta por su identificador único.
        /// </summary>
        /// <param name="id">Identificador de la respuesta.</param>
        /// <returns>La respuesta si existe, null en caso contrario.</returns>
        Task<Respuesta?> GetByIdAsync(int id);

        /// <summary>
        /// Crea una nueva respuesta en la base de datos.
        /// </summary>
        /// <param name="entity">Instancia de Respuesta que se va a agregar.</param>
        /// <returns>El identificador de la nueva respuesta creada.</returns>
        Task<int> CreateAsync(Respuesta entity);

        /// <summary>
        /// Actualiza los datos de una respuesta existente en la base de datos.
        /// </summary>
        /// <param name="entity">Instancia de Respuesta con los valores actualizados.</param>
        /// <returns>True si la actualización fue exitosa, False en caso contrario.</returns>
        Task<bool> UpdateAsync(Respuesta entity);

        /// <summary>
        /// Elimina una respuesta de la base de datos.
        /// </summary>
        /// <param name="id">Identificador de la respuesta que se desea eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
