namespace SistemaFacturacionVentas.Api.Models;

public class Venta
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }
    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
    public decimal Total { get; set; }
    public string Estado { get; set; } = "Registrada";
    public List<DetalleVenta> Detalles { get; set; } = new();
}