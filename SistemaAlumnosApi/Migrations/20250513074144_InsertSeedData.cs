using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAlumnosApi.Migrations
{
    /// <inheritdoc />
    public partial class InsertSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Datos de prueba para Alumnos
            migrationBuilder.Sql("INSERT INTO Alumnos (Nombre, Edad, Email, Password, Creditos) VALUES ('Ana López', 21, 'ana@example.com', 'pass123', 28);");
            migrationBuilder.Sql("INSERT INTO Alumnos (Nombre, Edad, Email, Password, Creditos) VALUES ('Luis Pérez', 22, 'luis@example.com', 'secure456', 30);");

            // Datos de prueba para Materias
            migrationBuilder.Sql("INSERT INTO Materias (Nombre, Creditos) VALUES ('Matemáticas', 6);");
            migrationBuilder.Sql("INSERT INTO Materias (Nombre, Creditos) VALUES ('Programación', 8);");

            // Asignaciones
            migrationBuilder.Sql("INSERT INTO Asignaciones (AlumnoID, MateriaID) VALUES (1, 1);"); // Ana - Matemáticas
            migrationBuilder.Sql("INSERT INTO Asignaciones (AlumnoID, MateriaID) VALUES (1, 2);"); // Ana - Programación
            migrationBuilder.Sql("INSERT INTO Asignaciones (AlumnoID, MateriaID) VALUES (2, 2);"); // Luis - Programación

            // Exámenes
            migrationBuilder.Sql("INSERT INTO Examenes (Titulo, MateriaID, FechaAplicacion) VALUES ('Parcial 1', 1, '2025-06-01');");
            migrationBuilder.Sql("INSERT INTO Examenes (Titulo, MateriaID, FechaAplicacion) VALUES ('Parcial 1', 2, '2025-06-05');");

            // Calificaciones
            migrationBuilder.Sql("INSERT INTO Calificaciones (AlumnoID, ExamenID, Nota) VALUES (1, 1, 8.5);");
            migrationBuilder.Sql("INSERT INTO Calificaciones (AlumnoID, ExamenID, Nota) VALUES (1, 2, 9.0);");
            migrationBuilder.Sql("INSERT INTO Calificaciones (AlumnoID, ExamenID, Nota) VALUES (2, 2, 7.5);");

            // Preguntas y Respuestas
            migrationBuilder.Sql("INSERT INTO Preguntas (Texto, ExamenID) VALUES ('¿Cuál es el resultado de 2+2?', 1);");
            migrationBuilder.Sql("INSERT INTO Respuestas (Texto, EsCorrecta, PreguntaID) VALUES ('4', 1, 1);");
            migrationBuilder.Sql("INSERT INTO Respuestas (Texto, EsCorrecta, PreguntaID) VALUES ('5', 0, 1);");

            migrationBuilder.Sql("INSERT INTO Preguntas (Texto, ExamenID) VALUES ('¿Qué es una variable en programación?', 2);");
            migrationBuilder.Sql("INSERT INTO Respuestas (Texto, EsCorrecta, PreguntaID) VALUES ('Un espacio para almacenar datos', 1, 2);");
            migrationBuilder.Sql("INSERT INTO Respuestas (Texto, EsCorrecta, PreguntaID) VALUES ('Un tipo de función', 0, 2);");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
