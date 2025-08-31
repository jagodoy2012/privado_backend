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
    public class cuentasController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public cuentasController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/cuentas
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<cuenta>>> Getcuenta()
        {
            return await _context.cuenta.ToListAsync();
        }

        // GET: api/cuentas/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<cuenta>> Getcuenta(int id)
        {
            var cuenta = await _context.cuenta.FindAsync(id);

            if (cuenta == null)
            {
                return NotFound();
            }

            return cuenta;
        }

        // PUT: api/cuentas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Putcuenta(int id, cuenta cuenta)
        {
            if (id != cuenta.id)
            {
                return BadRequest();
            }

            _context.Entry(cuenta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!cuentaExists(id))
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

        // POST: api/cuentas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<cuenta>> Postcuenta(cuenta cuenta)
        {
            _context.cuenta.Add(cuenta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getcuenta", new { id = cuenta.id }, cuenta);
        }

        // DELETE: api/cuentas/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Deletecuenta(int id)
        {
            var cuenta = await _context.cuenta.FindAsync(id);
            if (cuenta == null)
            {
                return NotFound();
            }

            _context.cuenta.Remove(cuenta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // GET: api/cuentas/activas
        [HttpGet("activas")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<cuenta>>> GetCuentasActivas()
        {
            var data = await _context.cuenta
                .AsNoTracking()
                .Where(c => c.estado == 1)
                .ToListAsync();

            return Ok(data);
        }

        // GET: api/cuentas/activas/5
        [HttpGet("activas/{id:int}")]
        [Authorize]
        public async Task<ActionResult<cuenta>> GetCuentaActivaPorId(int id)
        {
            var item = await _context.cuenta
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.id == id && c.estado == 1);

            if (item == null) return NotFound();
            return item;
        }

        private bool cuentaExists(int id)
        {
            return _context.cuenta.Any(e => e.id == id);
        }
    }
}
