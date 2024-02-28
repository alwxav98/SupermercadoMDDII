using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermercadoMDDII.Models
{
    [Table("Negocio")]
    public partial class NegocioModel
    {
        [Key]
        public int Idnegocio { get; set; }

        [MaxLength(13)]
        [Required(ErrorMessage = "El RUC es requerido.")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "El RUC debe tener exactamente 13 caracteres numéricos.")]
        public string? ruc { get; set; }

        [MaxLength(50)]
        public string? nombre { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "El correo es requerido.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
        public string? correo { get; set; }

        [MaxLength(50)]
        public string? direccion { get; set; }

        [MaxLength(10)]
        [Required(ErrorMessage = "El teléfono es requerido.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono debe tener exactamente 10 caracteres numéricos.")]
        public string? telefono { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El porcentaje de impuesto debe tener hasta dos decimales.")]
        public decimal? porcentajeImpuesto { get; set; }

    }
}
