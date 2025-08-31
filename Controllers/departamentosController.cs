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
    public class departamentosController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public departamentosController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/departamentos
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<departamento>>> Getdepartamento()
        {
            return await _context.departamento.ToListAsync();
        }

        // GET: api/departamentos/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<departamento>> Getdepartamento(int id)
        {
            var departamento = await _context.departamento.FindAsync(id);

            if (departamento == null)
            {
                return NotFound();
            }

            return departamento;
        }

        // PUT: api/departamentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Putdepartamento(int id, departamento departamento)
        {
            if (id != departamento.id)
            {
                return BadRequest();
            }

            _context.Entry(departamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!departamentoExists(id))
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

        // POST: api/departamentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<departamento>> Postdepartamento(departamento departamento)
        {
            _context.departamento.Add(departamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getdepartamento", new { id = departamento.id }, departamento);
        }

        // DELETE: api/departamentos/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Deletedepartamento(int id)
        {
            var departamento = await _context.departamento.FindAsync(id);
            if (departamento == null)
            {
                return NotFound();
            }

            _context.departamento.Remove(departamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool departamentoExists(int id)
        {
            return _context.departamento.Any(e => e.id == id);
        }
    }
}
