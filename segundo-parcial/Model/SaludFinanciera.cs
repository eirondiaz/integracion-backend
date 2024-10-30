using System;
namespace segundo_parcial.Model
{
	public class SaludFinanciera
	{
		public int Id { get; set; }
		public string? Cedula { get; set; }
		public string? Rnc { get; set; }
		public string? Indicador { get; set; }
		public string Comentario { get; set; }
		public int MontoTotalAdeudado { get; set; }
	}
}

