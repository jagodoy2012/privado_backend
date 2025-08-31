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
    public class tarjeta_tipoController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public tarjeta_tipoController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/tarjeta_tipo
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<tarjeta_tipo>>> Gettarjeta_tipo()
        {
            return await _context.tarjeta_tipo.ToListAsync();
        }

        // GET: api/tarjeta_tipo/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<tarjeta_tipo>> Gettarjeta_tipo(int id)
        {
            var tarjeta_tipo = await _context.tarjeta_tipo.FindAsync(id);

            if (tarjeta_tipo == null)
            {
                return NotFound();
            }

            return tarjeta_tipo;
        }

        // PUT: api/tarjeta_tipo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Puttarjeta_tipo(int id, tarjeta_tipo tarjeta_tipo)
        {
            if (id != tarjeta_tipo.id)
            {
                return BadRequest();
            }

            _context.Entry(tarjeta_tipo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tarjeta_tipoExists(id))
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

        // POST: api/tarjeta_tipo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<tarjeta_tipo>> Posttarjeta_tipo(tarjeta_tipo tarjeta_tipo)
        {
            _context.tarjeta_tipo.Add(tarjeta_tipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Gettarjeta_tipo", new { id = tarjeta_tipo.id }, tarjeta_tipo);
        }

        // DELETE: api/tarjeta_tipo/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Deletetarjeta_tipo(int id)
        {
            var tarjeta_tipo = await _context.tarjeta_tipo.FindAsync(id);
            if (tarjeta_tipo == null)
            {
                return NotFound();
            }

            _context.tarjeta_tipo.Remove(tarjeta_tipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool tarjeta_tipoExists(int id)
        {
            return _context.tarjeta_tipo.Any(e => e.id == id);
        }
    }
}
