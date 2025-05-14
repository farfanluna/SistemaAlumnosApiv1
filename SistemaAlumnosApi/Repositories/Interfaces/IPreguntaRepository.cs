using SistemaAlumnosApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAlumnosApi.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para la gestión de preguntas en el repositorio.
    /// Define las operaciones CRUD (Create, Read, Update, Delete).
    /// </summary>
    public interface IPreguntaRepository
    {
        /// <summary>
        /// Obtiene todas las preguntas almacenadas en la base de datos.
        /// </summary>
        /// <returns>Una colección de objetos Pregunta.</returns>
        Task<IEnumerable<Pregunta>> GetAllAsync();

        /// <summary>
        /// Busca una pregunta por su identificador único.
        /// </summary>
        /// <param name="id">Identificador de la pregunta.</param>
        /// <returns>La pregunta si existe, null en caso contrario.</returns>
        Task<Pregunta?> GetByIdAsync(int id);

        /// <summary>
        /// Crea una nueva pregunta en la base de datos.
        /// </summary>
        /// <param name="entity">Instancia de Pregunta que se va a agregar.</param>
        /// <returns>El identificador de la nueva pregunta creada.</returns>
        Task<int> CreateAsync(Pregunta entity);

        /// <summary>
        /// Actualiza los datos de una pregunta existente en la base de datos.
        /// </summary>
        /// <param name="entity">Instancia de Pregunta con los valores actualizados.</param>
        /// <returns>True si la actualización fue exitosa, False en caso contrario.</returns>
        Task<bool> UpdateAsync(Pregunta entity);

        /// <summary>
        /// Elimina una pregunta de la base de datos.
        /// </summary>
        /// <param name="id">Identificador de la pregunta que se desea eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
