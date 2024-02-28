using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermercadoMDDII.Models
{
    [Table("Pedido")]
    public partial class PedidoModel
    {
        [Key]
        public int Idpedido { get; set; }


        [Required]
        [ForeignKey("Productos")]
        public int Idproducto { get; set; }


        [Required]
        [ForeignKey("Proveedors")]
        public int Idproveedor { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El precio unitario debe tener hasta dos decimales y ser mayor a 0.")]
        public decimal precioU { get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El total debe tener hasta dos decimales y ser mayor a 0.")]
        public decimal total { get; set; }

        [Required]
        public DateTime fechaPedido { get; set; }

        [Required]
        public DateTime fechaEntrega { get; set; }

        public virtual ProductoModel? Productos { get; set; }
        public virtual ProveedorModel? Proveedors { get; set; }



    }
}
