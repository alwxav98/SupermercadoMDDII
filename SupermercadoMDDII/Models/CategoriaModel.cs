using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermercadoMDDII.Models
{
    [Table("Categoria")]
    public partial class CategoriaModel
    {
        [Key]
        public int Idcategoria { get; set; }
    

        [MaxLength(50)]
        public string? descripcion { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal? impuesto { get; set; }

        public virtual ICollection<ProductoModel>? Productos { get; set; }

    }
}
