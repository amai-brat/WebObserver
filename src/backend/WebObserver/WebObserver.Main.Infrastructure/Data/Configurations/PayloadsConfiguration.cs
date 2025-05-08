using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Text;
using WebObserver.Main.Domain.YouTubePlaylist;

namespace WebObserver.Main.Infrastructure.Data.Configurations;

public class ObseringPayloadConfiguration : IEntityTypeConfiguration<ObservingPayload>
{
    public void Configure(EntityTypeBuilder<ObservingPayload> builder)
    {
        builder.HasKey(x => x.ObservingId);
        builder.HasOne<ObservingEntryBase>()
            .WithOne()
            .HasForeignKey<ObservingPayload>(x => x.ObservingId);
        
        builder.UseTpcMappingStrategy();
    }
}


public class TextPayloadConfiguration : IEntityTypeConfiguration<TextPayload>
{
    public void Configure(EntityTypeBuilder<TextPayload> builder)
    {
        builder.Property(x => x.Text)
            .HasMaxLength(-1);
    }
}

public class YouTubePlaylistPayloadConfiguration : IEntityTypeConfiguration<YouTubePlaylistPayload>
{
    public void Configure(EntityTypeBuilder<YouTubePlaylistPayload> builder)
    {
        builder.HasMany(x => x.Items)
            .WithMany();
    }
}

public class YouTubePlaylistItemConfiguration : IEntityTypeConfiguration<YouTubePlaylistItem>
{
    public void Configure(EntityTypeBuilder<YouTubePlaylistItem> builder)
    {
        builder.HasKey(x => x.VideoId);
    }
}