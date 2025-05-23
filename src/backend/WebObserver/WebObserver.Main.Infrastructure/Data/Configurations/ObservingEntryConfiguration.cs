using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Text;
using WebObserver.Main.Domain.YouTubePlaylist;
using WebObserver.Main.Infrastructure.Data.Helpers;

namespace WebObserver.Main.Infrastructure.Data.Configurations;

public class ObservingEntryConfiguration : IEntityTypeConfiguration<ObservingEntryBase>
{
    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        Converters = { new DiffSummaryConverter() },
    };

    public void Configure(EntityTypeBuilder<ObservingEntryBase> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.DiffSummary)
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonSerializer.Serialize(v, _serializerOptions),
                v => JsonSerializer.Deserialize<DiffSummary>(v, _serializerOptions)!);
        
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

        builder.HasOne(x => x.LastDiff)
            .WithMany();
    }
}

public class YouTubePlaylistObservingEntryConfiguration : IEntityTypeConfiguration<YouTubePlaylistObservingEntry>
{
    public void Configure(EntityTypeBuilder<YouTubePlaylistObservingEntry> builder)
    {
        builder.HasOne(x => x.Payload)
            .WithOne()
            .HasForeignKey<YouTubePlaylistPayload>(x => x.ObservingEntryId);
        
        builder.HasOne(x => x.LastDiff)
            .WithMany();
    }
}