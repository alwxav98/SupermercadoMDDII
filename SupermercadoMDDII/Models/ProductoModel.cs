using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermercadoMDDII.Models
{
    [Table("Producto")]
    public partial class ProductoModel
    {
        [Key]
        public int Idproducto { get; set; }


        [Required]
        [MaxLength(5, ErrorMessage = "El código de barras debe tener exactamente 5 caracteres.")]
        public string codigoBarra { get; set; }

        [Required]
        [MaxLength(50)]
        public string marca { get; set; }

        [Required]
        [MaxLength(100)]
        public string descripcion { get; set; }

        [Required]
        [ForeignKey("Categorias")]
        public int Idcategoria { get; set; }

        [Required]
        [ForeignKey("Proveedors")]
        public int Idproveedor { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a 0.")]
        public int stock { get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El precio debe tener hasta dos decimales y ser mayor o igual a 0.")]

        public decimal precio { get; set; }

        [Required]
        public bool esActivo { get; set; }

        public DateTime? fechaCaducidad { get; set; }

        // Navegación a las tablas relacionadas
        public virtual CategoriaModel? Categorias { get; set; }
        public virtual ProveedorModel? Proveedors { get; set; }

        public virtual ICollection<DetalleVentaModel>? DetalleVentas { get; set; }
        public virtual ICollection<PedidoModel>? Pedidos { get; set; }
    }
}
