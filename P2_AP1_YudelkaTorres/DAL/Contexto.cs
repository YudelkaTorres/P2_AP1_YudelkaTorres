using P2_AP1_YudelkaTorres.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace P2_AP1_YudelkaTorres.DAL;
public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }
    public DbSet<Pedidos> Pedidos { get; set; }
    public DbSet<PedidosDetalle> Pedidosdetalle { get; set; }
    public DbSet<Componente> Componentes { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Componente>(entity =>
        {
            entity.HasData(
                new Componente
                {
                    ComponenteId = 1,
                    Descripcion = "Memoria 4GB",
                    Precio = 1580,
                    Existencia = 1
                },
                new Componente
                {
                    ComponenteId = 2,
                    Descripcion = "Disco SSD 120MB",
                    Precio = 4200,
                    Existencia = 8
                },
                new Componente
                {
                    ComponenteId = 3,
                    Descripcion = "Tarjeta de Video",
                    Precio = 10000,
                    Existencia = 4
                });
        });
    }
}
