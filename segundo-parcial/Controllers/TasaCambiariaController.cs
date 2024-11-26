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
    [Route("api/tasa-cambiaria")]
    [ApiController]
    public class TasaCambiariaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasaCambiariaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TasaCambiaria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TasaCambiaria>>> GetTasaCambiarias()
        {
            await CreateCounter();

            return await _context.TasaCambiarias.ToListAsync();
        }

        // GET: api/TasaCambiaria/5
        [HttpGet("{moneda}")]
        public async Task<ActionResult<TasaCambiaria>> GetTasaCambiaria(string moneda)
        {
            await CreateCounter();

            var tasaCambiaria = await _context.TasaCambiarias
                .Where(x => x.CodigoMoneda.ToLower() == moneda.ToLower())
                .FirstOrDefaultAsync();

            return Ok(tasaCambiaria);
        }

        // PUT: api/TasaCambiaria/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTasaCambiaria(int id, TasaCambiaria tasaCambiaria)
        {
            await CreateCounter();

            if (id != tasaCambiaria.Id)
            {
                return BadRequest();
            }

            _context.Entry(tasaCambiaria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TasaCambiariaExists(id))
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

        // POST: api/TasaCambiaria
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TasaCambiaria>> PostTasaCambiaria(TasaCambiaria tasaCambiaria)
        {
            await CreateCounter();

            _context.TasaCambiarias.Add(tasaCambiaria);
            await _context.SaveChangesAsync();

            return Ok(tasaCambiaria);
        }

        // DELETE: api/TasaCambiaria/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasaCambiaria(int id)
        {
            await CreateCounter();

            var tasaCambiaria = await _context.TasaCambiarias.FindAsync(id);
            if (tasaCambiaria == null)
            {
                return NotFound();
            }

            _context.TasaCambiarias.Remove(tasaCambiaria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TasaCambiariaExists(int id)
        {
            return _context.TasaCambiarias.Any(e => e.Id == id);
        }

        private async Task CreateCounter()
        {
            var counter = await _context.Counters.Where(x => x.Service == "Tasa Cambiaria").FirstOrDefaultAsync();

            if (counter is null)
            {
                await _context.Counters.AddAsync(new Counter { Service = "Tasa Cambiaria", Count = 1 });
            }
            else
            {
                counter.Count += 1;
            }

            await _context.SaveChangesAsync();
        }
    }
}
