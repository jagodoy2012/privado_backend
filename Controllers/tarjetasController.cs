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
    public class tarjetasController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public tarjetasController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/tarjetas
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<tarjeta>>> Gettarjeta()
        {
            return await _context.tarjeta.ToListAsync();
        }

        // GET: api/tarjetas/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<tarjeta>> Gettarjeta(int id)
        {
            var tarjeta = await _context.tarjeta.FindAsync(id);

            if (tarjeta == null)
            {
                return NotFound();
            }

            return tarjeta;
        }

        // PUT: api/tarjetas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Puttarjeta(int id, tarjeta tarjeta)
        {
            if (id != tarjeta.id)
            {
                return BadRequest();
            }

            _context.Entry(tarjeta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tarjetaExists(id))
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

        // POST: api/tarjetas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<tarjeta>> Posttarjeta(tarjeta tarjeta)
        {
            _context.tarjeta.Add(tarjeta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Gettarjeta", new { id = tarjeta.id }, tarjeta);
        }

        // DELETE: api/tarjetas/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Deletetarjeta(int id)
        {
            var tarjeta = await _context.tarjeta.FindAsync(id);
            if (tarjeta == null)
            {
                return NotFound();
            }

            _context.tarjeta.Remove(tarjeta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // GET: api/tarjetas/activas
        [HttpGet("activas")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<tarjeta>>> GetTarjetasActivas()
        {
            var data = await _context.tarjeta
                .AsNoTracking()
                .Where(t => t.estado == 1)
                .ToListAsync();

            return Ok(data);
        }

        // GET: api/tarjetas/activas/5
        [HttpGet("activas/{id:int}")]
        [Authorize]
        public async Task<ActionResult<tarjeta>> GetTarjetaActivaPorId(int id)
        {
            var item = await _context.tarjeta
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.id == id && t.estado == 1);

            if (item == null) return NotFound();
            return item;
        }

        private bool tarjetaExists(int id)
        {
            return _context.tarjeta.Any(e => e.id == id);
        }
    }
}
