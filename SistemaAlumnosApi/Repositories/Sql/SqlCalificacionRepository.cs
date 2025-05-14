using SistemaAlumnosApi.Models;
using SistemaAlumnosApi.Repositories.Interfaces;
using SistemaAlumnosApi.Mappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaAlumnosApi.DTOs;

namespace SistemaAlumnosApi.Repositories.Sql
{
    /// <summary>
    /// Repositorio SQL para la gestión de calificaciones en la base de datos.
    /// Implementa la interfaz ICalificacionRepository para realizar operaciones CRUD.
    /// </summary>
    public class SqlCalificacionRepository : ICalificacionRepository
    {
        private readonly string _conn;

        /// <summary>
        /// Constructor que inicializa la conexión con la base de datos.
        /// </summary>
        /// <param name="cfg">Configuración utilizada para obtener la cadena de conexión.</param>
        public SqlCalificacionRepository(IConfiguration cfg) =>
            _conn = cfg.GetConnectionString("DefaultConnection")!;

        /// <summary>
        /// Obtiene todas las calificaciones almacenadas en la base de datos y las convierte a DTOs.
        /// </summary>
        /// <returns>Lista de calificaciones en formato DTO.</returns>
        public async Task<IEnumerable<CalificacionDTO>> GetAllAsync()
        {
            var list = new List<Calificacion>();

            const string sql = @"SELECT CalificacionID, AlumnoID, ExamenID, Nota FROM Calificaciones";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            await cn.OpenAsync();
            using var rd = await cmd.ExecuteReaderAsync();

            while (await rd.ReadAsync())
            {
                list.Add(new Calificacion
                {
                    CalificacionID = rd.GetInt32(0),
                    AlumnoID = rd.GetInt32(1),
                    ExamenID = rd.GetInt32(2),
                    Nota = (decimal)rd.GetDecimal(3)
                });
            }

            return list.Select(CalificacionMapper.ToDTO);
        }

        /// <summary>
        /// Obtiene una calificación específica por su identificador y la convierte a DTO.
        /// </summary>
        /// <param name="id">Identificador único de la calificación.</param>
        /// <returns>CalificacionDTO si existe, null en caso contrario.</returns>
        public async Task<CalificacionDTO?> GetByIdAsync(int id)
        {
            const string sql = @"SELECT CalificacionID, AlumnoID, ExamenID, Nota FROM Calificaciones WHERE CalificacionID=@id";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", id);
            await cn.OpenAsync();
            using var rd = await cmd.ExecuteReaderAsync();

            if (!await rd.ReadAsync()) return null;

            return CalificacionMapper.ToDTO(new Calificacion
            {
                CalificacionID = rd.GetInt32(0),
                AlumnoID = rd.GetInt32(1),
                ExamenID = rd.GetInt32(2),
                Nota = (decimal)rd.GetDecimal(3)
            });
        }

        /// <summary>
        /// Crea una nueva calificación en la base de datos y devuelve su ID generado.
        /// </summary>
        /// <param name="entity">Entidad Calificacion a insertar.</param>
        /// <returns>Identificador de la nueva calificación.</returns>
        public async Task<int> CreateAsync(Calificacion entity)
        {
            using var cn = new SqlConnection(_conn);
            await cn.OpenAsync();

            // Verificar si ya existe una calificación para ese Alumno y Examen
            const string checkQuery = @"
        SELECT COUNT(*) 
        FROM Calificaciones 
        WHERE AlumnoID = @AlumnoID AND ExamenID = @ExamenID";

            using (var checkCmd = new SqlCommand(checkQuery, cn))
            {
                checkCmd.Parameters.AddWithValue("@AlumnoID", entity.AlumnoID);
                checkCmd.Parameters.AddWithValue("@ExamenID", entity.ExamenID);

                var count = Convert.ToInt32(await checkCmd.ExecuteScalarAsync());

                if (count > 0)
                    throw new InvalidOperationException("El alumno ya tiene una calificación registrada para este examen.");
            }

            // Insertar calificación con OUTPUT INTO para evitar conflicto con triggers
            const string insertQuery = @"
        DECLARE @InsertedIds TABLE (CalificacionID INT);
        INSERT INTO Calificaciones (AlumnoID, ExamenID, Nota)
        OUTPUT INSERTED.CalificacionID INTO @InsertedIds
        VALUES (@AlumnoID, @ExamenID, @Nota);
        SELECT CalificacionID FROM @InsertedIds;";

            using var insertCmd = new SqlCommand(insertQuery, cn);
            insertCmd.Parameters.AddWithValue("@AlumnoID", entity.AlumnoID);
            insertCmd.Parameters.AddWithValue("@ExamenID", entity.ExamenID);
            insertCmd.Parameters.AddWithValue("@Nota", entity.Nota);

            var result = await insertCmd.ExecuteScalarAsync();

            if (result == null || result == DBNull.Value)
                throw new Exception("No se pudo obtener el ID generado para la calificación.");

            return Convert.ToInt32(result);
        }

        /// <summary>
        /// Actualiza una calificación existente en la base de datos.
        /// </summary>
        /// <param name="entity">Entidad Calificacion con los valores actualizados.</param>
        /// <returns>True si la actualización fue exitosa, False en caso contrario.</returns>
        public async Task<bool> UpdateAsync(Calificacion entity)
        {
            const string sql = @"
                UPDATE Calificaciones
                SET AlumnoID=@a, ExamenID=@e, Nota=@n
                WHERE CalificacionID=@id";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", entity.CalificacionID);
            cmd.Parameters.AddWithValue("@a", entity.AlumnoID);
            cmd.Parameters.AddWithValue("@e", entity.ExamenID);
            cmd.Parameters.AddWithValue("@n", entity.Nota);
            await cn.OpenAsync();

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        /// <summary>
        /// Elimina una calificación de la base de datos.
        /// </summary>
        /// <param name="id">Identificador único de la calificación a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = @"DELETE FROM Calificaciones WHERE CalificacionID=@id";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", id);
            await cn.OpenAsync();

            return await cmd.ExecuteNonQueryAsync() > 0;
        }
    }
}
