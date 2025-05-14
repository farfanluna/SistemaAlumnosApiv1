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
        /// <param name="cfg">Configuración utilizada para obtener la cadena de conexión.</param>
        public SqlAlumnoRepository(IConfiguration cfg) =>
            _conn = cfg.GetConnectionString("DefaultConnection")!;

        /// <summary>
        /// Obtiene todos los alumnos almacenados en la base de datos.
        /// </summary>
        /// <returns>Lista de alumnos.</returns>
        public async Task<IEnumerable<AlumnoDTO>> GetAllAsync()
        {
            var alumnos = new List<Alumno>();
            const string sql = @"SELECT AlumnoID, Nombre, Email, Creditos FROM Alumnos";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            await cn.OpenAsync();
            using var rd = await cmd.ExecuteReaderAsync();

            while (await rd.ReadAsync())
            {
                alumnos.Add(new Alumno
                {
                    AlumnoID = rd.GetInt32(0),
                    Nombre = rd.GetString(1),
                    Email = rd.GetString(2),
                    Creditos = rd.GetInt32(3)
                });
            }

            return alumnos.Select(AlumnoMapper.ToDTO);
        }
        /// <summary>
        /// Obtiene un alumno por su correo electrónico.
        /// </summary>
        /// <param name="email">Correo electrónico del alumno.</param>
        /// <returns>Instancia del alumno si existe, null en caso contrario.</returns>
        public async Task<Alumno?> GetByEmailAsync(string email)
        {
            const string sql = @"
              SELECT AlumnoID, Nombre, Edad, Email, Password, Creditos 
              FROM Alumnos WHERE Email=@e";
            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@e", email);
            await cn.OpenAsync();
            using var rd = await cmd.ExecuteReaderAsync();
            if (!await rd.ReadAsync()) return null;
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
        /// <param name="id">Identificador único del alumno.</param>
        /// <returns>Instancia de Alumno si existe, null si no se encuentra.</returns>
        public async Task<AlumnoDTO?> GetByIdAsync(int id)
        {
            const string sql = @"SELECT AlumnoID, Nombre, Email, Creditos FROM Alumnos WHERE AlumnoID=@id";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", id);
            await cn.OpenAsync();
            using var rd = await cmd.ExecuteReaderAsync();

            if (!await rd.ReadAsync()) return null;

            // 📌 Convertimos la entidad Alumno a DTO usando AlumnoMapper
            return AlumnoMapper.ToDTO(new Alumno
            {
                AlumnoID = rd.GetInt32(0),
                Nombre = rd.GetString(1),
                Email = rd.GetString(2),
                Creditos = rd.GetInt32(3)
            });
        }

        /// <summary>
        /// Crea un nuevo alumno en la base de datos y devuelve su ID generado.
        /// </summary>
        /// <param name="dto">Entidad Alumno a insertar.</param>
        /// <returns>Identificador del nuevo alumno.</returns>
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
        /// <param name="dto">Entidad Alumno con los valores actualizados.</param>
        /// <returns>True si la actualización fue exitosa, False en caso contrario.</returns>
        public async Task<bool> UpdateAsync(Alumno dto)
        {
            const string sql = @"
                UPDATE Alumnos
                SET Nombre=@n, Edad=@e, Email=@m, Password=@p, Creditos=@c
                WHERE AlumnoID=@id";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", dto.AlumnoID);
            cmd.Parameters.AddWithValue("@n", dto.Nombre);
            cmd.Parameters.AddWithValue("@e", dto.Edad);
            cmd.Parameters.AddWithValue("@m", dto.Email);
            cmd.Parameters.AddWithValue("@p", dto.Password);
            cmd.Parameters.AddWithValue("@c", dto.Creditos);
            await cn.OpenAsync();

            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        /// <summary>
        /// Elimina un alumno de la base de datos.
        /// </summary>
        /// <param name="id">Identificador único del alumno a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = @"DELETE FROM Alumnos WHERE AlumnoID=@id";

            using var cn = new SqlConnection(_conn);
            using var cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", id);
            await cn.OpenAsync();

            return await cmd.ExecuteNonQueryAsync() > 0;
        }
    }
}
