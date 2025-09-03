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
    public class TRANSACCIONES_MOVIMIENTOSController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public TRANSACCIONES_MOVIMIENTOSController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/TRANSACCIONES_MOVIMIENTOS
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TRANSACCIONES_MOVIMIENTOS>>> GetTRANSACCIONES_MOVIMIENTOS()
        {
            return await _context.TRANSACCIONES_MOVIMIENTOS.ToListAsync();
        }

        // GET: api/TRANSACCIONES_MOVIMIENTOS/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TRANSACCIONES_MOVIMIENTOS>> GetTRANSACCIONES_MOVIMIENTOS(int id)
        {
            var tRANSACCIONES_MOVIMIENTOS = await _context.TRANSACCIONES_MOVIMIENTOS.FindAsync(id);

            if (tRANSACCIONES_MOVIMIENTOS == null)
            {
                return NotFound();
            }

            return tRANSACCIONES_MOVIMIENTOS;
        }

        // PUT: api/TRANSACCIONES_MOVIMIENTOS/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTRANSACCIONES_MOVIMIENTOS(int id, TRANSACCIONES_MOVIMIENTOS tRANSACCIONES_MOVIMIENTOS)
        {
            if (id != tRANSACCIONES_MOVIMIENTOS.id)
            {
                return BadRequest();
            }

            _context.Entry(tRANSACCIONES_MOVIMIENTOS).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TRANSACCIONES_MOVIMIENTOSExists(id))
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

        // POST: api/TRANSACCIONES_MOVIMIENTOS
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TRANSACCIONES_MOVIMIENTOS>> PostTRANSACCIONES_MOVIMIENTOS(TRANSACCIONES_MOVIMIENTOS tRANSACCIONES_MOVIMIENTOS)
        {
            _context.TRANSACCIONES_MOVIMIENTOS.Add(tRANSACCIONES_MOVIMIENTOS);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTRANSACCIONES_MOVIMIENTOS", new { id = tRANSACCIONES_MOVIMIENTOS.id }, tRANSACCIONES_MOVIMIENTOS);
        }

        // DELETE: api/TRANSACCIONES_MOVIMIENTOS/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTRANSACCIONES_MOVIMIENTOS(int id)
        {
            var tRANSACCIONES_MOVIMIENTOS = await _context.TRANSACCIONES_MOVIMIENTOS.FindAsync(id);
            if (tRANSACCIONES_MOVIMIENTOS == null)
            {
                return NotFound();
            }

            _context.TRANSACCIONES_MOVIMIENTOS.Remove(tRANSACCIONES_MOVIMIENTOS);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TRANSACCIONES_MOVIMIENTOSExists(int id)
        {
            return _context.TRANSACCIONES_MOVIMIENTOS.Any(e => e.id == id);
        }
    }
}
