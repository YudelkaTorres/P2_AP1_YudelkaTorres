using System.ComponentModel.DataAnnotations;

namespace P2_AP1_YudelkaTorres.Models;
public class Pedidos
{
    [Key]
    public int PedidoId { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Debe ingresar el nombre del cliente")]
    public string NombreCompleto { get; set; }
    public decimal Total {get; set;}
    public virtual List<PedidosDetalle> Detalle { get; set; } = new();
}
