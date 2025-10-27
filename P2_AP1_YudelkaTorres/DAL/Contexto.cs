using P2_AP1_YudelkaTorres.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace P2_AP1_YudelkaTorres.DAL;
public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }
    public DbSet<Registro> Registro { get; set; }
}
