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
    public class PRODUCTOController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public PRODUCTOController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/PRODUCTO
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PRODUCTO>>> GetPRODUCTO()
        {
            return await _context.PRODUCTO.ToListAsync();
        }

        // GET: api/PRODUCTO/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PRODUCTO>> GetPRODUCTO(int id)
        {
            var pRODUCTO = await _context.PRODUCTO.FindAsync(id);

            if (pRODUCTO == null)
            {
                return NotFound();
            }

            return pRODUCTO;
        }

        // PUT: api/PRODUCTO/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPRODUCTO(int id, PRODUCTO pRODUCTO)
        {
            if (id != pRODUCTO.id)
            {
                return BadRequest();
            }

            _context.Entry(pRODUCTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PRODUCTOExists(id))
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

        // POST: api/PRODUCTO
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PRODUCTO>> PostPRODUCTO(PRODUCTO pRODUCTO)
        {
            _context.PRODUCTO.Add(pRODUCTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPRODUCTO", new { id = pRODUCTO.id }, pRODUCTO);
        }

        // DELETE: api/PRODUCTO/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePRODUCTO(int id)
        {
            var pRODUCTO = await _context.PRODUCTO.FindAsync(id);
            if (pRODUCTO == null)
            {
                return NotFound();
            }

            _context.PRODUCTO.Remove(pRODUCTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PRODUCTOExists(int id)
        {
            return _context.PRODUCTO.Any(e => e.id == id);
        }
    }
}
