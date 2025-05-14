using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAlumnosApi.Migrations
{
    /// <inheritdoc />
    public partial class AddStoredProceduresAndTriggers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // —— Stored Procedures —— //

            /// <summary>
            /// Procedimiento almacenado para obtener el promedio de un alumno
            /// a partir de sus calificaciones registradas.
            /// </summary>
            migrationBuilder.Sql(@"
                    CREATE PROCEDURE ObtenerPromedioPorAlumno
                        @AlumnoID INT
                    AS
                    BEGIN
                        SELECT 
                            A.AlumnoID,
                            A.Nombre,
                            AVG(C.Nota) AS Promedio
                        FROM Calificaciones C
                        INNER JOIN Alumnos A ON A.AlumnoID = C.AlumnoID
                        WHERE C.AlumnoID = @AlumnoID
                        GROUP BY A.AlumnoID, A.Nombre;
                    END
                ");

            /// <summary>
            /// Procedimiento almacenado para generar un reporte de calificaciones
            /// agrupadas por materia y alumno.
            /// </summary>
            migrationBuilder.Sql(@"
                    CREATE PROCEDURE ReporteCalificacionesPorMateria
                    AS
                    BEGIN
                        SELECT 
                            M.Nombre AS Materia,
                            A.Nombre AS Alumno,
                            E.Titulo AS Examen,
                            C.Nota
                        FROM Calificaciones C
                        INNER JOIN Alumnos A ON C.AlumnoID = A.AlumnoID
                        INNER JOIN Examenes E ON C.ExamenID = E.ExamenID
                        INNER JOIN Materias M ON E.MateriaID = M.MateriaID
                        ORDER BY M.Nombre, A.Nombre;
                    END
                ");

            /// <summary>
            /// Procedimiento almacenado para obtener el historial de exámenes
            /// presentados por un alumno.
            /// </summary>
            migrationBuilder.Sql(@"
                    CREATE PROCEDURE HistorialExamenesAlumno
                        @AlumnoID INT
                    AS
                    BEGIN
                        SELECT 
                            A.Nombre AS Alumno,
                            M.Nombre AS Materia,
                            E.Titulo AS Examen,
                            E.FechaAplicacion,
                            C.Nota
                        FROM Calificaciones C
                        INNER JOIN Examenes E ON C.ExamenID = E.ExamenID
                        INNER JOIN Materias M ON E.MateriaID = M.MateriaID
                        INNER JOIN Alumnos A ON C.AlumnoID = A.AlumnoID
                        WHERE A.AlumnoID = @AlumnoID
                        ORDER BY E.FechaAplicacion DESC;
                    END
                ");

            /// <summary>
            /// Procedimiento almacenado para obtener el listado de los mejores alumnos
            /// basado en su promedio de calificaciones.
            /// </summary>
            migrationBuilder.Sql(@"
                    CREATE PROCEDURE TopAlumnosPorPromedio
                        @Top INT
                    AS
                    BEGIN
                        SELECT TOP(@Top)
                            A.AlumnoID,
                            A.Nombre,
                            AVG(C.Nota) AS Promedio
                        FROM Calificaciones C
                        JOIN Alumnos A ON A.AlumnoID = C.AlumnoID
                        GROUP BY A.AlumnoID, A.Nombre
                        ORDER BY Promedio DESC;
                    END
                ");

            /// <summary>
            /// Procedimiento almacenado para obtener estadísticas de un examen,
            /// incluyendo promedio, máxima y mínima puntuación.
            /// </summary>
            migrationBuilder.Sql(@"
                    CREATE PROCEDURE EstadisticasExamen
                        @ExamenID INT
                    AS
                    BEGIN
                        SELECT 
                            COUNT(*)            AS Total,
                            AVG(Nota)     AS Promedio,
                            MIN(Nota)     AS Minima,
                            MAX(Nota)     AS Maxima,
                            STDEV(Nota)   AS DesviacionEstandar
                        FROM Calificaciones
                        WHERE ExamenID = @ExamenID;
                    END
                ");

        

            // —— Auditoría y seguridad —— //

            /// <summary>
            /// Tabla de auditoría para registrar operaciones CRUD en la tabla Calificaciones.
            /// </summary>
            migrationBuilder.Sql(@"
                    CREATE TABLE Audit_Calificaciones (
                        AuditID        INT IDENTITY(1,1) PRIMARY KEY,
                        CalificacionID INT,
                        AlumnoID       INT,
                        ExamenID       INT,
                        Nota     DECIMAL(5,2),
                        Operacion      VARCHAR(10),
                        FechaOperacion DATETIME DEFAULT GETDATE()
                    );
                ");

            /// <summary>
            /// Trigger para auditar operaciones realizadas en la tabla Calificaciones.
            /// </summary>
            migrationBuilder.Sql(@"
                    CREATE TRIGGER trg_AuditCalificaciones
                    ON Calificaciones
                    AFTER INSERT, UPDATE, DELETE
                    AS
                    BEGIN
                        SET NOCOUNT ON;
                        INSERT INTO Audit_Calificaciones(CalificacionID, AlumnoID, ExamenID, Nota, Operacion)
                        SELECT CalificacionID, AlumnoID, ExamenID, Nota, 'INSERT' FROM inserted;

                        INSERT INTO Audit_Calificaciones(CalificacionID, AlumnoID, ExamenID, Nota, Operacion)
                        SELECT CalificacionID, AlumnoID, ExamenID, Nota, 'UPDATE' FROM inserted
                        WHERE EXISTS (SELECT 1 FROM deleted WHERE deleted.CalificacionID = inserted.CalificacionID);

                        INSERT INTO Audit_Calificaciones(CalificacionID, AlumnoID, ExamenID, Nota, Operacion)
                        SELECT CalificacionID, AlumnoID, ExamenID, Nota, 'DELETE' FROM deleted;
                    END
                ");

            /// <summary>
            /// Trigger para evitar que se elimine un alumno si tiene calificaciones registradas.
            /// </summary>
            migrationBuilder.Sql(@"
                    CREATE TRIGGER trg_PreventDeleteAlumno
                    ON Alumnos
                    INSTEAD OF DELETE
                    AS
                    BEGIN
                        IF EXISTS (
                            SELECT 1 FROM Calificaciones C
                            JOIN deleted d ON C.AlumnoID = d.AlumnoID
                        )
                        BEGIN
                            RAISERROR('No se puede eliminar el alumno porque tiene calificaciones registradas.', 16, 1);
                        END
                        ELSE
                        BEGIN
                            DELETE FROM Alumnos WHERE AlumnoID IN (SELECT AlumnoID FROM deleted);
                        END
                    END
                ");

    

            /// <summary>
            /// Trigger para evitar la inserción de una calificación duplicada para un mismo examen.
            /// </summary>
            migrationBuilder.Sql(@"
                    CREATE TRIGGER trg_VerificarUnicaCalificacion
                    ON Calificaciones
                    INSTEAD OF INSERT
                    AS
                    BEGIN
                        IF EXISTS (
                            SELECT 1 FROM Calificaciones C
                            INNER JOIN inserted I ON C.AlumnoID = I.AlumnoID AND C.ExamenID  = I.ExamenID
                        )
                        BEGIN
                            RAISERROR('El alumno ya tiene una calificación registrada para este examen.', 16, 1);
                        END
                        ELSE
                        BEGIN
                            INSERT INTO Calificaciones (ExamenID, AlumnoID, Nota)
                            SELECT ExamenID, AlumnoID, Nota FROM inserted;
                        END
                    END
                ");



        }

        /// <inheritdoc />
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /// <summary>
            /// Eliminación del trigger que evita la duplicación de calificaciones.
            /// </summary>
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_VerificarUnicaCalificacion;");

            /// <summary>
            /// Eliminación del trigger que previene la eliminación de alumnos con calificaciones registradas.
            /// </summary>
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_PreventDeleteAlumno;");

            /// <summary>
            /// Eliminación del trigger de auditoría que registra cambios en la tabla de calificaciones.
            /// </summary>
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_AuditCalificaciones;");

            /// <summary>
            /// Eliminación de la tabla de auditoría de calificaciones.
            /// </summary>
            migrationBuilder.Sql("DROP TABLE IF EXISTS Audit_Calificaciones;");

           

            /// <summary>
            /// Eliminación del procedimiento almacenado que calcula estadísticas de un examen.
            /// </summary>
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS EstadisticasExamen;");

            /// <summary>
            /// Eliminación del procedimiento almacenado que obtiene el top de alumnos por promedio.
            /// </summary>
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS TopAlumnosPorPromedio;");

            /// <summary>
            /// Eliminación del procedimiento almacenado que obtiene el historial de exámenes de un alumno.
            /// </summary>
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS HistorialExamenesAlumno;");

            /// <summary>
            /// Eliminación del procedimiento almacenado para generar un reporte de calificaciones por materia.
            /// </summary>
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS ReporteCalificacionesPorMateria;");

            /// <summary>
            /// Eliminación del procedimiento almacenado para calcular el promedio de calificaciones de un alumno.
            /// </summary>
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS ObtenerPromedioPorAlumno;");
        }


    }
}
