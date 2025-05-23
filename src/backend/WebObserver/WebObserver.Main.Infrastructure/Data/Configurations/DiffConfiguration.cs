using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Text;
using WebObserver.Main.Domain.YouTubePlaylist;
using WebObserver.Main.Infrastructure.Data.Helpers;

namespace WebObserver.Main.Infrastructure.Data.Configurations;

public class DiffConfiguration : IEntityTypeConfiguration<DiffBase>
{
    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        Converters = { 
            new DerivedConverter<DiffPayload>()
        },
    };
    
    public void Configure(EntityTypeBuilder<DiffBase> builder)
    {
        builder.HasKey(x => new { x.FirstEntryId, x.SecondEntryId });
        builder.HasOne(x => x.FirstEntry)
            .WithMany()
            .HasForeignKey(x => x.FirstEntryId);
        
        builder.HasOne(x => x.SecondEntry)
            .WithMany()
            .HasForeignKey(x => x.SecondEntryId);
        
        builder.Property(x => x.Payload)
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonSerializer.Serialize(v, _serializerOptions),
                v => JsonSerializer.Deserialize<DiffPayload>(v, _serializerOptions)!);
        
        builder.UseTpcMappingStrategy();
    }
}

public class TextDiffConfiguration : IEntityTypeConfiguration<TextDiff>
{
    public void Configure(EntityTypeBuilder<TextDiff> builder)
    {
        builder.HasBaseType<DiffBase>();
        
        builder.Property(x => x.Payload)
            .HasColumnType("jsonb");
    }
}

public class YouTubePlaylistDiffConfiguration : IEntityTypeConfiguration<YouTubePlaylistDiff>
{
    public void Configure(EntityTypeBuilder<YouTubePlaylistDiff> builder)
    {
        builder.HasBaseType<DiffBase>();
        
        builder.Property(x => x.Payload)
            .HasColumnType("jsonb");
    }
}