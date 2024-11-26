using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using segundo_parcial.Context;
using segundo_parcial.Model;

namespace segundo_parcial.Controllers
{
    [Route("api/historial-crediticio")]
    [ApiController]
    public class HistorialCrediticioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HistorialCrediticioController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/HistorialCrediticio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialCrediticio>>> GetHistorialCrediticios()
        {
            await CreateCounter();

            return await _context.HistorialCrediticios.ToListAsync();
        }

        // GET: api/HistorialCrediticio/5
        [HttpGet("{search}")]
        public async Task<ActionResult<HistorialCrediticio>> GetHistorialCrediticio(string search)
        {
            await CreateCounter();

            var historialCrediticio = await _context.HistorialCrediticios
                .Where(x => x.Cedula.Contains(search) || x.Rnc.Contains(search))
                .FirstOrDefaultAsync();

            if (historialCrediticio == null)
            {
                return NotFound();
            }

            return historialCrediticio;
        }

        // PUT: api/HistorialCrediticio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistorialCrediticio(int id, HistorialCrediticio historialCrediticio)
        {
            await CreateCounter();

            if (id != historialCrediticio.Id)
            {
                return BadRequest();
            }

            _context.Entry(historialCrediticio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorialCrediticioExists(id))
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

        // POST: api/HistorialCrediticio
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HistorialCrediticio>> PostHistorialCrediticio(HistorialCrediticio historialCrediticio)
        {
            await CreateCounter();

            _context.HistorialCrediticios.Add(historialCrediticio);
            await _context.SaveChangesAsync();

            return Ok(historialCrediticio);
        }

        // DELETE: api/HistorialCrediticio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorialCrediticio(int id)
        {
            await CreateCounter();

            var historialCrediticio = await _context.HistorialCrediticios.FindAsync(id);
            if (historialCrediticio == null)
            {
                return NotFound();
            }

            _context.HistorialCrediticios.Remove(historialCrediticio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistorialCrediticioExists(int id)
        {
            return _context.HistorialCrediticios.Any(e => e.Id == id);
        }

        private async Task CreateCounter()
        {
            var counter = await _context.Counters.Where(x => x.Service == "Historial Crediticio").FirstOrDefaultAsync();

            if (counter is null)
            {
                await _context.Counters.AddAsync(new Counter { Service = "Historial Crediticio", Count = 1 });
            }
            else
            {
                counter.Count += 1;
            }

            await _context.SaveChangesAsync();
        }
    }
}
