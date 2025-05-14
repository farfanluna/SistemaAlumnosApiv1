using Microsoft.EntityFrameworkCore;
using SistemaAlumnosApi.Models;

namespace SistemaAlumnosApi.Data
{
    /// <summary>
    /// Contexto de la base de datos para la aplicación.
    /// Maneja la conexión con la base de datos y la configuración de las entidades.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Constructor que inicializa el contexto con las opciones de configuración.
        /// </summary>
        /// <param name="options">Opciones de configuración del contexto.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Conjunto de datos de alumnos en la base de datos.
        /// </summary>
        public DbSet<Alumno> Alumnos { get; set; }

        /// <summary>
        /// Conjunto de datos de materias en la base de datos.
        /// </summary>
        public DbSet<Materia> Materias { get; set; }

        /// <summary>
        /// Conjunto de datos de asignaciones entre alumnos y materias.
        /// </summary>
        public DbSet<Asignacion> Asignaciones { get; set; }

        /// <summary>
        /// Conjunto de datos de exámenes en la base de datos.
        /// </summary>
        public DbSet<Examen> Examenes { get; set; }

        /// <summary>
        /// Conjunto de datos de preguntas de los exámenes.
        /// </summary>
        public DbSet<Pregunta> Preguntas { get; set; }

        /// <summary>
        /// Conjunto de datos de respuestas de las preguntas.
        /// </summary>
        public DbSet<Respuesta> Respuestas { get; set; }

        /// <summary>
        /// Conjunto de datos de calificaciones obtenidas por los alumnos.
        /// </summary>
        public DbSet<Calificacion> Calificaciones { get; set; }

        /// <summary>
        /// Configura las relaciones entre las entidades de la base de datos.
        /// </summary>
        /// <param name="modelBuilder">Instancia del ModelBuilder utilizada para definir las relaciones.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación: Un alumno puede tener muchas asignaciones.
            modelBuilder.Entity<Asignacion>()
                .HasOne(a => a.Alumno)
                .WithMany(b => b.Asignaciones)
                .HasForeignKey(a => a.AlumnoID);

            // Relación: Una materia puede tener muchas asignaciones.
            modelBuilder.Entity<Asignacion>()
                .HasOne(a => a.Materia)
                .WithMany(b => b.Asignaciones)
                .HasForeignKey(a => a.MateriaID);

            // Relación: Una materia puede tener múltiples exámenes.
            modelBuilder.Entity<Examen>()
                .HasOne(e => e.Materia)
                .WithMany(m => m.Examenes)
                .HasForeignKey(e => e.MateriaID);

            // Relación: Un examen puede tener muchas preguntas.
            modelBuilder.Entity<Pregunta>()
                .HasOne(p => p.Examen)
                .WithMany(e => e.Preguntas)
                .HasForeignKey(p => p.ExamenID);

            // Relación: Una pregunta puede tener múltiples respuestas.
            modelBuilder.Entity<Respuesta>()
                .HasOne(r => r.Pregunta)
                .WithMany(p => p.Respuestas)
                .HasForeignKey(r => r.PreguntaID);

            // Relación: Un alumno puede tener múltiples calificaciones.
            modelBuilder.Entity<Calificacion>()
                .HasOne(c => c.Alumno)
                .WithMany(a => a.Calificaciones)
                .HasForeignKey(c => c.AlumnoID);

            // Relación: Un examen puede tener múltiples calificaciones.
            modelBuilder.Entity<Calificacion>()
                .HasOne(c => c.Examen)
                .WithMany(e => e.Calificaciones)
                .HasForeignKey(c => c.ExamenID);
        }
    }
}
