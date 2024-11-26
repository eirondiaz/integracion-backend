using System;
using Microsoft.EntityFrameworkCore;
using segundo_parcial.Model;

namespace segundo_parcial.Context
{
	public class AppDbContext : DbContext
    {
		public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<TasaCambiaria> TasaCambiarias { get; set; }
        public DbSet<IndiceInflacion> IndiceInflaciones { get; set; }
        public DbSet<SaludFinanciera> SaludFinancieras { get; set; }
        public DbSet<HistorialCrediticio> HistorialCrediticios { get; set; }
        public DbSet<Counter> Counters { get; set; }
    }
}

