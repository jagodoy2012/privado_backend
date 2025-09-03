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
    public class TRANSACCION_TIPOController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public TRANSACCION_TIPOController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/TRANSACCION_TIPO
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TRANSACCION_TIPO>>> GetTRANSACCION_TIPO()
        {
            return await _context.TRANSACCION_TIPO.ToListAsync();
        }

        // GET: api/TRANSACCION_TIPO/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TRANSACCION_TIPO>> GetTRANSACCION_TIPO(int id)
        {
            var tRANSACCION_TIPO = await _context.TRANSACCION_TIPO.FindAsync(id);

            if (tRANSACCION_TIPO == null)
            {
                return NotFound();
            }

            return tRANSACCION_TIPO;
        }

        // PUT: api/TRANSACCION_TIPO/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTRANSACCION_TIPO(int id, TRANSACCION_TIPO tRANSACCION_TIPO)
        {
            if (id != tRANSACCION_TIPO.id)
            {
                return BadRequest();
            }

            _context.Entry(tRANSACCION_TIPO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TRANSACCION_TIPOExists(id))
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

        // POST: api/TRANSACCION_TIPO
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TRANSACCION_TIPO>> PostTRANSACCION_TIPO(TRANSACCION_TIPO tRANSACCION_TIPO)
        {
            _context.TRANSACCION_TIPO.Add(tRANSACCION_TIPO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTRANSACCION_TIPO", new { id = tRANSACCION_TIPO.id }, tRANSACCION_TIPO);
        }

        // DELETE: api/TRANSACCION_TIPO/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTRANSACCION_TIPO(int id)
        {
            var tRANSACCION_TIPO = await _context.TRANSACCION_TIPO.FindAsync(id);
            if (tRANSACCION_TIPO == null)
            {
                return NotFound();
            }

            _context.TRANSACCION_TIPO.Remove(tRANSACCION_TIPO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TRANSACCION_TIPOExists(int id)
        {
            return _context.TRANSACCION_TIPO.Any(e => e.id == id);
        }
    }
}
