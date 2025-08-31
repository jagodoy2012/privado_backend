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
    public class usuario_direccionController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public usuario_direccionController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/usuario_direccion
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<usuario_direccion>>> Getusuario_direccion()
        {
            return await _context.usuario_direccion.ToListAsync();
        }

        // GET: api/usuario_direccion/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<usuario_direccion>> Getusuario_direccion(int id)
        {
            var usuario_direccion = await _context.usuario_direccion.FindAsync(id);

            if (usuario_direccion == null)
            {
                return NotFound();
            }

            return usuario_direccion;
        }

        // PUT: api/usuario_direccion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Putusuario_direccion(int id, usuario_direccion usuario_direccion)
        {
            if (id != usuario_direccion.id)
            {
                return BadRequest();
            }

            _context.Entry(usuario_direccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!usuario_direccionExists(id))
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

        // POST: api/usuario_direccion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<usuario_direccion>> Postusuario_direccion(usuario_direccion usuario_direccion)
        {
            _context.usuario_direccion.Add(usuario_direccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getusuario_direccion", new { id = usuario_direccion.id }, usuario_direccion);
        }

        // DELETE: api/usuario_direccion/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Deleteusuario_direccion(int id)
        {
            var usuario_direccion = await _context.usuario_direccion.FindAsync(id);
            if (usuario_direccion == null)
            {
                return NotFound();
            }

            _context.usuario_direccion.Remove(usuario_direccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("{id:int}/direcciones")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<usuario_direccion>>> GetDireccionesPorUsuario(int id)
        {
            if (id <= 0) return BadRequest("Id inválido.");

            var direcciones = await _context.usuario_direccion
                .AsNoTracking()
                .Where(d => d.id_usuario_direccion == id)  // <- EF parametriza este "id"
                .ToListAsync();

            return Ok(direcciones); // 200 con [] si no hay datos
        }

        private bool usuario_direccionExists(int id)
        {
            return _context.usuario_direccion.Any(e => e.id == id);
        }
    }
}
