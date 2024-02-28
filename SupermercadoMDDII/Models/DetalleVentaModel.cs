using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermercadoMDDII.Models
{
    [Table("DetalleVenta ")]
    public partial class DetalleVentaModel
    {
        [Key]
        public int IddetalleVenta { get; set; }

        [Required]
        [ForeignKey("Ventas")]
        public int Idventa { get; set; }

        [Required]
        [ForeignKey("Productos")]
        public int Idproducto { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]

        public int cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El precio debe tener hasta dos decimales.")]
        public decimal precio { get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El total debe tener hasta dos decimales.")]

        public decimal total { get; set; }

        // Navegación a la tabla relacionada
        public virtual VentaModel? Ventas { get; set; }
        public virtual ProductoModel? Productos { get; set; }

    }
}
