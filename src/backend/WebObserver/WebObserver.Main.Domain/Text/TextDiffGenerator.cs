using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Services;

namespace WebObserver.Main.Domain.Text;

public class TextDiffGenerator : IDiffGenerator<TextPayload, TextDiffPayload>
{
    public Diff<TextDiffPayload>? GenerateDiff(
        ObservingEntry<TextPayload>? firstEntry,
        ObservingEntry<TextPayload> secondEntry)
    {
        if (firstEntry is null)
        {
            return null;
        }

        var oldText = firstEntry.Payload.Text;
        var newText = secondEntry.Payload.Text;

        var oldLines = SplitIntoLines(oldText);
        var newLines = SplitIntoLines(newText);

        var oldLineCounts = GetLineCounts(oldLines);
        var newLineCounts = GetLineCounts(newLines);

        var added = ComputeAddedLines(newLines, oldLineCounts);
        var removed = ComputeRemovedLines(oldLines, newLineCounts);

        var diffPayload = new TextDiffPayload
        {
            Added = added,
            Removed = removed
        };

        return new Diff<TextDiffPayload>
        {
            Payload = diffPayload
        };
    }

    private static string[] SplitIntoLines(string text)
    {
        return text.Split(["\r\n", "\r", "\n"], StringSplitOptions.None);
    }

    private static Dictionary<string, int> GetLineCounts(string[] lines)
    {
        var counts = new Dictionary<string, int>();
        foreach (var line in lines)
        {
            counts[line] = counts.TryGetValue(line, out var count) ? count + 1 : 1;
        }
        return counts;
    }

    private static List<string> ComputeAddedLines(string[] newLines, Dictionary<string, int> oldLineCounts)
    {
        var added = new List<string>();
        var tempOldCounts = new Dictionary<string, int>(oldLineCounts);
        foreach (var line in newLines)
        {
            if (tempOldCounts.TryGetValue(line, out var count) && count > 0)
            {
                tempOldCounts[line] = count - 1;
            }
            else
            {
                added.Add(line);
            }
        }
        return added;
    }

    private static List<string> ComputeRemovedLines(string[] oldLines, Dictionary<string, int> newLineCounts)
    {
        var removed = new List<string>();
        var tempNewCounts = new Dictionary<string, int>(newLineCounts);
        foreach (var line in oldLines)
        {
            if (tempNewCounts.TryGetValue(line, out var count) && count > 0)
            {
                tempNewCounts[line] = count - 1;
            }
            else
            {
                removed.Add(line);
            }
        }
        return removed;
    }
}