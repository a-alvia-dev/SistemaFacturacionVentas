using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaFacturacionVentas.Api.Models;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Precio { get; set; }

    public int StockActual { get; set; }
    public bool Activo { get; set; } = true;

    [Timestamp]
    public byte[]? RowVersion { get; set; }
}