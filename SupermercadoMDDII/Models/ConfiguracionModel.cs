using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermercadoMDDII.Models
{
    [Table("Configuracion ")]
    public partial class ConfiguracionModel
    {
        [Key]
        public int Idconfiguracion { get; set; }

        [MaxLength(50)]
        public string? recurso { get; set; }

        [MaxLength(50)]
        public string? propiedad { get; set; }

        [MaxLength(60)]
        public string? valor { get; set; }

    }
}
