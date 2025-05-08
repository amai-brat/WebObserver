using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebObserver.Main.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructure(builder.Configuration);
var app = builder.Build();

app.MapGet("/", () =>
{
    Console.WriteLine(JsonSerializer.Serialize(TimeSpan.FromDays(121)));
    return Results.Ok();
});

app.Run();