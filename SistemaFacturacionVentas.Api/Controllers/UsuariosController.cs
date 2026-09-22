using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SistemaFacturacionVentas.Api.Data;
using SistemaFacturacionVentas.Api.DTOs;

namespace SistemaFacturacionVentas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UsuariosController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsuariosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioResumenDto>>> GetUsuarios()
    {
        return await _context.Usuarios
            .Select(u => new UsuarioResumenDto
            {
                Id = u.Id,
                NombreUsuario = u.NombreUsuario,
                Rol = u.Rol,
                Activo = u.Activo
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioResumenDto>> GetUsuario(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return NotFound();

        return new UsuarioResumenDto
        {
            Id = usuario.Id,
            NombreUsuario = usuario.NombreUsuario,
            Rol = usuario.Rol,
            Activo = usuario.Activo
        };
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUsuario(int id, ActualizarUsuarioDto dto)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return NotFound();

        usuario.Rol = dto.Rol;
        usuario.Activo = dto.Activo;

        await _context.SaveChangesAsync();
        return NoContent();
    }
}