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
    public class monedatipocambioapisController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public monedatipocambioapisController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/monedatipocambioapis
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<monedatipocambioapi>>> Getmonedatipocambioapi()
        {
            return await _context.monedatipocambioapi.ToListAsync();
        }

        // GET: api/monedatipocambioapis/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<monedatipocambioapi>> Getmonedatipocambioapi(int id)
        {
            var monedatipocambioapi = await _context.monedatipocambioapi.FindAsync(id);

            if (monedatipocambioapi == null)
            {
                return NotFound();
            }

            return monedatipocambioapi;
        }

        // PUT: api/monedatipocambioapis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Putmonedatipocambioapi(int id, monedatipocambioapi monedatipocambioapi)
        {
            if (id != monedatipocambioapi.id)
            {
                return BadRequest();
            }

            _context.Entry(monedatipocambioapi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!monedatipocambioapiExists(id))
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

        // POST: api/monedatipocambioapis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<monedatipocambioapi>> Postmonedatipocambioapi(monedatipocambioapi monedatipocambioapi)
        {
            _context.monedatipocambioapi.Add(monedatipocambioapi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getmonedatipocambioapi", new { id = monedatipocambioapi.id }, monedatipocambioapi);
        }

        // DELETE: api/monedatipocambioapis/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Deletemonedatipocambioapi(int id)
        {
            var monedatipocambioapi = await _context.monedatipocambioapi.FindAsync(id);
            if (monedatipocambioapi == null)
            {
                return NotFound();
            }

            _context.monedatipocambioapi.Remove(monedatipocambioapi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool monedatipocambioapiExists(int id)
        {
            return _context.monedatipocambioapi.Any(e => e.id == id);
        }
    }
}
