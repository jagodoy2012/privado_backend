using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using privado_backend.Data;
using privado_backend.Models.SistemasPrivado;

namespace privado_backend.Controllers.admin_sistemas
{
    [Route("api/[controller]")]
    [ApiController]
    public class TRANSACCIONES_COMPRAS_VENTASController : ControllerBase
    {
        private readonly privado_backendContext _context;

        public TRANSACCIONES_COMPRAS_VENTASController(privado_backendContext context)
        {
            _context = context;
        }

        // GET: api/TRANSACCIONES_COMPRAS_VENTAS
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TRANSACCIONES_COMPRAS_VENTAS>>> GetTRANSACCIONES_COMPRAS_VENTAS()
        {
            return await _context.TRANSACCIONES_COMPRAS_VENTAS.ToListAsync();
        }

        // GET: api/TRANSACCIONES_COMPRAS_VENTAS/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TRANSACCIONES_COMPRAS_VENTAS>> GetTRANSACCIONES_COMPRAS_VENTAS(int id)
        {
            var tRANSACCIONES_COMPRAS_VENTAS = await _context.TRANSACCIONES_COMPRAS_VENTAS.FindAsync(id);

            if (tRANSACCIONES_COMPRAS_VENTAS == null)
            {
                return NotFound();
            }

            return tRANSACCIONES_COMPRAS_VENTAS;
        }

        // PUT: api/TRANSACCIONES_COMPRAS_VENTAS/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTRANSACCIONES_COMPRAS_VENTAS(int id, TRANSACCIONES_COMPRAS_VENTAS tRANSACCIONES_COMPRAS_VENTAS)
        {
            if (id != tRANSACCIONES_COMPRAS_VENTAS.id)
            {
                return BadRequest();
            }

            _context.Entry(tRANSACCIONES_COMPRAS_VENTAS).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TRANSACCIONES_COMPRAS_VENTASExists(id))
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

        // POST: api/TRANSACCIONES_COMPRAS_VENTAS
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TRANSACCIONES_COMPRAS_VENTAS>> PostTRANSACCIONES_COMPRAS_VENTAS(TRANSACCIONES_COMPRAS_VENTAS tRANSACCIONES_COMPRAS_VENTAS)
        {
            _context.TRANSACCIONES_COMPRAS_VENTAS.Add(tRANSACCIONES_COMPRAS_VENTAS);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTRANSACCIONES_COMPRAS_VENTAS", new { id = tRANSACCIONES_COMPRAS_VENTAS.id }, tRANSACCIONES_COMPRAS_VENTAS);
        }

        // DELETE: api/TRANSACCIONES_COMPRAS_VENTAS/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTRANSACCIONES_COMPRAS_VENTAS(int id)
        {
            var tRANSACCIONES_COMPRAS_VENTAS = await _context.TRANSACCIONES_COMPRAS_VENTAS.FindAsync(id);
            if (tRANSACCIONES_COMPRAS_VENTAS == null)
            {
                return NotFound();
            }

            _context.TRANSACCIONES_COMPRAS_VENTAS.Remove(tRANSACCIONES_COMPRAS_VENTAS);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // GET: api/TRANSACCIONES_COMPRAS_VENTAS/rango-fechas?desde=2025-01-01&hasta=2025-01-31
        [HttpGet("rango-fechas")]
        public async Task<IActionResult> GetPorRangoFechas(
            [FromQuery] DateTime? desde,
            [FromQuery] DateTime? hasta,
            [FromQuery] int? id_tienda = null,
            [FromQuery] int? id_usuario = null,
            [FromQuery] int? id_transaccion_tipo = null,
            [FromQuery] int? id_transaccion_movimiento = null,
            [FromQuery] int? estado = 1,    // null = no filtrar por estado, 1 = activos por defecto
            [FromQuery] int skip = 0,
            [FromQuery] int take = 100
        )
        {
            if (desde is null || hasta is null)
                return BadRequest("Debe enviar 'desde' y 'hasta' en formato YYYY-MM-DD.");

            // Normalizamos a día completo (inclusive)
            var inicio = desde.Value.Date;                      // 00:00:00
            var finExclusivo = hasta.Value.Date.AddDays(1);     // < finExclusivo  (equivalente a <= 23:59:59.9999999)

            var query = _context.TRANSACCIONES_COMPRAS_VENTAS
                .AsNoTracking()
                .Where(t => t.fecha >= inicio && t.fecha < finExclusivo);

            if (estado.HasValue) query = query.Where(t => t.estado == estado);
            if (id_tienda.HasValue) query = query.Where(t => t.id_tienda == id_tienda);
            if (id_usuario.HasValue) query = query.Where(t => t.id_usuario == id_usuario);
            if (id_transaccion_tipo.HasValue) query = query.Where(t => t.id_transaccion_tipo == id_transaccion_tipo);
            if (id_transaccion_movimiento.HasValue)
                query = query.Where(t => t.id_transaccion_movimiento == id_transaccion_movimiento);

            var totalReg = await query.CountAsync();
            var sumaTotal = await query.SumAsync(t => (decimal?)(t.total ?? 0m)) ?? 0m;

            var items = await query
                .OrderBy(t => t.fecha)
                .Skip(Math.Max(0, skip))
                .Take(Math.Clamp(take, 1, 500))
                .ToListAsync();

            return Ok(new
            {
                ok = true,
                desde = inicio,
                hasta = finExclusivo.AddTicks(-1), // informativo
                total_registros = totalReg,
                suma_total = sumaTotal,
                items
            });
        }


        private bool TRANSACCIONES_COMPRAS_VENTASExists(int id)
        {
            return _context.TRANSACCIONES_COMPRAS_VENTAS.Any(e => e.id == id);
        }
    }
}
