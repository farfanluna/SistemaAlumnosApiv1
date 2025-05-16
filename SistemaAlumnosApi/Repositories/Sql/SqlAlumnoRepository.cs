using SistemaAlumnosApi.Models;
using SistemaAlumnosApi.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaAlumnosApi.Mappers;

namespace SistemaAlumnosApi.Repositories.Sql
{
    /// <summary>
    /// Repositorio SQL para la gestión de alumnos en la base de datos.
    /// Implementa la interfaz IAlumnoRepository para realizar operaciones CRUD.
    /// </summary>
    public class SqlAlumnoRepository : IAlumnoRepository
    {
        private readonly string _conn;

        /// <summary>
        /// Constructor que inicializa la conexión con la base de datos.
        /// </summary>
        public SqlAlumnoRepository(IConfiguration cfg) =>
            _conn = cfg.GetConnectionString("DefaultConnection")!;

        /// <summary>
        /// Obtiene todos los alumnos almacenados en la base de datos.
        /// </summary>
        public async Task<IEnumerable<AlumnoDTO>> GetAllAsync()
        {
            var alumnos = new List<Alumno>();

            // ← Incluimos Edad en la consulta, en el orden correcto
            const string sql = @"
                SELECT AlumnoID,
                       Nombre,
                       Edad,
                       Email,
                       Creditos
                  FROM Alumnos";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            await cn.OpenAsync();
            using var rd = await cmd.ExecuteReaderAsync();

            while (await rd.ReadAsync())
            {
                alumnos.Add(new Alumno
                {
                    AlumnoID = rd.GetInt32(0),   // AlumnoID
                    Nombre = rd.GetString(1),  // Nombre
                    Edad = rd.GetInt32(2),   // Edad
                    Email = rd.GetString(3),  // Email
                    Creditos = rd.GetInt32(4)    // Creditos
                });
            }

            // Convertimos la lista de entidades a DTOs
            return alumnos.Select(AlumnoMapper.ToDTO);
        }

        /// <summary>
        /// Obtiene un alumno por su correo electrónico.
        /// </summary>
        public async Task<Alumno?> GetByEmailAsync(string email)
        {
            const string sql = @"
                SELECT AlumnoID,
                       Nombre,
                       Edad,
                       Email,
                       Password,
                       Creditos
                  FROM Alumnos
                 WHERE Email = @e";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@e", email);
            await cn.OpenAsync();
            using var rd = await cmd.ExecuteReaderAsync();

            if (!await rd.ReadAsync())
                return null;

            return new Alumno
            {
                AlumnoID = rd.GetInt32(0),
                Nombre = rd.GetString(1),
                Edad = rd.GetInt32(2),
                Email = rd.GetString(3),
                Password = rd.GetString(4),
                Creditos = rd.GetInt32(5)
            };
        }

        /// <summary>
        /// Obtiene un alumno específico por su identificador.
        /// </summary>
        public async Task<AlumnoDTO?> GetByIdAsync(int id)
        {
            // ← Incluimos Edad en el SELECT y en el orden correcto
            const string sql = @"
                SELECT AlumnoID,
                       Nombre,
                       Edad,
                       Email,
                       Creditos
                  FROM Alumnos
                 WHERE AlumnoID = @id";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", id);
            await cn.OpenAsync();
            using var rd = await cmd.ExecuteReaderAsync();

            if (!await rd.ReadAsync())
                return null;

            var entidad = new Alumno
            {
                AlumnoID = rd.GetInt32(0),
                Nombre = rd.GetString(1),
                Edad = rd.GetInt32(2),
                Email = rd.GetString(3),
                Creditos = rd.GetInt32(4)
            };

            return AlumnoMapper.ToDTO(entidad);
        }

        /// <summary>
        /// Crea un nuevo alumno en la base de datos y devuelve su ID generado.
        /// </summary>
        public async Task<int> CreateAsync(Alumno dto)
        {
            const string sql = @"
                INSERT INTO Alumnos (Nombre, Edad, Email, Password, Creditos)
                VALUES (@n, @e, @m, @p, @c);
                SELECT SCOPE_IDENTITY();";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@n", dto.Nombre);
            cmd.Parameters.AddWithValue("@e", dto.Edad);
            cmd.Parameters.AddWithValue("@m", dto.Email);
            cmd.Parameters.AddWithValue("@p", dto.Password);
            cmd.Parameters.AddWithValue("@c", dto.Creditos);

            await cn.OpenAsync();
            return Convert.ToInt32(await cmd.ExecuteScalarAsync());
        }

        /// <summary>
        /// Actualiza un alumno existente en la base de datos.
        /// </summary>
        public async Task<bool> UpdateAsync(Alumno dto)
        {
            bool cambiaPwd = !string.IsNullOrWhiteSpace(dto.Password);

            // Si no cambiamos password, no lo incluimos en el SET
            string sql = cambiaPwd
                ? @"
                    UPDATE Alumnos
                       SET Nombre   = @n,
                           Edad     = @e,
                           Email    = @m,
                           Password = @p,
                           Creditos = @c
                     WHERE AlumnoID = @id"
                : @"
                    UPDATE Alumnos
                       SET Nombre   = @n,
                           Edad     = @e,
                           Email    = @m,
                           Creditos = @c
                     WHERE AlumnoID = @id";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", dto.AlumnoID);
            cmd.Parameters.AddWithValue("@n", dto.Nombre);
            cmd.Parameters.AddWithValue("@e", dto.Edad);
            cmd.Parameters.AddWithValue("@m", dto.Email);
            cmd.Parameters.AddWithValue("@c", dto.Creditos);

            if (cambiaPwd)
                cmd.Parameters.AddWithValue("@p", dto.Password!);

            await cn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        /// <summary>
        /// Elimina un alumno de la base de datos.
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = @"DELETE FROM Alumnos WHERE AlumnoID = @id";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }
    }
}
