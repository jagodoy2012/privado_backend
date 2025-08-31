using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using privado_backend.Data;
using privado_backend.Models;

namespace privado_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class tipo_cuenta_tarjetaController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public tipo_cuenta_tarjetaController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/tipo_cuenta_tarjeta
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<tipo_cuenta_tarjeta>>> Gettipo_cuenta_tarjeta()
        {
            return await _context.tipo_cuenta_tarjeta.ToListAsync();
        }

        // GET: api/tipo_cuenta_tarjeta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<tipo_cuenta_tarjeta>> Gettipo_cuenta_tarjeta(int id)
        {
            var tipo_cuenta_tarjeta = await _context.tipo_cuenta_tarjeta.FindAsync(id);

            if (tipo_cuenta_tarjeta == null)
            {
                return NotFound();
            }

            return tipo_cuenta_tarjeta;
        }

        // PUT: api/tipo_cuenta_tarjeta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Puttipo_cuenta_tarjeta(int id, tipo_cuenta_tarjeta tipo_cuenta_tarjeta)
        {
            if (id != tipo_cuenta_tarjeta.id)
            {
                return BadRequest();
            }

            _context.Entry(tipo_cuenta_tarjeta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tipo_cuenta_tarjetaExists(id))
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

        // POST: api/tipo_cuenta_tarjeta
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<tipo_cuenta_tarjeta>> Posttipo_cuenta_tarjeta(tipo_cuenta_tarjeta tipo_cuenta_tarjeta)
        {
            _context.tipo_cuenta_tarjeta.Add(tipo_cuenta_tarjeta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Gettipo_cuenta_tarjeta", new { id = tipo_cuenta_tarjeta.id }, tipo_cuenta_tarjeta);
        }

        // DELETE: api/tipo_cuenta_tarjeta/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Deletetipo_cuenta_tarjeta(int id)
        {
            var tipo_cuenta_tarjeta = await _context.tipo_cuenta_tarjeta.FindAsync(id);
            if (tipo_cuenta_tarjeta == null)
            {
                return NotFound();
            }

            _context.tipo_cuenta_tarjeta.Remove(tipo_cuenta_tarjeta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool tipo_cuenta_tarjetaExists(int id)
        {
            return _context.tipo_cuenta_tarjeta.Any(e => e.id == id);
        }
    }
}
