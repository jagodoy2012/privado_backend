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
    public class TRANSACCIONESController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public TRANSACCIONESController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/TRANSACCIONES
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TRANSACCIONES>>> GetTRANSACCIONES()
        {
            return await _context.TRANSACCIONES.ToListAsync();
        }

        // GET: api/TRANSACCIONES/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TRANSACCIONES>> GetTRANSACCIONES(int id)
        {
            var tRANSACCIONES = await _context.TRANSACCIONES.FindAsync(id);

            if (tRANSACCIONES == null)
            {
                return NotFound();
            }

            return tRANSACCIONES;
        }

        // PUT: api/TRANSACCIONES/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTRANSACCIONES(int id, TRANSACCIONES tRANSACCIONES)
        {
            if (id != tRANSACCIONES.id)
            {
                return BadRequest();
            }

            _context.Entry(tRANSACCIONES).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TRANSACCIONESExists(id))
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

        // POST: api/TRANSACCIONES
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TRANSACCIONES>> PostTRANSACCIONES(TRANSACCIONES tRANSACCIONES)
        {
            _context.TRANSACCIONES.Add(tRANSACCIONES);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTRANSACCIONES", new { id = tRANSACCIONES.id }, tRANSACCIONES);
        }

        // DELETE: api/TRANSACCIONES/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTRANSACCIONES(int id)
        {
            var tRANSACCIONES = await _context.TRANSACCIONES.FindAsync(id);
            if (tRANSACCIONES == null)
            {
                return NotFound();
            }

            _context.TRANSACCIONES.Remove(tRANSACCIONES);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TRANSACCIONESExists(int id)
        {
            return _context.TRANSACCIONES.Any(e => e.id == id);
        }
    }
}
