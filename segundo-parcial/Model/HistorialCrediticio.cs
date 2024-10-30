using System;
namespace segundo_parcial.Model
{
	public class HistorialCrediticio
	{
		public int Id { get; set; }
        public string? Cedula { get; set; }
        public string? Rnc { get; set; }
        public string Concepto { get; set; }
        public DateTime Fecha { get; set; }
        public int MontoTotal { get; set; }
    }
}

