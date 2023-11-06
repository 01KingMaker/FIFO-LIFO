using FIFO_LIFO.Models.orm.Mouvement;

namespace FIFO_LIFO.Models.orm;

using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    public DbSet<MouvementEntre> MouvementEntres { get; set; }
}