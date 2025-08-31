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
    public class zonasController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public zonasController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/zonas
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<zona>>> Getzona()
        {
            return await _context.zona.ToListAsync();
        }

        // GET: api/zonas/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<zona>> Getzona(int id)
        {
            var zona = await _context.zona.FindAsync(id);

            if (zona == null)
            {
                return NotFound();
            }

            return zona;
        }

        // PUT: api/zonas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Putzona(int id, zona zona)
        {
            if (id != zona.id)
            {
                return BadRequest();
            }

            _context.Entry(zona).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!zonaExists(id))
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

        // POST: api/zonas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<zona>> Postzona(zona zona)
        {
            _context.zona.Add(zona);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getzona", new { id = zona.id }, zona);
        }

        // DELETE: api/zonas/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Deletezona(int id)
        {
            var zona = await _context.zona.FindAsync(id);
            if (zona == null)
            {
                return NotFound();
            }

            _context.zona.Remove(zona);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DTO para no exponer columnas innecesarias
        public sealed record ZonaDto(int id, string titulo);

        // GET: /api/municipios/{municipioId}/zonas?page=1&pageSize=100
        [HttpGet("{municipioId:int}/municipios")]
        [Authorize] // o Policy/Role si aplica
        public async Task<ActionResult<IReadOnlyList<ZonaDto>>> GetZonasPorMunicipio(
            int municipioId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 100,
            CancellationToken ct = default)
        {
            if (municipioId <= 0) return BadRequest("municipioId inválido.");

            // límites de paginación para evitar respuestas enormes (DoS).
            page = Math.Clamp(page, 1, 100000);
            pageSize = Math.Clamp(pageSize, 1, 500);

            // (Opcional) valida existencia del municipio para evitar enumeración arbitraria.
            var existeMun = await _context.municipio
                .AsNoTracking()
                .AnyAsync(m => m.id == municipioId, ct);
            if (!existeMun) return NotFound();

            var query = _context.zona.AsNoTracking()
                .Where(z => z.id_municipio == municipioId)
                .OrderBy(z => z.titulo);

            var total = await query.CountAsync(ct);
            Response.Headers["X-Total-Count"] = total.ToString();

            var data = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(z => new ZonaDto(z.id, z.titulo))
                .ToListAsync(ct);

            return Ok(data);
        }


        private bool zonaExists(int id)
        {
            return _context.zona.Any(e => e.id == id);
        }
    }
}
