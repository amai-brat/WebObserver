using Microsoft.EntityFrameworkCore;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Entities;

namespace WebObserver.Main.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<ObservingTemplate> Templates => Set<ObservingTemplate>();
    public DbSet<ObservingBase> Observings => Set<ObservingBase>();
    public DbSet<ObservingEntryBase> ObservingEntries => Set<ObservingEntryBase>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}