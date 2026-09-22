using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SistemaFacturacionVentas.Api.Data;
using SistemaFacturacionVentas.Api.Models;
using SistemaFacturacionVentas.Api.DTOs;
using System.Security.Claims;

namespace SistemaFacturacionVentas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // requiere estar logueado (cualquier rol) para todo este controller
public class VentasController : ControllerBase
{
    private readonly AppDbContext _context;

    public VentasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Venta>>> GetVentas()
    {
        return await _context.Ventas
            .Include(v => v.Cliente)
            .Include(v => v.Usuario)
            .Include(v => v.Detalles)
                .ThenInclude(d => d.Producto)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Venta>> GetVenta(int id)
    {
        var venta = await _context.Ventas
            .Include(v => v.Cliente)
            .Include(v => v.Usuario)
            .Include(v => v.Detalles)
                .ThenInclude(d => d.Producto)
            .FirstOrDefaultAsync(v => v.Id == id);

        if (venta == null) return NotFound();
        return venta;
    }

    [HttpPost]
    public async Task<ActionResult<Venta>> PostVenta(CrearVentaDto dto)
    {
        if (dto.Detalles.Count == 0)
            return BadRequest("La venta debe tener al menos un producto.");

        var cliente = await _context.Clientes.FindAsync(dto.ClienteId);
        if (cliente == null) return BadRequest("Cliente no encontrado.");

        var usuarioIdStr = User.FindFirst("id")?.Value;
        if (usuarioIdStr == null) return Unauthorized();
        var usuarioId = int.Parse(usuarioIdStr);

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var venta = new Venta
            {
                ClienteId = dto.ClienteId,
                UsuarioId = usuarioId,
                Fecha = DateTime.Now,
                Estado = "Registrada"
            };

            decimal totalVenta = 0;

            foreach (var item in dto.Detalles)
            {
                var producto = await _context.Productos.FindAsync(item.ProductoId);
                if (producto == null)
                    return BadRequest($"Producto {item.ProductoId} no encontrado.");

                if (producto.StockActual < item.Cantidad)
                    return BadRequest($"Stock insuficiente para '{producto.Nombre}'. Disponible: {producto.StockActual}, solicitado: {item.Cantidad}.");

                producto.StockActual -= item.Cantidad;

                var subtotal = producto.Precio * item.Cantidad;
                totalVenta += subtotal;

                venta.Detalles.Add(new DetalleVenta
                {
                    ProductoId = producto.Id,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = producto.Precio,
                    Subtotal = subtotal
                });
            }

            venta.Total = totalVenta;

            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return CreatedAtAction(nameof(GetVenta), new { id = venta.Id }, venta);
        }
        catch (DbUpdateConcurrencyException)
        {
            await transaction.RollbackAsync();
            return Conflict("Otro usuario modificó el stock de un producto al mismo tiempo. Intenta la venta de nuevo.");
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}