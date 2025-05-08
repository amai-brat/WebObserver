using Microsoft.EntityFrameworkCore;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Entities;
using WebObserver.Main.Domain.Text;
using WebObserver.Main.Domain.YouTubePlaylist;

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
        Seed(modelBuilder);
    }

    private static void Seed(ModelBuilder mb)
    {
        mb.Entity<YouTubePlaylistObservingTemplate>().HasData(new YouTubePlaylistObservingTemplate
        {
            Id = 1,
            Name = "YouTube плейлист",
            Description = "Отслеживает за изменениями YouTube плейлиста: добавление, удаление, изменение ролика, недоступность ролика",
        });

        mb.Entity<TextObservingTemplate>().HasData(new TextObservingTemplate
        {
            Id = 2,
            Name = "Текстовый файл",
            Description = "Отслеживает за текстовым файлом (не бинари)"
        });
    }
}