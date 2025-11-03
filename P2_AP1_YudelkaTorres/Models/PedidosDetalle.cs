using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P2_AP1_YudelkaTorres.Models;
public class PedidosDetalle
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int PedidoId { get; set; }

    [Required]
    public int ComponenteId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero")]
    public int Cantidad { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero")]
    public decimal Precio { get; set; }

    [ForeignKey("PedidoId")]
    public Pedidos Pedido { get; set; }

    [ForeignKey("ComponenteId")]
    public Componente Componente { get; set; }
}
