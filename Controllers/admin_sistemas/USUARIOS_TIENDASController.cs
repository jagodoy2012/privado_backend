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
    public class USUARIOS_TIENDASController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public USUARIOS_TIENDASController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/USUARIOS_TIENDAS
        [HttpGet]
        public async Task<ActionResult<IEnumerable<USUARIOS_TIENDAS>>> GetUSUARIOS_TIENDAS()
        {
            return await _context.USUARIOS_TIENDAS.ToListAsync();
        }

        // GET: api/USUARIOS_TIENDAS/5
        [HttpGet("{id}")]
        public async Task<ActionResult<USUARIOS_TIENDAS>> GetUSUARIOS_TIENDAS(int id)
        {
            var uSUARIOS_TIENDAS = await _context.USUARIOS_TIENDAS.FindAsync(id);

            if (uSUARIOS_TIENDAS == null)
            {
                return NotFound();
            }

            return uSUARIOS_TIENDAS;
        }

        // PUT: api/USUARIOS_TIENDAS/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUSUARIOS_TIENDAS(int id, USUARIOS_TIENDAS uSUARIOS_TIENDAS)
        {
            if (id != uSUARIOS_TIENDAS.id)
            {
                return BadRequest();
            }

            _context.Entry(uSUARIOS_TIENDAS).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!USUARIOS_TIENDASExists(id))
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

        // POST: api/USUARIOS_TIENDAS
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<USUARIOS_TIENDAS>> PostUSUARIOS_TIENDAS(USUARIOS_TIENDAS uSUARIOS_TIENDAS)
        {
            _context.USUARIOS_TIENDAS.Add(uSUARIOS_TIENDAS);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUSUARIOS_TIENDAS", new { id = uSUARIOS_TIENDAS.id }, uSUARIOS_TIENDAS);
        }

        // DELETE: api/USUARIOS_TIENDAS/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUSUARIOS_TIENDAS(int id)
        {
            var uSUARIOS_TIENDAS = await _context.USUARIOS_TIENDAS.FindAsync(id);
            if (uSUARIOS_TIENDAS == null)
            {
                return NotFound();
            }

            _context.USUARIOS_TIENDAS.Remove(uSUARIOS_TIENDAS);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool USUARIOS_TIENDASExists(int id)
        {
            return _context.USUARIOS_TIENDAS.Any(e => e.id == id);
        }
    }
}
