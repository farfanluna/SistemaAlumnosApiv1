using SistemaAlumnosApi.Models;
using SistemaAlumnosApi.Repositories.Interfaces;
using SistemaAlumnosApi.Mappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaAlumnosApi.DTOs;

namespace SistemaAlumnosApi.Repositories.Sql
{
    /// <summary>
    /// Repositorio SQL para la gestión de materias en la base de datos.
    /// Implementa la interfaz IMateriaRepository para realizar operaciones CRUD.
    /// </summary>
    public class SqlMateriaRepository : IMateriaRepository
    {
        private readonly string _conn;

        /// <summary>
        /// Constructor que inicializa la conexión con la base de datos.
        /// </summary>
        /// <param name="cfg">Configuración utilizada para obtener la cadena de conexión.</param>
        public SqlMateriaRepository(IConfiguration cfg) =>
            _conn = cfg.GetConnectionString("DefaultConnection")!;

        /// <summary>
        /// Obtiene todas las materias almacenadas en la base de datos.
        /// </summary>
        /// <returns>Lista de materias.</returns>
        public async Task<IEnumerable<Materia>> GetAllAsync()
        {
            var list = new List<Materia>();
            const string sql = @"SELECT MateriaID, Nombre, Creditos FROM Materias";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            await cn.OpenAsync();
            using var rd = await cmd.ExecuteReaderAsync();

            while (await rd.ReadAsync())
            {
                list.Add(new Materia
                {
                    MateriaID = rd.GetInt32(0),
                    Nombre = rd.GetString(1),
                    Creditos = rd.GetInt32(2)
                });
            }

            return list;
        }

        /// <summary>
        /// Obtiene una materia específica por su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la materia.</param>
        /// <returns>Instancia de Materia si existe, null si no se encuentra.</returns>
        public async Task<Materia?> GetByIdAsync(int id)
        {
            const string sql = @"SELECT MateriaID, Nombre, Creditos FROM Materias WHERE MateriaID=@id";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", id);
            await cn.OpenAsync();
            using var rd = await cmd.ExecuteReaderAsync();

            if (!await rd.ReadAsync()) return null;

            return new Materia
            {
                MateriaID = rd.GetInt32(0),
                Nombre = rd.GetString(1),
                Creditos = rd.GetInt32(2)
            };
        }

        /// <summary>
        /// Crea una nueva materia en la base de datos y devuelve su ID generado.
        /// </summary>
        /// <param name="dto">Entidad Materia a insertar.</param>
        /// <returns>Identificador de la nueva materia.</returns>
        public async Task<int> CreateAsync(MateriaCreateDTO dto)
        {
            var entidad = MateriaMapper.ToEntity(dto); // 🔹 Convierte DTO a entidad

            const string sql = @"
                INSERT INTO Materias (Nombre, Creditos)
                VALUES (@n, @c);
                SELECT SCOPE_IDENTITY();";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@n", entidad.Nombre);
            cmd.Parameters.AddWithValue("@c", entidad.Creditos);
            await cn.OpenAsync();

            return Convert.ToInt32(await cmd.ExecuteScalarAsync());
        }

        /// <summary>
        /// Actualiza una materia existente en la base de datos.
        /// </summary>
        /// <param name="dto">Entidad Materia con los valores actualizados.</param>
        /// <returns>True si la actualización fue exitosa, False en caso contrario.</returns>
        public async Task<bool> UpdateAsync(MateriaUpdateDTO dto)
        {
            var entidad = MateriaMapper.ToEntity(dto); // 🔹 Convierte DTO a entidad

            const string sql = @"
                UPDATE Materias
                SET Nombre=@n, Creditos=@c
                WHERE MateriaID=@id";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", entidad.MateriaID);
            cmd.Parameters.AddWithValue("@n", entidad.Nombre);
            cmd.Parameters.AddWithValue("@c", entidad.Creditos);
            await cn.OpenAsync();

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        /// <summary>
        /// Elimina una materia de la base de datos.
        /// </summary>
        /// <param name="id">Identificador único de la materia a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = @"DELETE FROM Materias WHERE MateriaID=@id";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", id);
            await cn.OpenAsync();

            return await cmd.ExecuteNonQueryAsync() > 0;
        }
    }
}
