using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using privado_backend.Data;
using privado_backend.Models.DTOs;

namespace privado_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class remesasController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public remesasController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/remesas
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<remesa>>> Getremesa()
        {
            return await _context.remesa.ToListAsync();
        }

        // GET: api/remesas/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<remesa>> Getremesa(int id)
        {
            var remesa = await _context.remesa.FindAsync(id);

            if (remesa == null)
            {
                return NotFound();
            }

            return remesa;
        }

        // PUT: api/remesas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Putremesa(int id, remesa remesa)
        {
            if (id != remesa.id)
            {
                return BadRequest();
            }

            _context.Entry(remesa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!remesaExists(id))
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

        // POST: api/remesas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<remesa>> Postremesa(remesa remesa)
        {
            _context.remesa.Add(remesa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getremesa", new { id = remesa.id }, remesa);
        }

        // DELETE: api/remesas/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Deleteremesa(int id)
        {
            var remesa = await _context.remesa.FindAsync(id);
            if (remesa == null)
            {
                return NotFound();
            }

            _context.remesa.Remove(remesa);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // GET: api/remesas/by-nopago/ABC123
        [HttpGet("by-nopago/{no_pago}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<remesa>>> GetByNoPago(string no_pago)
        {
            var rows = await _context.remesa
                .AsNoTracking()
                .Where(r => r.no_pago == no_pago)   // usa Contains si quieres búsqueda parcial
                .OrderByDescending(r => r.fecha)
                .ToListAsync();

            return Ok(rows);
        }
        // GET: api/remesas/by-usuario/7
        [HttpGet("by-usuario/{idUsuario:int}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<RemesaDto>>> GetByUsuario(int idUsuario)
        {
            var list = await _context.remesa
                .AsNoTracking()
                .Where(r => r.id_producto_bancario_usuario == idUsuario   // aparece como “envía”
                         || r.id_usuario == idUsuario)                    // aparece como “recibe”
                .OrderByDescending(r => r.fecha)
                .Select(r => new RemesaDto
                {
                    id = r.id,
                    id_producto_bancario_usuario = r.id_producto_bancario_usuario,
                    id_usuario = r.id_usuario,
                    nombre_remitente = r.nombre_remitente,
                    nombre_receptor = r.nombre_receptor,
                    monto = r.monto,
                    no_pago = r.no_pago,
                    estado = r.estado,
                    fecha = r.fecha_envio,
                    id_moneda_tipo = r.id_moneda_tipo,
                    tipo = (r.id_producto_bancario_usuario == idUsuario) ? "envia" : "recibe"
                })
                .ToListAsync();

            return Ok(list);
        }
        private bool remesaExists(int id)
        {
            return _context.remesa.Any(e => e.id == id);
        }
    }
}
