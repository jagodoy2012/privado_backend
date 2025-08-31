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
    public class operacionesController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public operacionesController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/operaciones
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<operaciones>>> Getoperaciones()
        {
            return await _context.operaciones.ToListAsync();
        }

        // GET: api/operaciones/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<operaciones>> Getoperaciones(int id)
        {
            var operaciones = await _context.operaciones.FindAsync(id);

            if (operaciones == null)
            {
                return NotFound();
            }

            return operaciones;
        }

        // PUT: api/operaciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Putoperaciones(int id, operaciones operaciones)
        {
            if (id != operaciones.id)
            {
                return BadRequest();
            }

            _context.Entry(operaciones).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!operacionesExists(id))
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

        // POST: api/operaciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<operaciones>> Postoperaciones(operaciones operaciones)
        {
            _context.operaciones.Add(operaciones);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getoperaciones", new { id = operaciones.id }, operaciones);
        }

        // DELETE: api/operaciones/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Deleteoperaciones(int id)
        {
            var operaciones = await _context.operaciones.FindAsync(id);
            if (operaciones == null)
            {
                return NotFound();
            }

            _context.operaciones.Remove(operaciones);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool operacionesExists(int id)
        {
            return _context.operaciones.Any(e => e.id == id);
        }
    }
}
