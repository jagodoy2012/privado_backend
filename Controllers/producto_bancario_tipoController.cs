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
    public class producto_bancario_tipoController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public producto_bancario_tipoController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/producto_bancario_tipo
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<producto_bancario_tipo>>> Getproducto_bancario_tipo()
        {
            return await _context.producto_bancario_tipo.ToListAsync();
        }

        // GET: api/producto_bancario_tipo/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<producto_bancario_tipo>> Getproducto_bancario_tipo(int id)
        {
            var producto_bancario_tipo = await _context.producto_bancario_tipo.FindAsync(id);

            if (producto_bancario_tipo == null)
            {
                return NotFound();
            }

            return producto_bancario_tipo;
        }

        // PUT: api/producto_bancario_tipo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Putproducto_bancario_tipo(int id, producto_bancario_tipo producto_bancario_tipo)
        {
            if (id != producto_bancario_tipo.id)
            {
                return BadRequest();
            }

            _context.Entry(producto_bancario_tipo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!producto_bancario_tipoExists(id))
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

        // POST: api/producto_bancario_tipo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<producto_bancario_tipo>> Postproducto_bancario_tipo(producto_bancario_tipo producto_bancario_tipo)
        {
            _context.producto_bancario_tipo.Add(producto_bancario_tipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getproducto_bancario_tipo", new { id = producto_bancario_tipo.id }, producto_bancario_tipo);
        }

        // DELETE: api/producto_bancario_tipo/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Deleteproducto_bancario_tipo(int id)
        {
            var producto_bancario_tipo = await _context.producto_bancario_tipo.FindAsync(id);
            if (producto_bancario_tipo == null)
            {
                return NotFound();
            }

            _context.producto_bancario_tipo.Remove(producto_bancario_tipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool producto_bancario_tipoExists(int id)
        {
            return _context.producto_bancario_tipo.Any(e => e.id == id);
        }
    }
}
