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
    [Route("api/counter")]
    [ApiController]
    public class CounterController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CounterController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/HistorialCrediticio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Counter>>> Get()
        {
            return await _context.Counters.ToListAsync();
        }
    }
}
