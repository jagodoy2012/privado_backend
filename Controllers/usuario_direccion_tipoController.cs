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
    public class usuario_direccion_tipoController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public usuario_direccion_tipoController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/usuario_direccion_tipo
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<usuario_direccion_tipo>>> Getusuario_direccion_tipo()
        {
            return await _context.usuario_direccion_tipo.ToListAsync();
        }

        // GET: api/usuario_direccion_tipo/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<usuario_direccion_tipo>> Getusuario_direccion_tipo(int id)
        {
            var usuario_direccion_tipo = await _context.usuario_direccion_tipo.FindAsync(id);

            if (usuario_direccion_tipo == null)
            {
                return NotFound();
            }

            return usuario_direccion_tipo;
        }

        // PUT: api/usuario_direccion_tipo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Putusuario_direccion_tipo(int id, usuario_direccion_tipo usuario_direccion_tipo)
        {
            if (id != usuario_direccion_tipo.id)
            {
                return BadRequest();
            }

            _context.Entry(usuario_direccion_tipo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!usuario_direccion_tipoExists(id))
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

        // POST: api/usuario_direccion_tipo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<usuario_direccion_tipo>> Postusuario_direccion_tipo(usuario_direccion_tipo usuario_direccion_tipo)
        {
            _context.usuario_direccion_tipo.Add(usuario_direccion_tipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getusuario_direccion_tipo", new { id = usuario_direccion_tipo.id }, usuario_direccion_tipo);
        }

        // DELETE: api/usuario_direccion_tipo/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Deleteusuario_direccion_tipo(int id)
        {
            var usuario_direccion_tipo = await _context.usuario_direccion_tipo.FindAsync(id);
            if (usuario_direccion_tipo == null)
            {
                return NotFound();
            }

            _context.usuario_direccion_tipo.Remove(usuario_direccion_tipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool usuario_direccion_tipoExists(int id)
        {
            return _context.usuario_direccion_tipo.Any(e => e.id == id);
        }
    }
}
