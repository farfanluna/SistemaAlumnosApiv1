using System.ComponentModel.DataAnnotations;

/// <summary>
/// Representa una solicitud de inicio de sesión en el sistema.
/// Contiene los datos necesarios para autenticar a un usuario.
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Dirección de correo electrónico del usuario.
    /// Debe ser un formato de email válido y no puede estar vacío.
    /// </summary>
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty; // Inicialización para evitar valores nulos

    /// <summary>
    /// Contraseña del usuario.
    /// Es un campo obligatorio y no puede estar vacío.
    /// </summary>
    [Required]
    public string Password { get; set; } = string.Empty; // Inicialización para evitar valores nulos
}
