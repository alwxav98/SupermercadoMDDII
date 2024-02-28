using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SupermercadoMDDII.Models
{
    [Table("Asistencia")]
    public partial class AsistenciaModel
    {

        [Key]
        public int Idasistencia { get; set; }

        [Required]
        [ForeignKey("Usuarios")]
        public int Idusuario { get; set; }

        [Required]
        public DateTime ingreso { get; set; }

        [Required]
        public DateTime salida { get; set; }

        
        public double? horasExtra { get; set; }

        public virtual UsuarioModel? Usuarios { get; set; }
    }
}
