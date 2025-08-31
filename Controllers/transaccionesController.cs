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
    public class transaccionesController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public transaccionesController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/transacciones
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<transacciones>>> Gettransacciones()
        {
            return await _context.transacciones.ToListAsync();
        }

        // GET: api/transacciones/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<transacciones>> Gettransacciones(int id)
        {
            var transacciones = await _context.transacciones.FindAsync(id);

            if (transacciones == null)
            {
                return NotFound();
            }

            return transacciones;
        }

        // PUT: api/transacciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Puttransacciones(int id, transacciones transacciones)
        {
            if (id != transacciones.id)
            {
                return BadRequest();
            }

            _context.Entry(transacciones).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!transaccionesExists(id))
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

        // POST: api/transacciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<transacciones>> Posttransacciones(transacciones transacciones)
        {
            _context.transacciones.Add(transacciones);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Gettransacciones", new { id = transacciones.id }, transacciones);
        }

        // DELETE: api/transacciones/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Deletetransacciones(int id)
        {
            var transacciones = await _context.transacciones.FindAsync(id);
            if (transacciones == null)
            {
                return NotFound();
            }

            _context.transacciones.Remove(transacciones);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // GET: api/transacciones/by-cuenta/123
        [HttpGet("by-cuenta/{idCuenta:int}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TransaccionDto>>> GetByCuenta(int idCuenta)
        {
            var list = await _context.transacciones
                .AsNoTracking()
                .Where(t => t.id_producto_bancario_usuario_envia == idCuenta
                         || t.id_producto_bancario_usuario_recibe == idCuenta)
                .OrderByDescending(t => t.fecha_realizado ?? t.fecha)
                .Select(t => new TransaccionDto
                {
                    id = t.id,
                    id_producto_bancario_usuario_envia = (int)t.id_producto_bancario_usuario_envia,
                    id_producto_bancario_usuario_recibe = (int)t.id_producto_bancario_usuario_recibe,
                    id_operaciones = (int)t.id_operaciones,
                    monto = t.monto,
                    id_moneda_tipo = t.id_moneda_tipo,
                    cambio = t.cambio,
                    nota = t.nota,
                    fecha_realizado = t.fecha_realizado,
                    estado = t.estado,
                    fecha = t.fecha,
                    tipo = (t.id_producto_bancario_usuario_envia == idCuenta) ? "Transferencia" : "Deposito"
                })
                .ToListAsync();

            return Ok(list); // 200 con [] si no hay
        }

        private bool transaccionesExists(int id)
        {
            return _context.transacciones.Any(e => e.id == id);
        }
    }
}
