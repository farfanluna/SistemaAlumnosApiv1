using SistemaAlumnosApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAlumnosApi.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para la gestión de asignaciones en el repositorio.
    /// Define las operaciones básicas CRUD (Create, Read, Update, Delete).
    /// </summary>
    public interface IAsignacionRepository
    {
        /// <summary>
        /// Obtiene todas las asignaciones almacenadas en la base de datos.
        /// </summary>
        /// <returns>Una colección de objetos Asignacion.</returns>
        Task<IEnumerable<Asignacion>> GetAllAsync();

        /// <summary>
        /// Busca una asignación por su identificador único.
        /// </summary>
        /// <param name="id">Identificador de la asignación.</param>
        /// <returns>La asignación si existe, null en caso contrario.</returns>
        Task<Asignacion?> GetByIdAsync(int id);

        /// <summary>
        /// Crea una nueva asignación en la base de datos.
        /// </summary>
        /// <param name="entity">Instancia de Asignacion que se va a agregar.</param>
        /// <returns>El identificador de la nueva asignación creada.</returns>
        Task<int> CreateAsync(Asignacion entity);

        /// <summary>
        /// Actualiza los datos de una asignación existente en la base de datos.
        /// </summary>
        /// <param name="entity">Instancia de Asignacion con los valores actualizados.</param>
        /// <returns>True si la actualización fue exitosa, False en caso contrario.</returns>
        Task<bool> UpdateAsync(Asignacion entity);

        /// <summary>
        /// Elimina una asignación de la base de datos.
        /// </summary>
        /// <param name="id">Identificador de la asignación que se desea eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
