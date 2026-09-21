using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaFacturacionVentas.Api.Models;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Precio { get; set; }

    public int StockActual { get; set; }
    public bool Activo { get; set; } = true;
}