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
    public class cuentatercerosController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public cuentatercerosController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/cuentaterceros
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<cuentaterceros>>> Getcuentaterceros()
        {
            return await _context.cuentaterceros.ToListAsync();
        }

        // GET: api/cuentaterceros/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<cuentaterceros>> Getcuentaterceros(int id)
        {
            var cuentaterceros = await _context.cuentaterceros.FindAsync(id);

            if (cuentaterceros == null)
            {
                return NotFound();
            }

            return cuentaterceros;
        }

        // PUT: api/cuentaterceros/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Putcuentaterceros(int id, cuentaterceros cuentaterceros)
        {
            if (id != cuentaterceros.id)
            {
                return BadRequest();
            }

            _context.Entry(cuentaterceros).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!cuentatercerosExists(id))
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

        // POST: api/cuentaterceros
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<cuentaterceros>> Postcuentaterceros(cuentaterceros cuentaterceros)
        {
            _context.cuentaterceros.Add(cuentaterceros);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getcuentaterceros", new { id = cuentaterceros.id }, cuentaterceros);
        }

        // DELETE: api/cuentaterceros/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Deletecuentaterceros(int id)
        {
            var cuentaterceros = await _context.cuentaterceros.FindAsync(id);
            if (cuentaterceros == null)
            {
                return NotFound();
            }

            _context.cuentaterceros.Remove(cuentaterceros);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // GET: api/cuentaterceros/buscar?id_usuario_prim=123
        [HttpGet("buscar")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<cuentaterceros>>> BuscarPorUsuarioPrim([FromQuery] int id_usuario_prim)
        {
            var data = await _context.cuentaterceros
                .AsNoTracking()
                .Where(c => c.id_usuario_prim == id_usuario_prim && c.estado == 1)
                .OrderBy(c => c.id)
                .ToListAsync();

            return Ok(data);
        }

        private bool cuentatercerosExists(int id)
        {
            return _context.cuentaterceros.Any(e => e.id == id);
        }
    }
}
