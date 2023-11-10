using LifoFifo.Models.orm.infrastructure;
using LifoFifo.Models.orm.moovment;
using LifoFifo.Models.orm.Object;
using LifoFifo.Models.orm.stock;
using Microsoft.EntityFrameworkCore;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    public DbSet<Input> Inputs{ get; set;}
    public DbSet<Output> Outputs{ get; set;}
    public DbSet<Moove> Mooves{ get; set;}
    public DbSet<Shop> Shops{ get; set;}
    public DbSet<Product> Products{ get; set;}
    public DbSet<VEtatStock> VEtatStocks { get; set; } 
}