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
    public class monedatiposController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public monedatiposController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/monedatipoes
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<monedatipo>>> Getmonedatipo()
        {
            return await _context.monedatipo.ToListAsync();
        }

        // GET: api/monedatipoes/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<monedatipo>> Getmonedatipo(int id)
        {
            var monedatipo = await _context.monedatipo.FindAsync(id);

            if (monedatipo == null)
            {
                return NotFound();
            }

            return monedatipo;
        }

        // PUT: api/monedatipoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Putmonedatipo(int id, monedatipo monedatipo)
        {
            if (id != monedatipo.id)
            {
                return BadRequest();
            }

            _context.Entry(monedatipo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!monedatipoExists(id))
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

        // POST: api/monedatipoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<monedatipo>> Postmonedatipo(monedatipo monedatipo)
        {
            _context.monedatipo.Add(monedatipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getmonedatipo", new { id = monedatipo.id }, monedatipo);
        }

        // DELETE: api/monedatipoes/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Deletemonedatipo(int id)
        {
            var monedatipo = await _context.monedatipo.FindAsync(id);
            if (monedatipo == null)
            {
                return NotFound();
            }

            _context.monedatipo.Remove(monedatipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool monedatipoExists(int id)
        {
            return _context.monedatipo.Any(e => e.id == id);
        }
    }
}
