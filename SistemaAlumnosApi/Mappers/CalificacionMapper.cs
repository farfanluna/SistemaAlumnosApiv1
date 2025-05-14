using SistemaAlumnosApi.DTOs;
using SistemaAlumnosApi.Models;

public static class CalificacionMapper
{
    public static CalificacionDTO ToDTO(Calificacion entity) => new CalificacionDTO
    {
        CalificacionID = entity.CalificacionID,
        AlumnoID = entity.AlumnoID,
        ExamenID = entity.ExamenID,
        Nota = entity.Nota
    };

    public static Calificacion ToEntity(CalificacionDTO dto) => new Calificacion
    {
        CalificacionID = dto.CalificacionID,
        AlumnoID = dto.AlumnoID,
        ExamenID = dto.ExamenID,
        Nota = dto.Nota
    };

    // 🔹 Agregar este método para convertir `CalificacionCreateDTO` a `Calificacion`
    public static Calificacion ToEntity(CalificacionCreateDTO dto) => new Calificacion
    {
        AlumnoID = dto.AlumnoID,
        ExamenID = dto.ExamenID,
        Nota = dto.Nota
    };
    public static Calificacion ToEntity(CalificacionUpdateDTO dto) => new Calificacion
    {
        CalificacionID = dto.CalificacionID,
        AlumnoID = dto.AlumnoID,
        ExamenID = dto.ExamenID,
        Nota = dto.Nota
    };

}
