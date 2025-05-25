using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Services;

namespace WebObserver.Main.Domain.YouTubePlaylist;

public class YouTubePlaylistDiffGenerator : IDiffGenerator
{
    public DiffBase? GenerateDiff(ObservingEntryBase? firstEntry, ObservingEntryBase secondEntry)
    {
        if (firstEntry is not YouTubePlaylistObservingEntry e1 ||
            secondEntry is not YouTubePlaylistObservingEntry e2)
        {
            return null;
        }
        
        var comparer = new YouTubePlaylistItemEqualityComparer();
        
        var firstSet = new HashSet<YouTubePlaylistItem>((e1.Payload as YouTubePlaylistPayload)!.Items);
        var secondSet = new HashSet<YouTubePlaylistItem>((e2.Payload as YouTubePlaylistPayload)!.Items);

        var added = secondSet
            .Where(item2 => firstSet.All(item1 => item1.VideoId != item2.VideoId))
            .ToList();
        var removed = new List<YouTubePlaylistItem>();
        var changed = new List<BeforeAfter<YouTubePlaylistItem>>();
        var unavailable = new List<BeforeAfter<YouTubePlaylistItem>>();
            
        foreach (var item1 in firstSet)
        {
            var item2 = secondSet.FirstOrDefault(x => x.VideoId == item1.VideoId);
            if (item2 is null)
            {
                removed.Add(item1);
                continue;
            }

            if (comparer.Equals(item1, item2))
            {
                continue;
            }

            if (item1.IsAvailable && !item2.IsAvailable)
            {
                unavailable.Add(new BeforeAfter<YouTubePlaylistItem>(item1, item2));
                continue;
            }
            
            changed.Add(new BeforeAfter<YouTubePlaylistItem>(item1, item2));
        }
        
        return new YouTubePlaylistDiff
        {
            FirstEntry = e1,
            SecondEntry = e2,
            Payload = new YouTubePlaylistDiffPayload
            {
                Added = added,
                Removed = removed,
                Changed = changed,
                Unavailable = unavailable
            }
        };
    }
}