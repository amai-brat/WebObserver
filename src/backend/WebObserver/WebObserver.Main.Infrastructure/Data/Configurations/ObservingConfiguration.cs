using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Text;
using WebObserver.Main.Domain.YouTubePlaylist;

namespace WebObserver.Main.Infrastructure.Data.Configurations;

public class ObservingConfiguration : IEntityTypeConfiguration<ObservingBase>
{
    public void Configure(EntityTypeBuilder<ObservingBase> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Template)
            .WithMany()
            .HasForeignKey(x => x.TemplateId);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Observings)
            .HasForeignKey(x => x.UserId);
        
        builder.UseTphMappingStrategy();
    }
}

public class YouTubeObservingConfiguration : IEntityTypeConfiguration<YouTubePlaylistObserving>
{
    public void Configure(EntityTypeBuilder<YouTubePlaylistObserving> builder)
    {
        builder.HasMany(x => x.Entries)
            .WithOne()
            .HasForeignKey(x => x.ObservingId);

        builder.HasMany(x => x.UnavailableItems)
            .WithMany();
    }
}

public class TextObservingConfiguration : IEntityTypeConfiguration<TextObserving>
{
    public void Configure(EntityTypeBuilder<TextObserving> builder)
    {
        builder.HasMany(x => x.Entries)
            .WithOne()
            .HasForeignKey(x => x.ObservingId);
    }
}