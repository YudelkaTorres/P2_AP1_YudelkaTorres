using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace P2_AP1_YudelkaTorres.Models;
public class Componente
{
    [Key]
    public int ComponenteId { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int Existencia { get; set; }
}
