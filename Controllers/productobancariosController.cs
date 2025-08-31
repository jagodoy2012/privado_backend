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
    public class productobancariosController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public productobancariosController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/productobancarios
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<productobancario>>> Getproductobancario()
        {
            return await _context.productobancario.ToListAsync();
        }

        // GET: api/productobancarios/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<productobancario>> Getproductobancario(int id)
        {
            var productobancario = await _context.productobancario.FindAsync(id);

            if (productobancario == null)
            {
                return NotFound();
            }

            return productobancario;
        }

        // PUT: api/productobancarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Putproductobancario(int id, productobancario productobancario)
        {
            if (id != productobancario.id)
            {
                return BadRequest();
            }

            _context.Entry(productobancario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productobancarioExists(id))
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

        // POST: api/productobancarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<productobancario>> Postproductobancario(productobancario productobancario)
        {
            _context.productobancario.Add(productobancario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getproductobancario", new { id = productobancario.id }, productobancario);
        }

        // DELETE: api/productobancarios/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Deleteproductobancario(int id)
        {
            var productobancario = await _context.productobancario.FindAsync(id);
            if (productobancario == null)
            {
                return NotFound();
            }

            _context.productobancario.Remove(productobancario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool productobancarioExists(int id)
        {
            return _context.productobancario.Any(e => e.id == id);
        }
    }
}
