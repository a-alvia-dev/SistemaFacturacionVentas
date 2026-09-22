namespace SistemaFacturacionVentas.Api.DTOs;

public class RegistroDto
{
    public string NombreUsuario { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Rol { get; set; } = "Vendedor";
}

public class LoginDto
{
    public string NombreUsuario { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class UsuarioResumenDto
{
    public int Id { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public string Rol { get; set; } = string.Empty;
    public bool Activo { get; set; }
}

public class ActualizarUsuarioDto
{
    public string Rol { get; set; } = string.Empty;
    public bool Activo { get; set; }
}