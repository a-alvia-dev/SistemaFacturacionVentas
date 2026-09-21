namespace SistemaFacturacionVentas.Api.Models;

public class Usuario
{
    public int Id { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Rol { get; set; } = "Vendedor"; // "Admin" o "Vendedor"
    public bool Activo { get; set; } = true;
}