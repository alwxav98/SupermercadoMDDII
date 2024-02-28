using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermercadoMDDII.Models
{
    [Table("Proveedor")]
    public partial class ProveedorModel
    {
        [Key]
        public int Idproveedor { get; set; }

        [Required]
        [MaxLength(13, ErrorMessage = "El RUC debe tener exactamente 13 caracteres.")]
        public string ruc { get; set; }

        [Required]
        [MaxLength(100)]
        public string nombre { get; set; }

        [Required]
        [MaxLength(100)]
        public string apellido { get; set; }

        [MaxLength(100)]
        public string? direccion { get; set; }

        [MaxLength(10, ErrorMessage = "El teléfono debe tener exactamente 10 caracteres.")]
        [MinLength(10, ErrorMessage = "El teléfono debe tener exactamente 10 caracteres.")]
        public string? telefono { get; set; }

        [EmailAddress(ErrorMessage = "El campo Correo debe tener un formato válido.")]
        public string correo { get; set; }

        public virtual ICollection<PedidoModel>? Pedidos { get; set; }
        public virtual ICollection<ProductoModel>? Productos { get; set; }

    }
}
