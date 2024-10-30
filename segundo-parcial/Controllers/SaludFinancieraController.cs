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
    [Route("api/salud-financiera")]
    [ApiController]
    public class SaludFinancieraController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SaludFinancieraController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SaludFinanciera
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaludFinanciera>>> GetSaludFinancieras()
        {
            return await _context.SaludFinancieras.ToListAsync();
        }

        // GET: api/SaludFinanciera/5
        [HttpGet("{search}")]
        public async Task<ActionResult<SaludFinanciera>> GetSaludFinanciera(string search)
        {
            var saludFinanciera = await _context.SaludFinancieras
                .Where(x => x.Cedula.Contains(search) || x.Rnc.Contains(search))
                .FirstOrDefaultAsync();

            if (saludFinanciera == null)
            {
                return NotFound();
            }

            return saludFinanciera;
        }

        // PUT: api/SaludFinanciera/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSaludFinanciera(int id, SaludFinanciera saludFinanciera)
        {
            if (id != saludFinanciera.Id)
            {
                return BadRequest();
            }

            _context.Entry(saludFinanciera).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaludFinancieraExists(id))
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

        // POST: api/SaludFinanciera
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SaludFinanciera>> PostSaludFinanciera(SaludFinanciera saludFinanciera)
        {
            _context.SaludFinancieras.Add(saludFinanciera);
            await _context.SaveChangesAsync();

            return Ok(saludFinanciera);
        }

        // DELETE: api/SaludFinanciera/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaludFinanciera(int id)
        {
            var saludFinanciera = await _context.SaludFinancieras.FindAsync(id);
            if (saludFinanciera == null)
            {
                return NotFound();
            }

            _context.SaludFinancieras.Remove(saludFinanciera);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SaludFinancieraExists(int id)
        {
            return _context.SaludFinancieras.Any(e => e.Id == id);
        }
    }
}
