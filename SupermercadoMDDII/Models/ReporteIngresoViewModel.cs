namespace SupermercadoMDDII.Models
{
    public class ReporteIngresoViewModel
    {
        public int IdVenta { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        
        public decimal? ValorTotalPagado { get; set; }
        public DateTime? FechaVenta { get; set; }
    }
}
