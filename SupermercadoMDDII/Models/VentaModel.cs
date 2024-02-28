using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermercadoMDDII.Models
{
    [Table("Venta")]
    public partial class VentaModel
    {
        [Key]
        public int Idventa { get; set; }

        [Required]
        [ForeignKey("Usuarios")]
        public int Idusuario { get; set; }

        [Required]
        [ForeignKey("Clientes")]
        public int Idcliente { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El subTotal debe tener hasta dos decimales.")]
        public decimal? subTotal { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El impuestoTotal debe tener hasta dos decimales.")]

        public decimal? impuestoTotal { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El Total debe tener hasta dos decimales.")]

        public decimal? Total { get; set; }

        public DateTime? fechaRegistro { get; set; }

        // Navegación a las tablas relacionadas
        public virtual UsuarioModel? Usuarios { get; set; }
        public virtual ClienteModel? Clientes { get; set; }

        public virtual ICollection<DetalleVentaModel>? DetalleVentas { get; set; }
    }
}
