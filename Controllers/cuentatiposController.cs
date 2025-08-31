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
    public class cuentatiposController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public cuentatiposController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/cuentatipos
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<cuentatipo>>> Getcuentatipo()
        {
            return await _context.cuentatipo.ToListAsync();
        }

        // GET: api/cuentatipos/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<cuentatipo>> Getcuentatipo(int id)
        {
            var cuentatipo = await _context.cuentatipo.FindAsync(id);

            if (cuentatipo == null)
            {
                return NotFound();
            }

            return cuentatipo;
        }

        // PUT: api/cuentatipos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Putcuentatipo(int id, cuentatipo cuentatipo)
        {
            if (id != cuentatipo.id)
            {
                return BadRequest();
            }

            _context.Entry(cuentatipo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!cuentatipoExists(id))
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

        // POST: api/cuentatipos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<cuentatipo>> Postcuentatipo(cuentatipo cuentatipo)
        {
            _context.cuentatipo.Add(cuentatipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getcuentatipo", new { id = cuentatipo.id }, cuentatipo);
        }

        // DELETE: api/cuentatipos/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Deletecuentatipo(int id)
        {
            var cuentatipo = await _context.cuentatipo.FindAsync(id);
            if (cuentatipo == null)
            {
                return NotFound();
            }

            _context.cuentatipo.Remove(cuentatipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool cuentatipoExists(int id)
        {
            return _context.cuentatipo.Any(e => e.id == id);
        }
    }
}
