using Microsoft.EntityFrameworkCore;
using P2_AP1_YudelkaTorres.DAL;
using P2_AP1_YudelkaTorres.Models;
using System.Linq.Expressions;

namespace P2_AP1_YudelkaTorres.Service;
public class RegistroPedidosService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<List<Pedidos>> Listar(Expression<Func<Pedidos, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Pedidos
            .Include(p => p.Detalle)
            .ThenInclude(d => d.Componente)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<bool> Guardar(Pedidos pedido)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        bool existe = await Existe(pedido.PedidoId);

        if (!existe)
            contexto.Pedidos.Add(pedido);
        else
            contexto.Pedidos.Update(pedido);

        return await contexto.SaveChangesAsync() > 0;
    }
    private async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Pedidos.AnyAsync(p => p.PedidoId == id);
    }
    public async Task<Pedidos?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Pedidos
            .Include(p => p.Detalle)
            .ThenInclude(d => d.Componente)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PedidoId == id);
    }

    private async Task<bool> Insertar(Pedidos pedido)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        pedido.Total = pedido.Detalle.Sum(d => d.Precio * d.Cantidad);

        contexto.Pedidos.Add(pedido);

        foreach (var detalle in pedido.Detalle)
        {
            var componente = await contexto.Componentes.FindAsync(detalle.ComponenteId);
            if (componente != null)
            {
                if (componente.Existencia < detalle.Cantidad)
                    throw new InvalidOperationException($"No hay suficiente existencia para {componente.Descripcion}");

                componente.Existencia -= detalle.Cantidad;
                contexto.Componentes.Update(componente);
            }
        }
        return await contexto.SaveChangesAsync() > 0;
    }
    private async Task<bool> Modificar(Pedidos pedido)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var pedidoAnterior = await contexto.Pedidos
            .Include(p => p.Detalle)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PedidoId == pedido.PedidoId);

        if (pedidoAnterior == null)
            return false;

        foreach (var detalle in pedidoAnterior.Detalle)
        {
            var componente = await contexto.Componentes.FindAsync(detalle.ComponenteId);
            if (componente != null)
            {
                componente.Existencia += detalle.Cantidad;
                contexto.Componentes.Update(componente);
            }
        }

        await contexto.Database.ExecuteSqlRawAsync($"DELETE FROM PedidosDetalle WHERE PedidoId = {pedido.PedidoId}");

        pedido.Total = pedido.Detalle.Sum(d => d.Precio * d.Cantidad);

        contexto.Pedidos.Update(pedido);

        foreach (var detalle in pedido.Detalle)
        {
            var componente = await contexto.Componentes.FindAsync(detalle.ComponenteId);
            if (componente != null)
            {
                if (componente.Existencia < detalle.Cantidad)
                    throw new InvalidOperationException($"No hay suficiente existencia para {componente.Descripcion}");

                componente.Existencia -= detalle.Cantidad;
                contexto.Componentes.Update(componente);
            }
        }
        return await contexto.SaveChangesAsync() > 0;
    }
}
