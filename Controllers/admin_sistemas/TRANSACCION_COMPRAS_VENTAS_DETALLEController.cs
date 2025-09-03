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
    public class TRANSACCION_COMPRAS_VENTAS_DETALLEController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public TRANSACCION_COMPRAS_VENTAS_DETALLEController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/TRANSACCION_COMPRAS_VENTAS_DETALLE
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TRANSACCION_COMPRAS_VENTAS_DETALLE>>> GetTRANSACCION_COMPRAS_VENTAS_DETALLE()
        {
            return await _context.TRANSACCION_COMPRAS_VENTAS_DETALLE.ToListAsync();
        }

        // GET: api/TRANSACCION_COMPRAS_VENTAS_DETALLE/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TRANSACCION_COMPRAS_VENTAS_DETALLE>> GetTRANSACCION_COMPRAS_VENTAS_DETALLE(int id)
        {
            var tRANSACCION_COMPRAS_VENTAS_DETALLE = await _context.TRANSACCION_COMPRAS_VENTAS_DETALLE.FindAsync(id);

            if (tRANSACCION_COMPRAS_VENTAS_DETALLE == null)
            {
                return NotFound();
            }

            return tRANSACCION_COMPRAS_VENTAS_DETALLE;
        }

        // PUT: api/TRANSACCION_COMPRAS_VENTAS_DETALLE/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTRANSACCION_COMPRAS_VENTAS_DETALLE(int id, TRANSACCION_COMPRAS_VENTAS_DETALLE tRANSACCION_COMPRAS_VENTAS_DETALLE)
        {
            if (id != tRANSACCION_COMPRAS_VENTAS_DETALLE.id)
            {
                return BadRequest();
            }

            _context.Entry(tRANSACCION_COMPRAS_VENTAS_DETALLE).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TRANSACCION_COMPRAS_VENTAS_DETALLEExists(id))
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

        // POST: api/TRANSACCION_COMPRAS_VENTAS_DETALLE
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TRANSACCION_COMPRAS_VENTAS_DETALLE>> PostTRANSACCION_COMPRAS_VENTAS_DETALLE(TRANSACCION_COMPRAS_VENTAS_DETALLE tRANSACCION_COMPRAS_VENTAS_DETALLE)
        {
            _context.TRANSACCION_COMPRAS_VENTAS_DETALLE.Add(tRANSACCION_COMPRAS_VENTAS_DETALLE);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTRANSACCION_COMPRAS_VENTAS_DETALLE", new { id = tRANSACCION_COMPRAS_VENTAS_DETALLE.id }, tRANSACCION_COMPRAS_VENTAS_DETALLE);
        }

        // DELETE: api/TRANSACCION_COMPRAS_VENTAS_DETALLE/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTRANSACCION_COMPRAS_VENTAS_DETALLE(int id)
        {
            var tRANSACCION_COMPRAS_VENTAS_DETALLE = await _context.TRANSACCION_COMPRAS_VENTAS_DETALLE.FindAsync(id);
            if (tRANSACCION_COMPRAS_VENTAS_DETALLE == null)
            {
                return NotFound();
            }

            _context.TRANSACCION_COMPRAS_VENTAS_DETALLE.Remove(tRANSACCION_COMPRAS_VENTAS_DETALLE);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TRANSACCION_COMPRAS_VENTAS_DETALLEExists(int id)
        {
            return _context.TRANSACCION_COMPRAS_VENTAS_DETALLE.Any(e => e.id == id);
        }
    }
}
