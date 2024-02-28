using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermercadoMDDII.Models
{
    [Table("Usuario")]
    public partial class UsuarioModel
    {
        [Key]
        public int Idusuario { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "La cédula debe tener exactamente 10 dígitos.")]
        public string? cedula { get; set; }

        [Required]
        [MaxLength(100)]
        public string? nombre { get; set; }

        [Required]
        [MaxLength(100)]
        public string? apellido { get; set; }

        [MaxLength(200)]
        public string? direccion { get; set; }

        [MaxLength(10)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono debe tener exactamente 10 dígitos.")]
        public string? telefono { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "El campo Correo debe tener un formato válido.")]

        public string? correo { get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El sueldo debe tener hasta dos decimales.")]
        public decimal? sueldo { get; set; }

        [Required]
        public string? rol { get; set; }

        [Required]
        public string? clave { get; set; }

        [Required]
        public bool esActivo { get; set; }

        public DateTime? fechaRegistro { get; set; }

        // Navegación a las tablas relacionadas
        public virtual ICollection<VentaModel>? Ventas { get; set; }
        public virtual ICollection<AsistenciaModel>? Asistencias { get; set; }
    }
}
