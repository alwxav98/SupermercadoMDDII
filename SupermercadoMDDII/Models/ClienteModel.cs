using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermercadoMDDII.Models
{
    [Table("Cliente")]
    public partial class ClienteModel
    {
        [Key]
        public int Idcliente { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "La cédula debe tener exactamente 10 caracteres.")]

        public string cedula { get; set; }

        [Required]
        [MaxLength(100)]
        public string nombre { get; set; }

        [Required]
        [MaxLength(100)]
        public string apellido { get; set; }

        [MaxLength(200)]
        public string? direccion { get; set; }

        [MaxLength(10)]
        public string? telefono { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Los puntos de recompensa deben ser mayores o iguales a 0.")]

        public int? puntosRecompensa { get; set; }

        public virtual ICollection<VentaModel>? Ventas { get; set; }
    }
}
