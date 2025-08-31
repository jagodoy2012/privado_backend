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
    public class usuario_tipo_menuController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public usuario_tipo_menuController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/usuario_tipo_menu
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<usuario_tipo_menu>>> Getusuario_tipo_menu()
        {
            return await _context.usuario_tipo_menu.ToListAsync();
        }

        // GET: api/usuario_tipo_menu/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<usuario_tipo_menu>> Getusuario_tipo_menu(int id)
        {
            var usuario_tipo_menu = await _context.usuario_tipo_menu.FindAsync(id);

            if (usuario_tipo_menu == null)
            {
                return NotFound();
            }

            return usuario_tipo_menu;
        }

        // PUT: api/usuario_tipo_menu/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Putusuario_tipo_menu(int id, usuario_tipo_menu usuario_tipo_menu)
        {
            if (id != usuario_tipo_menu.id)
            {
                return BadRequest();
            }

            _context.Entry(usuario_tipo_menu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!usuario_tipo_menuExists(id))
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

        // POST: api/usuario_tipo_menu
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<usuario_tipo_menu>> Postusuario_tipo_menu(usuario_tipo_menu usuario_tipo_menu)
        {
            _context.usuario_tipo_menu.Add(usuario_tipo_menu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getusuario_tipo_menu", new { id = usuario_tipo_menu.id }, usuario_tipo_menu);
        }

        // DELETE: api/usuario_tipo_menu/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Deleteusuario_tipo_menu(int id)
        {
            var usuario_tipo_menu = await _context.usuario_tipo_menu.FindAsync(id);
            if (usuario_tipo_menu == null)
            {
                return NotFound();
            }

            _context.usuario_tipo_menu.Remove(usuario_tipo_menu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool usuario_tipo_menuExists(int id)
        {
            return _context.usuario_tipo_menu.Any(e => e.id == id);
        }
    }
}
