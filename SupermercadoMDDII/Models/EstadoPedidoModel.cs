using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermercadoMDDII.Models
{
    [Table("EstadoPedido")]
    public partial class EstadoPedidoModel
    {
        [Key]
        public int IdestadoPedido { get; set; }

        [Required]
        [ForeignKey("Pedidos")]
        public int Idpedido { get; set; }

        [Required]
        public string? estado { get; set; }

        public string? calificiacion { get; set; }

        public virtual PedidoModel? Pedidos { get; set; }
    }
}
