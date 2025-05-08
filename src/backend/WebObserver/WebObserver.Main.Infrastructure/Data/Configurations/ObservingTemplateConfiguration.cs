using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Text;
using WebObserver.Main.Domain.YouTubePlaylist;

namespace WebObserver.Main.Infrastructure.Data.Configurations;

public class ObservingTemplateConfiguration : IEntityTypeConfiguration<ObservingTemplate>
{
    public void Configure(EntityTypeBuilder<ObservingTemplate> builder)
    {
        builder.HasKey(x => x.Id);

        builder.UseTphMappingStrategy();
    }
}

public class TextObservingTemplateConfiguration : IEntityTypeConfiguration<TextObservingTemplate>
{
    public void Configure(EntityTypeBuilder<TextObservingTemplate> builder)
    {
    }
}

public class YouTubePlyalistObservingTemplateConfiguration : IEntityTypeConfiguration<YouTubePlaylistObservingTemplate>
{
    public void Configure(EntityTypeBuilder<YouTubePlaylistObservingTemplate> builder)
    {
    }
}