namespace SupermercadoMDDII.Models
{
    public class FacturaViewModel
    {
        public int IdVenta { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal? Total { get; set; }
        public DateTime? FechaVenta { get; set; }
    }
}
