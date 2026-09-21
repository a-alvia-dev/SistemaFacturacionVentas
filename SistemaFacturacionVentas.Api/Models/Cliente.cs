namespace SistemaFacturacionVentas.Api.Models;

public class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Documento { get; set; } = string.Empty; // cédula/RUC
    public string? Telefono { get; set; }
    public string? Email { get; set; }
}