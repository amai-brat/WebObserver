﻿using Microsoft.EntityFrameworkCore;
using Npgsql;
using WebObserver.Main.Domain.Text;
using WebObserver.Main.Infrastructure.Data;
using WebObserver.Main.Infrastructure.Data.Repositories;

await CheckRepositoryAsync();

return;

void CheckTextGenerator()
{
    // var textPayload = new TextPayload
    // {
    //     Text = "abobda\nsuka\ndadayaaa\nassss"
    // };
    // var textObservingEntry1 = new TextObservingEntry
    // {
    //     Payload = textPayload,
    //     PayloadSummary = textPayload.CreateSummary(),
    //     DiffSummary = null,
    //     
    // };
    //
    // var textPayload2 = new TextPayload
    // {
    //     Text = "aboba\nsuka\ndadaya"
    // };
    // var textObservingEntry2 = new TextObservingEntry
    // {
    //     Payload = textPayload2,
    //     PayloadSummary = textPayload2.CreateSummary(),
    //     DiffSummary = null
    // };
    //
    // var diffGenerator = new TextDiffGenerator();
    // var diff = diffGenerator.GenerateDiff(textObservingEntry1, textObservingEntry2);
    //
    // Console.WriteLine("Added:");
    // foreach (var added in diff!.Payload.Added)
    // {
    //     Console.WriteLine($"\t{added}");
    // }
    //
    // Console.WriteLine("Removed:");
    // foreach (var removed in diff.Payload.Removed)
    // {
    //     Console.WriteLine($"\t{removed}");
    // }
}

async Task CheckRepositoryAsync()
{
    await using var dbContext = new AppDbContext(CreateOptions());
    var observingRepo = new ObservingRepository(dbContext);

    var observing = await observingRepo.GetByIdWithEntriesSummaryAsync(1);
}

DbContextOptions<AppDbContext> CreateOptions()
{
    var b = new DbContextOptionsBuilder<AppDbContext>();
    b.UseNpgsql(new NpgsqlDataSourceBuilder("User ID=postgres;Password=password;Host=localhost;Port=5432;Database=WebObserver.Main;Pooling=true;Include Error Detail=true")
            .EnableDynamicJson()
            .Build(), 
        o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
    b.EnableSensitiveDataLogging();
    b.UseSnakeCaseNamingConvention();
    
    return b.Options;
}