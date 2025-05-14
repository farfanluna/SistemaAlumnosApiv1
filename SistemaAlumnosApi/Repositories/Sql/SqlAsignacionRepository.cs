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
    /// Repositorio SQL para la gestión de asignaciones en la base de datos.
    /// Implementa la interfaz IAsignacionRepository para realizar operaciones CRUD.
    /// </summary>
    public class SqlAsignacionRepository : IAsignacionRepository
    {
        private readonly string _conn;

        /// <summary>
        /// Constructor que inicializa la conexión con la base de datos.
        /// </summary>
        /// <param name="cfg">Configuración utilizada para obtener la cadena de conexión.</param>
        public SqlAsignacionRepository(IConfiguration cfg) =>
            _conn = cfg.GetConnectionString("DefaultConnection")!;

        /// <summary>
        /// Obtiene todas las asignaciones almacenadas en la base de datos.
        /// </summary>
        /// <returns>Lista de asignaciones.</returns>
        public async Task<IEnumerable<Asignacion>> GetAllAsync()
        {
            var list = new List<Asignacion>();
            const string sql = @"SELECT AsignacionID, AlumnoID, MateriaID FROM Asignaciones";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            await cn.OpenAsync();
            using var rd = await cmd.ExecuteReaderAsync();

            while (await rd.ReadAsync())
            {
                list.Add(new Asignacion
                {
                    AsignacionID = rd.GetInt32(0),
                    AlumnoID = rd.GetInt32(1),
                    MateriaID = rd.GetInt32(2)
                });
            }

            return list;
        }

        /// <summary>
        /// Obtiene una asignación específica por su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la asignación.</param>
        /// <returns>Instancia de Asignacion si existe, null si no se encuentra.</returns>
        public async Task<Asignacion?> GetByIdAsync(int id)
        {
            const string sql = @"SELECT AsignacionID, AlumnoID, MateriaID FROM Asignaciones WHERE AsignacionID=@id";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", id);
            await cn.OpenAsync();
            using var rd = await cmd.ExecuteReaderAsync();

            if (!await rd.ReadAsync()) return null;

            return new Asignacion
            {
                AsignacionID = rd.GetInt32(0),
                AlumnoID = rd.GetInt32(1),
                MateriaID = rd.GetInt32(2)
            };
        }

        /// <summary>
        /// Crea una nueva asignación en la base de datos y devuelve su ID generado.
        /// </summary>
        /// <param name="dto">Entidad Asignacion a insertar.</param>
        /// <returns>Identificador de la nueva asignación.</returns>
        public async Task<int> CreateAsync(Asignacion dto)
        {
            const string sql = @"
                INSERT INTO Asignaciones (AlumnoID, MateriaID)
                VALUES (@a, @m);
                SELECT SCOPE_IDENTITY();";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@a", dto.AlumnoID);
            cmd.Parameters.AddWithValue("@m", dto.MateriaID);
            await cn.OpenAsync();

            return Convert.ToInt32(await cmd.ExecuteScalarAsync());
        }

        /// <summary>
        /// Actualiza una asignación existente en la base de datos.
        /// </summary>
        /// <param name="dto">Entidad Asignacion con los valores actualizados.</param>
        /// <returns>True si la actualización fue exitosa, False en caso contrario.</returns>
        public async Task<bool> UpdateAsync(Asignacion dto)
        {
            const string sql = @"
                UPDATE Asignaciones
                SET AlumnoID=@a, MateriaID=@m
                WHERE AsignacionID=@id";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", dto.AsignacionID);
            cmd.Parameters.AddWithValue("@a", dto.AlumnoID);
            cmd.Parameters.AddWithValue("@m", dto.MateriaID);
            await cn.OpenAsync();

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        /// <summary>
        /// Elimina una asignación de la base de datos.
        /// </summary>
        /// <param name="id">Identificador único de la asignación a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = @"DELETE FROM Asignaciones WHERE AsignacionID=@id";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", id);
            await cn.OpenAsync();

            return await cmd.ExecuteNonQueryAsync() > 0;
        }
    }
}
