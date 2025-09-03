using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using privado_backend.Data;
using privado_backend.Models.SistemasPrivado;

namespace privado_backend.Controllers.admin_sistemas
{
    [Route("api/[controller]")]
    [ApiController]
    public class TIENDASController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public TIENDASController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/TIENDAS
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TIENDAS>>> GetTIENDAS()
        {
            return await _context.TIENDAS.ToListAsync();
        }

        // GET: api/TIENDAS/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TIENDAS>> GetTIENDAS(int id)
        {
            var tIENDAS = await _context.TIENDAS.FindAsync(id);

            if (tIENDAS == null)
            {
                return NotFound();
            }

            return tIENDAS;
        }

        // PUT: api/TIENDAS/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTIENDAS(int id, TIENDAS tIENDAS)
        {
            if (id != tIENDAS.id)
            {
                return BadRequest();
            }

            _context.Entry(tIENDAS).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TIENDASExists(id))
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

        // POST: api/TIENDAS
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TIENDAS>> PostTIENDAS(TIENDAS tIENDAS)
        {
            _context.TIENDAS.Add(tIENDAS);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTIENDAS", new { id = tIENDAS.id }, tIENDAS);
        }

        // DELETE: api/TIENDAS/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTIENDAS(int id)
        {
            var tIENDAS = await _context.TIENDAS.FindAsync(id);
            if (tIENDAS == null)
            {
                return NotFound();
            }

            _context.TIENDAS.Remove(tIENDAS);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TIENDASExists(int id)
        {
            return _context.TIENDAS.Any(e => e.id == id);
        }
    }
}
