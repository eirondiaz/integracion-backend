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
    [Route("api/indice-inflacion")]
    [ApiController]
    public class IndiceInflacionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IndiceInflacionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/IndiceInflacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IndiceInflacion>>> GetIndiceInflaciones()
        {
            await CreateCounter();

            return await _context.IndiceInflaciones.ToListAsync();
        }

        // GET: api/IndiceInflacion/5
        [HttpGet("{fecha}")]
        public async Task<ActionResult<IndiceInflacion>> GetIndiceInflacion(DateTime fecha)
        {
            await CreateCounter();

            var indiceInflacion = await _context.IndiceInflaciones
                .Where(x => x.Periodo.Year == fecha.Year && x.Periodo.Month == fecha.Month)
                .FirstOrDefaultAsync();

            return indiceInflacion;
        }

        // PUT: api/IndiceInflacion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIndiceInflacion(int id, IndiceInflacion indiceInflacion)
        {
            await CreateCounter();

            if (id != indiceInflacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(indiceInflacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IndiceInflacionExists(id))
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

        // POST: api/IndiceInflacion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IndiceInflacion>> PostIndiceInflacion(IndiceInflacion indiceInflacion)
        {
            await CreateCounter();

            _context.IndiceInflaciones.Add(indiceInflacion);
            await _context.SaveChangesAsync();

            return Ok(indiceInflacion);
        }

        // DELETE: api/IndiceInflacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIndiceInflacion(int id)
        {
            await CreateCounter();

            var indiceInflacion = await _context.IndiceInflaciones.FindAsync(id);
            if (indiceInflacion == null)
            {
                return NotFound();
            }

            _context.IndiceInflaciones.Remove(indiceInflacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IndiceInflacionExists(int id)
        {
            return _context.IndiceInflaciones.Any(e => e.Id == id);
        }

        private async Task CreateCounter()
        {
            var counter = await _context.Counters.Where(x => x.Service == "Indice Inflacion").FirstOrDefaultAsync();

            if (counter is null)
            {
                await _context.Counters.AddAsync(new Counter { Service = "Indice Inflacion", Count = 1 });
            }
            else
            {
                counter.Count += 1;
            }

            await _context.SaveChangesAsync();
        }
    }
}
