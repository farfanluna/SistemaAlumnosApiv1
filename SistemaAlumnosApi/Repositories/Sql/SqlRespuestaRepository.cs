using SistemaAlumnosApi.Models;
using SistemaAlumnosApi.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAlumnosApi.Repositories.Sql
{
    /// <summary>
    /// Repositorio SQL para la gestión de respuestas en la base de datos.
    /// Implementa la interfaz IRespuestaRepository para operaciones CRUD.
    /// </summary>
    public class SqlRespuestaRepository : IRespuestaRepository
    {
        private readonly string _conn;

        /// <summary>
        /// Constructor que inicializa la conexión a la base de datos.
        /// </summary>
        /// <param name="cfg">Configuración para obtener la cadena de conexión.</param>
        public SqlRespuestaRepository(IConfiguration cfg) =>
            _conn = cfg.GetConnectionString("DefaultConnection")!;

        /// <summary>
        /// Obtiene todas las respuestas almacenadas en la base de datos.
        /// </summary>
        /// <returns>Lista de respuestas.</returns>
        public async Task<IEnumerable<Respuesta>> GetAllAsync()
        {
            var list = new List<Respuesta>();
            const string sql = @"SELECT RespuestaID, Texto, EsCorrecta, PreguntaID FROM Respuestas";
            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            await cn.OpenAsync();
            using var rd = await cmd.ExecuteReaderAsync();

            while (await rd.ReadAsync())
            {
                list.Add(new Respuesta
                {
                    RespuestaID = rd.GetInt32(0),
                    Texto = rd.GetString(1),
                    EsCorrecta = rd.GetBoolean(2),
                    PreguntaID = rd.GetInt32(3)
                });
            }
            return list;
        }

        /// <summary>
        /// Obtiene una respuesta específica por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la respuesta.</param>
        /// <returns>Instancia de Respuesta si existe, de lo contrario null.</returns>
        public async Task<Respuesta?> GetByIdAsync(int id)
        {
            const string sql = @"SELECT RespuestaID, Texto, EsCorrecta, PreguntaID FROM Respuestas WHERE RespuestaID=@id";
            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", id);
            await cn.OpenAsync();
            using var rd = await cmd.ExecuteReaderAsync();
            if (!await rd.ReadAsync()) return null;

            return new Respuesta
            {
                RespuestaID = rd.GetInt32(0),
                Texto = rd.GetString(1),
                EsCorrecta = rd.GetBoolean(2),
                PreguntaID = rd.GetInt32(3)
            };
        }

        /// <summary>
        /// Crea una nueva respuesta en la base de datos y devuelve su ID generado.
        /// </summary>
        /// <param name="entity">Entidad Respuesta a insertar.</param>
        /// <returns>Identificador de la nueva respuesta.</returns>
        public async Task<int> CreateAsync(Respuesta entity)
        {
            const string sql = @"
                INSERT INTO Respuestas (Texto, EsCorrecta, PreguntaID)
                VALUES (@t, @c, @p);
                SELECT SCOPE_IDENTITY();";
            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@t", entity.Texto);
            cmd.Parameters.AddWithValue("@c", entity.EsCorrecta);
            cmd.Parameters.AddWithValue("@p", entity.PreguntaID);
            await cn.OpenAsync();
            return Convert.ToInt32(await cmd.ExecuteScalarAsync());
        }

        /// <summary>
        /// Actualiza una respuesta existente en la base de datos.
        /// </summary>
        /// <param name="entity">Entidad Respuesta con los valores actualizados.</param>
        /// <returns>True si la actualización fue exitosa, False en caso contrario.</returns>
        public async Task<bool> UpdateAsync(Respuesta entity)
        {
            const string sql = @"
                UPDATE Respuestas
                SET Texto=@t, EsCorrecta=@c, PreguntaID=@p
                WHERE RespuestaID=@id";
            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", entity.RespuestaID);
            cmd.Parameters.AddWithValue("@t", entity.Texto);
            cmd.Parameters.AddWithValue("@c", entity.EsCorrecta);
            cmd.Parameters.AddWithValue("@p", entity.PreguntaID);
            await cn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        /// <summary>
        /// Elimina una respuesta de la base de datos.
        /// </summary>
        /// <param name="id">Identificador de la respuesta a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = @"DELETE FROM Respuestas WHERE RespuestaID=@id";
            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", id);
            await cn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }
    }
}
