using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Text;
using WebObserver.Main.Domain.YouTubePlaylist;

namespace WebObserver.Main.Infrastructure.Data.Configurations;

public class ObservingEntryConfiguration : IEntityTypeConfiguration<ObservingEntryBase>
{
    public void Configure(EntityTypeBuilder<ObservingEntryBase> builder)
    {
        builder.HasKey(x => x.Id);
        builder.UseTphMappingStrategy();
    }
}

public class TextObservingEntryConfiguration : IEntityTypeConfiguration<TextObservingEntry>
{
    public void Configure(EntityTypeBuilder<TextObservingEntry> builder)
    {
        builder.HasOne(x => x.Payload)
            .WithOne()
            .HasForeignKey<TextPayload>(x => x.ObservingEntryId);
    }
}

public class YouTubePlaylistObservingEntryConfiguration : IEntityTypeConfiguration<YouTubePlaylistObservingEntry>
{
    public void Configure(EntityTypeBuilder<YouTubePlaylistObservingEntry> builder)
    {
        builder.HasOne(x => x.Payload)
            .WithOne()
            .HasForeignKey<YouTubePlaylistPayload>(x => x.ObservingEntryId);
    }
}