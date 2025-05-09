using WebObserver.Main.Domain.Text;

CheckTextGenerator();

return;

void CheckTextGenerator()
{
    var textObservingEntry1 = new TextObservingEntry
    {
        Payload = new TextPayload
        {
            Text = "abobda\nsuka\ndadayaaa\nassss"
        },
    };

    var textObservingEntry2 = new TextObservingEntry
    {
        Payload = new TextPayload
        {
            Text = "aboba\nsuka\ndadaya"
        },
    };

    var diffGenerator = new TextDiffGenerator();
    var diff = diffGenerator.GenerateDiff(textObservingEntry1, textObservingEntry2);
    
    Console.WriteLine("Added:");
    foreach (var added in diff!.Payload.Added)
    {
        Console.WriteLine($"\t{added}");
    }
    
    Console.WriteLine("Removed:");
    foreach (var removed in diff.Payload.Removed)
    {
        Console.WriteLine($"\t{removed}");
    }
}