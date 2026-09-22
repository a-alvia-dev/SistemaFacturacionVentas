namespace SistemaFacturacionVentas.Api.DTOs;

public class DetalleVentaDto
{
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
}

public class CrearVentaDto
{
    public int ClienteId { get; set; }
    public List<DetalleVentaDto> Detalles { get; set; } = new();
}