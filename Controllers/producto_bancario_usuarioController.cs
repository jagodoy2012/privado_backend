using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using privado_backend.Data;

namespace privado_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class producto_bancario_usuarioController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public producto_bancario_usuarioController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/producto_bancario_usuario
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<producto_bancario_usuario>>> Getproducto_bancario_usuario()
        {
            return await _context.producto_bancario_usuario.ToListAsync();
        }

        // GET: api/producto_bancario_usuario/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<producto_bancario_usuario>> Getproducto_bancario_usuario(int id)
        {
            var producto_bancario_usuario = await _context.producto_bancario_usuario.FindAsync(id);

            if (producto_bancario_usuario == null)
            {
                return NotFound();
            }

            return producto_bancario_usuario;
        }

        // PUT: api/producto_bancario_usuario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Putproducto_bancario_usuario(int id, producto_bancario_usuario producto_bancario_usuario)
        {
            if (id != producto_bancario_usuario.id)
            {
                return BadRequest();
            }

            _context.Entry(producto_bancario_usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!producto_bancario_usuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/producto_bancario_usuario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<producto_bancario_usuario>> Postproducto_bancario_usuario(producto_bancario_usuario producto_bancario_usuario)
        {
            _context.producto_bancario_usuario.Add(producto_bancario_usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getproducto_bancario_usuario", new { id = producto_bancario_usuario.id }, producto_bancario_usuario);
        }

        // DELETE: api/producto_bancario_usuario/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Deleteproducto_bancario_usuario(int id)
        {
            var producto_bancario_usuario = await _context.producto_bancario_usuario.FindAsync(id);
            if (producto_bancario_usuario == null)
            {
                return NotFound();
            }

            _context.producto_bancario_usuario.Remove(producto_bancario_usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/producto_bancario_usuario/find?id_producto_bancario_asignado=10&id_usuario_producto=5
        [HttpGet("find")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<producto_bancario_usuario>>> Find(
            [FromQuery] int id_producto_bancario_asignado,
            [FromQuery] int id_usuario_producto)
        {
            var list = await _context.producto_bancario_usuario
                .AsNoTracking()
                .Where(x => x.id_producto_bancario_asignado == id_producto_bancario_asignado
                         && x.id_usuario_producto == id_usuario_producto)
                .ToListAsync();

            if (list.Count == 0) return NotFound();
            return Ok(list);
        }
        // GET: api/producto_bancario_usuario/by-usuario/5
        [HttpGet("by-usuario/{usuarioId:int}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<producto_bancario_usuario>>> GetByUsuario(int usuarioId)
        {
            var list = await _context.producto_bancario_usuario
                .AsNoTracking()
                .Where(x => x.id_usuario_producto == usuarioId)
                .ToListAsync();

            if (list.Count == 0) return NotFound();
            return Ok(list);
        }

        // GET: api/producto_bancario_usuario/by-producto-asignado/10
        [HttpGet("by-producto-asignado/{asignadoId:int}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<producto_bancario_usuario>>> GetByProductoAsignado(int asignadoId)
        {
            var list = await _context.producto_bancario_usuario
                .AsNoTracking()
                .Where(x => x.id_producto_bancario_asignado == asignadoId)
                .ToListAsync();

            if (list.Count == 0) return NotFound();
            return Ok(list);
        }

        // GET: api/producto_bancario_usuario/exists?asignadoId=10&usuarioId=5
        [HttpGet("exists")]
        [Authorize]
        public async Task<ActionResult<bool>> Exists([FromQuery] int asignadoId, [FromQuery] int usuarioId)
        {
            bool exists = await _context.producto_bancario_usuario
                .AsNoTracking()
                .AnyAsync(x => x.id_producto_bancario_asignado == asignadoId &&
                               x.id_usuario_producto == usuarioId);
            return Ok(exists);
        }

        // GET: api/producto_bancario_usuario/search
        // Ejemplo:
        //   /api/producto_bancario_usuario/search?usuarioId=5&asignadoId=10&monedaId=1&estado=1
        //   &desde=2025-01-01&hasta=2025-12-31&page=1&pageSize=20&sort=-fecha
        [HttpGet("search")]
        [Authorize]
        public async Task<ActionResult<object>> Search(
            [FromQuery] int? usuarioId,
            [FromQuery] int? asignadoId,
            [FromQuery] int? monedaId,
            [FromQuery] int? estado,
            [FromQuery] DateTime? desde,
            [FromQuery] DateTime? hasta,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? sort = "-fecha" // "fecha" asc, "-fecha" desc, "-id" etc.
        )
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 20;
            if (pageSize > 200) pageSize = 200;

            var q = _context.producto_bancario_usuario.AsNoTracking().AsQueryable();

            if (usuarioId.HasValue)
                q = q.Where(x => x.id_usuario_producto == usuarioId.Value);

            if (asignadoId.HasValue)
                q = q.Where(x => x.id_producto_bancario_asignado == asignadoId.Value);

            if (monedaId.HasValue)
                q = q.Where(x => x.id_moneda_tipo == monedaId.Value);

            if (estado.HasValue)
                q = q.Where(x => x.estado == estado.Value);

            if (desde.HasValue)
                q = q.Where(x => x.fecha >= desde.Value);

            if (hasta.HasValue)
                q = q.Where(x => x.fecha <= hasta.Value);

            // Ordenamiento simple por campo
            // soporta: "id", "-id", "fecha", "-fecha", "monto", "-monto", "disponible", "-disponible"
            q = sort switch
            {
                "id" => q.OrderBy(x => x.id),
                "-id" => q.OrderByDescending(x => x.id),
                "fecha" => q.OrderBy(x => x.fecha),
                "-fecha" => q.OrderByDescending(x => x.fecha),
                "monto" => q.OrderBy(x => x.monto),
                "-monto" => q.OrderByDescending(x => x.monto),
                "disponible" => q.OrderBy(x => x.disponible),
                "-disponible" => q.OrderByDescending(x => x.disponible),
                _ => q.OrderByDescending(x => x.fecha) // default
            };

            var total = await q.CountAsync();

            var items = await q
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new
                {
                    x.id,
                    x.id_usuario_producto,
                    x.id_producto_bancario_asignado,
                    x.id_moneda_tipo,
                    x.monto,
                    x.disponible,
                    x.estado,
                    x.fecha,
                    x.id_usuario,
                    x.fecha_ultimo_corte
                })
                .ToListAsync();

            return Ok(new
            {
                page,
                pageSize,
                total,
                totalPages = (int)Math.Ceiling(total / (double)pageSize),
                items
            });
        }
        // GET: api/producto_bancario_usuario/active
        [HttpGet("active")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<producto_bancario_usuario>>> GetActive()
        {
            var list = await _context.producto_bancario_usuario
                .AsNoTracking()
                .Where(x => x.estado == 1)
                .ToListAsync();

            if (list.Count == 0) return NotFound();
            return Ok(list);
        }

        // GET: api/producto_bancario_usuario/active/5
        [HttpGet("active/{id}")]
        [Authorize]
        public async Task<ActionResult<producto_bancario_usuario>> GetActiveById(int id)
        {
            var entity = await _context.producto_bancario_usuario
                .AsNoTracking()
                .Where(x => x.estado == 1 && x.id == id)
                .FirstOrDefaultAsync();

            if (entity == null) return NotFound();
            return Ok(entity);
        }

        // GET: api/producto_bancario_usuario/active/find?id_producto_bancario_asignado=10&id_usuario_producto=5
        [HttpGet("active/find")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<producto_bancario_usuario>>> FindActive(
            [FromQuery] int id_producto_bancario_asignado,
            [FromQuery] int id_usuario_producto)
        {
            var list = await _context.producto_bancario_usuario
                .AsNoTracking()
                .Where(x => x.estado == 1
                         && x.id_producto_bancario_asignado == id_producto_bancario_asignado
                         && x.id_usuario_producto == id_usuario_producto)
                .ToListAsync();

            if (list.Count == 0) return NotFound();
            return Ok(list);
        }


        private bool producto_bancario_usuarioExists(int id)
        {
            return _context.producto_bancario_usuario.Any(e => e.id == id);
        }
    }
}
