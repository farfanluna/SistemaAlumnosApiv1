using SistemaAlumnosApi.DTOs;
using SistemaAlumnosApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAlumnosApi.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para la gestión de exámenes en el repositorio.
    /// Define las operaciones CRUD (Create, Read, Update, Delete).
    /// </summary>
    public interface IExamenRepository
    {
        /// <summary>
        /// Obtiene todos los exámenes almacenados en la base de datos.
        /// Devuelve solo los datos esenciales en formato DTO.
        /// </summary>
        /// <returns>Lista de exámenes en formato DTO.</returns>
        Task<IEnumerable<ExamenDTO>> GetAllAsync();

        /// <summary>
        /// Busca un examen por su identificador único.
        /// Devuelve solo los datos esenciales en formato DTO.
        /// </summary>
        /// <param name="id">Identificador del examen.</param>
        /// <returns>El examen en formato DTO si existe, null en caso contrario.</returns>
        Task<ExamenDTO?> GetByIdAsync(int id);

        /// <summary>
        /// Crea un nuevo examen en la base de datos.
        /// </summary>
        /// <param name="entity">Instancia de Examen que se va a agregar.</param>
        /// <returns>El identificador del nuevo examen creado.</returns>
        Task<int> CreateAsync(ExamenCreateDTO entity);

        /// <summary>
        /// Actualiza los datos de un examen existente en la base de datos.
        /// </summary>
        /// <param name="entity">Instancia de Examen con los valores actualizados.</param>
        /// <returns>True si la actualización fue exitosa, False en caso contrario.</returns>
        Task<bool> UpdateAsync(ExamenUpdateDTO entity);

        /// <summary>
        /// Elimina un examen de la base de datos.
        /// </summary>
        /// <param name="id">Identificador del examen que se desea eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
