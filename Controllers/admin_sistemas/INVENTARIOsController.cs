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
    public class INVENTARIOsController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public INVENTARIOsController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/INVENTARIOs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<INVENTARIO>>> GetINVENTARIO()
        {
            return await _context.INVENTARIO.ToListAsync();
        }

        // GET: api/INVENTARIOs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<INVENTARIO>> GetINVENTARIO(int id)
        {
            var iNVENTARIO = await _context.INVENTARIO.FindAsync(id);

            if (iNVENTARIO == null)
            {
                return NotFound();
            }

            return iNVENTARIO;
        }

        // PUT: api/INVENTARIOs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutINVENTARIO(int id, INVENTARIO iNVENTARIO)
        {
            if (id != iNVENTARIO.id)
            {
                return BadRequest();
            }

            _context.Entry(iNVENTARIO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!INVENTARIOExists(id))
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

        // POST: api/INVENTARIOs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<INVENTARIO>> PostINVENTARIO(INVENTARIO iNVENTARIO)
        {
            _context.INVENTARIO.Add(iNVENTARIO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetINVENTARIO", new { id = iNVENTARIO.id }, iNVENTARIO);
        }

        // DELETE: api/INVENTARIOs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteINVENTARIO(int id)
        {
            var iNVENTARIO = await _context.INVENTARIO.FindAsync(id);
            if (iNVENTARIO == null)
            {
                return NotFound();
            }

            _context.INVENTARIO.Remove(iNVENTARIO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool INVENTARIOExists(int id)
        {
            return _context.INVENTARIO.Any(e => e.id == id);
        }
    }
}
