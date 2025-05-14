using SistemaAlumnosApi.Models;
using SistemaAlumnosApi.DTOs;
using SistemaAlumnosApi.Repositories.Interfaces;
using SistemaAlumnosApi.Mappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAlumnosApi.Repositories.Sql
{
    /// <summary>
    /// Repositorio SQL para la gestión de exámenes en la base de datos.
    /// Implementa la interfaz IExamenRepository para realizar operaciones CRUD.
    /// </summary>
    public class SqlExamenRepository : IExamenRepository
    {
        private readonly string _conn;

        /// <summary>
        /// Constructor que inicializa la conexión con la base de datos.
        /// </summary>
        /// <param name="cfg">Configuración utilizada para obtener la cadena de conexión.</param>
        public SqlExamenRepository(IConfiguration cfg) =>
            _conn = cfg.GetConnectionString("DefaultConnection")!;

        /// <summary>
        /// Obtiene todos los exámenes almacenados en la base de datos y los convierte a DTOs.
        /// </summary>
        /// <returns>Lista de exámenes en formato DTO.</returns>
        public async Task<IEnumerable<ExamenDTO>> GetAllAsync()
        {
            var list = new List<Examen>();
            const string sql = @"SELECT ExamenID, Titulo, MateriaID FROM Examenes";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            await cn.OpenAsync();
            using var rd = await cmd.ExecuteReaderAsync();

            while (await rd.ReadAsync())
            {
                list.Add(new Examen
                {
                    ExamenID = rd.GetInt32(0),
                    Titulo = rd.GetString(1),
                    MateriaID = rd.GetInt32(2)
                });
            }

            return list.Select(ExamenMapper.ToDTO);
        }

        /// <summary>
        /// Obtiene un examen específico por su identificador y lo convierte a DTO.
        /// </summary>
        /// <param name="id">Identificador único del examen.</param>
        /// <returns>ExamenDTO si existe, null en caso contrario.</returns>
        public async Task<ExamenDTO?> GetByIdAsync(int id)
        {
            const string sql = @"SELECT ExamenID, Titulo, MateriaID FROM Examenes WHERE ExamenID=@id";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", id);
            await cn.OpenAsync();
            using var rd = await cmd.ExecuteReaderAsync();

            if (!await rd.ReadAsync()) return null;

            return ExamenMapper.ToDTO(new Examen
            {
                ExamenID = rd.GetInt32(0),
                Titulo = rd.GetString(1),
                MateriaID = rd.GetInt32(2)
            });
        }

        /// <summary>
        /// Crea un nuevo examen en la base de datos y devuelve su ID generado.
        /// </summary>
        /// <param name="entity">Entidad Examen a insertar.</param>
        /// <returns>Identificador del nuevo examen.</returns>
        public async Task<int> CreateAsync(Examen entity)
        {
            const string sql = @"
                INSERT INTO Examenes (Titulo, MateriaID)
                VALUES (@t, @m);
                SELECT SCOPE_IDENTITY();";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@t", entity.Titulo);
            cmd.Parameters.AddWithValue("@m", entity.MateriaID);
            await cn.OpenAsync();

            return Convert.ToInt32(await cmd.ExecuteScalarAsync());
        }

        /// <summary>
        /// Actualiza un examen existente en la base de datos.
        /// </summary>
        /// <param name="entity">Entidad Examen con los valores actualizados.</param>
        /// <returns>True si la actualización fue exitosa, False en caso contrario.</returns>
        public async Task<bool> UpdateAsync(Examen entity)
        {
            const string sql = @"
                UPDATE Examenes
                SET Titulo=@t, MateriaID=@m
                WHERE ExamenID=@id";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", entity.ExamenID);
            cmd.Parameters.AddWithValue("@t", entity.Titulo);
            cmd.Parameters.AddWithValue("@m", entity.MateriaID);
            await cn.OpenAsync();

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        /// <summary>
        /// Elimina un examen de la base de datos.
        /// </summary>
        /// <param name="id">Identificador único del examen a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = @"DELETE FROM Examenes WHERE ExamenID=@id";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", id);
            await cn.OpenAsync();

            return await cmd.ExecuteNonQueryAsync() > 0;
        }
    }
}
