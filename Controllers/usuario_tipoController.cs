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
    public class usuario_tipoController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public usuario_tipoController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/usuario_tipo
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<usuario_tipo>>> Getusuario_tipo()
        {
            return await _context.usuario_tipo.ToListAsync();
        }

        // GET: api/usuario_tipo/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<usuario_tipo>> Getusuario_tipo(int id)
        {
            var usuario_tipo = await _context.usuario_tipo.FindAsync(id);

            if (usuario_tipo == null)
            {
                return NotFound();
            }

            return usuario_tipo;
        }

        // PUT: api/usuario_tipo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Putusuario_tipo(int id, usuario_tipo usuario_tipo)
        {
            if (id != usuario_tipo.id)
            {
                return BadRequest();
            }

            _context.Entry(usuario_tipo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!usuario_tipoExists(id))
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

        // POST: api/usuario_tipo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<usuario_tipo>> Postusuario_tipo(usuario_tipo usuario_tipo)
        {
            _context.usuario_tipo.Add(usuario_tipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getusuario_tipo", new { id = usuario_tipo.id }, usuario_tipo);
        }

        // DELETE: api/usuario_tipo/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Deleteusuario_tipo(int id)
        {
            var usuario_tipo = await _context.usuario_tipo.FindAsync(id);
            if (usuario_tipo == null)
            {
                return NotFound();
            }

            _context.usuario_tipo.Remove(usuario_tipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool usuario_tipoExists(int id)
        {
            return _context.usuario_tipo.Any(e => e.id == id);
        }
    }
}
