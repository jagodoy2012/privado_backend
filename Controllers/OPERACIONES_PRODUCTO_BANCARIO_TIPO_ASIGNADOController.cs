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
    public class OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADOController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADOController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO>>> GetOPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO()
        {
            return await _context.OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO.ToListAsync();
        }

        // GET: api/OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO>> GetOPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO(int id)
        {
            var oPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO = await _context.OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO.FindAsync(id);

            if (oPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO == null)
            {
                return NotFound();
            }

            return oPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO;
        }

        // PUT: api/OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> PutOPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO(int id, OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO oPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO)
        {
            if (id != oPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO.id)
            {
                return BadRequest();
            }

            _context.Entry(oPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADOExists(id))
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

        // POST: api/OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO>> PostOPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO(OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO oPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO)
        {
            _context.OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO.Add(oPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO", new { id = oPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO.id }, oPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO);
        }

        // DELETE: api/OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> DeleteOPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO(int id)
        {
            var oPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO = await _context.OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO.FindAsync(id);
            if (oPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO == null)
            {
                return NotFound();
            }

            _context.OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO.Remove(oPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // GET: api/OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO/by-producto/5
        [HttpGet("by-producto/{id_producto_bancario}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO>>>
            GetByProducto(int id_producto_bancario)
        {
            var operaciones = await _context.OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO
                .AsNoTracking()
                .Where(o => o.id_producto_bancario == id_producto_bancario)
                .ToListAsync();

            if (operaciones == null || operaciones.Count == 0)
                return NotFound(); // Si prefieres 200 con [], cambia a: return Ok(operaciones);

            return Ok(operaciones);
        }

        private bool OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADOExists(int id)
        {
            return _context.OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO.Any(e => e.id == id);
        }
    }
}
