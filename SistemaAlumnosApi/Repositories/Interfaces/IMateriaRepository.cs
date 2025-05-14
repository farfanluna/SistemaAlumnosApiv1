using SistemaAlumnosApi.DTOs;
using SistemaAlumnosApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAlumnosApi.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz para la gestión de materias en el repositorio.
    /// Define las operaciones CRUD (Create, Read, Update, Delete).
    /// </summary>
    public interface IMateriaRepository
    {
        /// <summary>
        /// Obtiene todas las materias almacenadas en la base de datos.
        /// </summary>
        /// <returns>Una colección de objetos Materia.</returns>
        Task<IEnumerable<Materia>> GetAllAsync();

        /// <summary>
        /// Busca una materia por su identificador único.
        /// </summary>
        /// <param name="id">Identificador de la materia.</param>
        /// <returns>La materia si existe, null en caso contrario.</returns>
        Task<Materia?> GetByIdAsync(int id);

        /// <summary>
        /// Crea una nueva materia en la base de datos.
        /// </summary>
        /// <param name="entity">Instancia de Materia que se va a agregar.</param>
        /// <returns>El identificador de la nueva materia creada.</returns>
       
        Task<int> CreateAsync(MateriaCreateDTO dto);

        /// <summary>
        /// Actualiza los datos de una materia existente en la base de datos.
        /// </summary>
        /// <param name="entity">Instancia de Materia con los valores actualizados.</param>
        /// <returns>True si la actualización fue exitosa, False en caso contrario.</returns>
        Task<bool> UpdateAsync(MateriaUpdateDTO dto);

        /// <summary>
        /// Elimina una materia de la base de datos.
        /// </summary>
        /// <param name="id">Identificador de la materia que se desea eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
