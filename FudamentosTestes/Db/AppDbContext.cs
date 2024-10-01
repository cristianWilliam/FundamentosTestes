using FudamentosTestes.Entities;
using Microsoft.EntityFrameworkCore;

namespace FudamentosTestes.Db;

internal sealed class AppDbContext : DbContext
{
    public DbSet<Car> Cars { get; init; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        :base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}