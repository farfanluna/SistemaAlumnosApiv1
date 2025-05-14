using SistemaAlumnosApi.Models;
using SistemaAlumnosApi.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaAlumnosApi.DTOs;
using SistemaAlumnosApi.Mappers;

namespace SistemaAlumnosApi.Repositories.Sql
{
    /// <summary>
    /// Repositorio SQL para gestionar las preguntas en la base de datos.
    /// Implementa la interfaz IPreguntaRepository para realizar operaciones CRUD.
    /// </summary>
    public class SqlPreguntaRepository : IPreguntaRepository
    {
        private readonly string _conn;

        /// <summary>
        /// Constructor que inicializa la conexión con la base de datos.
        /// </summary>
        /// <param name="cfg">Configuración utilizada para obtener la cadena de conexión.</param>
        public SqlPreguntaRepository(IConfiguration cfg) =>
            _conn = cfg.GetConnectionString("DefaultConnection")!;

        /// <summary>
        /// Obtiene todas las preguntas almacenadas en la base de datos.
        /// </summary>
        /// <returns>Lista de preguntas en formato DTO.</returns>

        public async Task<IEnumerable<Pregunta>> GetAllAsync()
        {
            var list = new List<Pregunta>();
            const string sql = @"SELECT PreguntaID, Texto, ExamenID FROM Preguntas";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            await cn.OpenAsync();
            using var rd = await cmd.ExecuteReaderAsync();

            while (await rd.ReadAsync())
            {
                var entity = new Pregunta
                {
                    PreguntaID = rd.GetInt32(0),
                    Texto = rd.GetString(1),
                    ExamenID = rd.GetInt32(2)
                };

                list.Add(entity);
            }

            return list;
        }

        /// <summary>
        /// Obtiene una pregunta específica por su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la pregunta.</param>
        /// <returns>Pregunta en formato DTO si existe, null si no se encuentra.</returns>
        public async Task<Pregunta?> GetByIdAsync(int id)
        {
            const string sql = @"SELECT PreguntaID, Texto, ExamenID FROM Preguntas WHERE PreguntaID=@id";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", id);
            await cn.OpenAsync();
            using var rd = await cmd.ExecuteReaderAsync();

            if (!await rd.ReadAsync()) return null;

            var entity = new Pregunta
            {
                PreguntaID = rd.GetInt32(0),
                Texto = rd.GetString(1),
                ExamenID = rd.GetInt32(2)
            };

            return entity;
        }
        /// <summary>
        /// Crea una nueva pregunta en la base de datos y devuelve su ID generado.
        /// </summary>
        /// <param name="dto">DTO con los datos de la nueva pregunta.</param>
        /// <returns>Identificador de la nueva pregunta.</returns>
        public async Task<int> CreateAsync(PreguntaCreateDTO dto)
        {
            var entity = PreguntaMapper.ToEntity(dto);

            const string sql = @"
                INSERT INTO Preguntas (Texto, ExamenID)
                VALUES (@t, @e);
                SELECT SCOPE_IDENTITY();";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@t", entity.Texto);
            cmd.Parameters.AddWithValue("@e", entity.ExamenID);
            await cn.OpenAsync();

            return Convert.ToInt32(await cmd.ExecuteScalarAsync());
        }

        /// <summary>
        /// Actualiza una pregunta existente en la base de datos.
        /// </summary>
        /// <param name="dto">DTO con los datos actualizados de la pregunta.</param>
        /// <returns>True si la actualización fue exitosa, False en caso contrario.</returns>
        public async Task<bool> UpdateAsync(PreguntaUpdateDTO dto)
        {
            var entity = PreguntaMapper.ToEntity(dto);

            const string sql = @"
                UPDATE Preguntas
                SET Texto=@t, ExamenID=@e
                WHERE PreguntaID=@id";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", entity.PreguntaID);
            cmd.Parameters.AddWithValue("@t", entity.Texto);
            cmd.Parameters.AddWithValue("@e", entity.ExamenID);
            await cn.OpenAsync();

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        /// <summary>
        /// Elimina una pregunta de la base de datos.
        /// </summary>
        /// <param name="id">Identificador único de la pregunta a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = @"DELETE FROM Preguntas WHERE PreguntaID=@id";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", id);
            await cn.OpenAsync();

            return await cmd.ExecuteNonQueryAsync() > 0;
        }
    }
}
