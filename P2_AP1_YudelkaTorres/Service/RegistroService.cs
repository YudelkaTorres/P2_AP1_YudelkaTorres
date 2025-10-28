using Microsoft.EntityFrameworkCore;
using P2_AP1_YudelkaTorres.DAL;
using P2_AP1_YudelkaTorres.Models;
using System.Linq.Expressions;

namespace P2_AP1_YudelkaTorres.Service;
public class RegistroService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task <List<Registro>> Listar (Expression<Func<Registro, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Registro
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}
