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
    public class municipiosController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public municipiosController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/municipios
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<municipio>>> Getmunicipio()
        {
            return await _context.municipio.ToListAsync();
        }

        // GET: api/municipios/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<municipio>> Getmunicipio(int id)
        {
            var municipio = await _context.municipio.FindAsync(id);

            if (municipio == null)
            {
                return NotFound();
            }

            return municipio;
        }

        // PUT: api/municipios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Putmunicipio(int id, municipio municipio)
        {
            if (id != municipio.id)
            {
                return BadRequest();
            }

            _context.Entry(municipio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!municipioExists(id))
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

        // POST: api/municipios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<municipio>> Postmunicipio(municipio municipio)
        {
            _context.municipio.Add(municipio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getmunicipio", new { id = municipio.id }, municipio);
        }
        // GET: /api/departamentos/{dptoId}/municipios
        // DTO para no exponer columnas innecesarias
        public sealed record MunicipioDto(int id, string titulo);

        // GET: /api/departamentos/{dptoId}/municipios   ← usa "~" para forzar la ruta exacta
        [HttpGet("{dptoId:int}/departamentos")]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<MunicipioDto>>> GetMunicipiosPorDepartamento(
            [FromRoute] int dptoId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 200,
            CancellationToken ct = default)
        {
            if (dptoId <= 0) return BadRequest("dptoId inválido.");
            page = Math.Clamp(page, 1, 100000);
            pageSize = Math.Clamp(pageSize, 1, 500);

            // (Opcional) valida existencia del departamento para evitar enumeración
            var existeDpto = await _context.departamento
                .AsNoTracking()
                .AnyAsync(d => d.id == dptoId, ct);
            if (!existeDpto) return NotFound();

            var baseQuery = _context.municipio
                .AsNoTracking()
                .Where(m => m.id_departamento == dptoId);

            var total = await baseQuery.CountAsync(ct);
            Response.Headers["X-Total-Count"] = total.ToString();

            var data = await baseQuery
                .OrderBy(m => m.titulo)                 // ajusta si tu campo es 'nombre'
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(m => new MunicipioDto(m.id, m.titulo))
                .ToListAsync(ct);

            return Ok(data); // 200 y [] si no hay resultados
        }


        // DELETE: api/municipios/5
        [HttpDelete("{id}")]

        [Authorize]

        public async Task<IActionResult> Deletemunicipio(int id)
        {
            var municipio = await _context.municipio.FindAsync(id);
            if (municipio == null)
            {
                return NotFound();
            }

            _context.municipio.Remove(municipio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool municipioExists(int id)
        {
            return _context.municipio.Any(e => e.id == id);
        }
    }
}
