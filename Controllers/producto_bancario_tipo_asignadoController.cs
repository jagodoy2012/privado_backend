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
    public class producto_bancario_tipo_asignadoController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public producto_bancario_tipo_asignadoController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/producto_bancario_tipo_asignado
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<producto_bancario_tipo_asignado>>> Getproducto_bancario_tipo_asignado()
        {
            return await _context.producto_bancario_tipo_asignado.ToListAsync();
        }

        // GET: api/producto_bancario_tipo_asignado/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<producto_bancario_tipo_asignado>> Getproducto_bancario_tipo_asignado(int id)
        {
            var producto_bancario_tipo_asignado = await _context.producto_bancario_tipo_asignado.FindAsync(id);

            if (producto_bancario_tipo_asignado == null)
            {
                return NotFound();
            }

            return producto_bancario_tipo_asignado;
        }

        // PUT: api/producto_bancario_tipo_asignado/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Putproducto_bancario_tipo_asignado(int id, producto_bancario_tipo_asignado producto_bancario_tipo_asignado)
        {
            if (id != producto_bancario_tipo_asignado.Id)
            {
                return BadRequest();
            }

            _context.Entry(producto_bancario_tipo_asignado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!producto_bancario_tipo_asignadoExists(id))
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

        // POST: api/producto_bancario_tipo_asignado
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<producto_bancario_tipo_asignado>> Postproducto_bancario_tipo_asignado(producto_bancario_tipo_asignado producto_bancario_tipo_asignado)
        {
            _context.producto_bancario_tipo_asignado.Add(producto_bancario_tipo_asignado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getproducto_bancario_tipo_asignado", new { id = producto_bancario_tipo_asignado.Id }, producto_bancario_tipo_asignado);
        }

        // DELETE: api/producto_bancario_tipo_asignado/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Deleteproducto_bancario_tipo_asignado(int id)
        {
            var producto_bancario_tipo_asignado = await _context.producto_bancario_tipo_asignado.FindAsync(id);
            if (producto_bancario_tipo_asignado == null)
            {
                return NotFound();
            }

            _context.producto_bancario_tipo_asignado.Remove(producto_bancario_tipo_asignado);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // GET: api/producto_bancario_tipo_asignado/by-producto/5
        [HttpGet("by-producto/{idProductoBancario:int}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<producto_bancario_tipo_asignado>>>
            GetByProducto(int idProductoBancario)
        {
            var rows = await _context.producto_bancario_tipo_asignado
                .AsNoTracking()
                .Where(x => x.IdProductoBancario == idProductoBancario)
                .ToListAsync();

            return Ok(rows); // devuelve [] si no hay coincidencias
        }

        private bool producto_bancario_tipo_asignadoExists(int id)
        {
            return _context.producto_bancario_tipo_asignado.Any(e => e.Id == id);
        }
    }
}
