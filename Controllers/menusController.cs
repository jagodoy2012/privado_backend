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
using privado_backend.Models.DTOs;
using privado_backend.Services;

namespace privado_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class menusController : ControllerBase
    {
        private readonly privado_backendContext _context;
        private readonly IMenuService _menuService;

        public menusController(privado_backendContext context, IMenuService menuService)
        {
            _context = context;
            _menuService = menuService;
        }

        // GET: api/menus
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<menu>>> Getmenu()
        {
            return await _context.menu.ToListAsync();
        }

        // GET: api/menus/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<menu>> Getmenu(int id)
        {
            var menu = await _context.menu.FindAsync(id);

            if (menu == null)
            {
                return NotFound();
            }

            return menu;
        }

        // PUT: api/menus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Putmenu(int id, menu menu)
        {
            if (id != menu.id)
            {
                return BadRequest();
            }

            _context.Entry(menu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!menuExists(id))
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

        // POST: api/menus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<menu>> Postmenu(menu menu)
        {
            _context.menu.Add(menu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getmenu", new { id = menu.id }, menu);
        }

        // DELETE: api/menus/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Deletemenu(int id)
        {
            var menu = await _context.menu.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            _context.menu.Remove(menu);
            await _context.SaveChangesAsync();

            return NoContent();
        }
      
          

            // GET api/menu/tipo/5
            [HttpGet("tipo/{idUsuarioTipo:int}")]
            [Authorize]

        public async Task<ActionResult<List<MenuNodeDto>>> GetByTipo(int idUsuarioTipo)
            {
                var tree = await _menuService.GetMenuForTipoAsync(idUsuarioTipo);
                return Ok(tree);
            }
        

        private bool menuExists(int id)
        {
            return _context.menu.Any(e => e.id == id);
        }
    }
}
