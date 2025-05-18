using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Text;
using WebObserver.Main.Domain.YouTubePlaylist;

namespace WebObserver.Main.Infrastructure.Data.Configurations;

public class DiffConfiguration : IEntityTypeConfiguration<DiffBase>
{
    public void Configure(EntityTypeBuilder<DiffBase> builder)
    {
        builder.HasKey(x => new { x.FirstEntryId, x.SecondEntryId });
        builder.HasOne(x => x.FirstEntry)
            .WithMany()
            .HasForeignKey(x => x.FirstEntryId);
        
        builder.HasOne(x => x.SecondEntry)
            .WithMany()
            .HasForeignKey(x => x.SecondEntryId);
        
        builder.UseTpcMappingStrategy();
    }
}

public class TextDiffConfiguration : IEntityTypeConfiguration<Diff<TextDiffPayload>>
{
    public void Configure(EntityTypeBuilder<Diff<TextDiffPayload>> builder)
    {
        builder.HasBaseType<DiffBase>();
        
        builder.Property(x => x.Payload)
            .HasColumnType("jsonb");
    }
}

public class YouTubePlaylistDiffConfiguration : IEntityTypeConfiguration<Diff<YouTubePlaylistDiffPayload>>
{
    public void Configure(EntityTypeBuilder<Diff<YouTubePlaylistDiffPayload>> builder)
    {
        builder.HasBaseType<DiffBase>();
        
        builder.Property(x => x.Payload)
            .HasColumnType("jsonb");
    }
}