using Hangfire;
using WebObserver.Main.API;
using WebObserver.Main.API.Helpers;
using WebObserver.Main.Application;
using WebObserver.Main.Application.Options;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Infrastructure;
using WebObserver.Main.Infrastructure.Data;

var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, "../.env");
DotEnv.Load(dotenv);

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.Configure<YouTubeOptions>(builder.Configuration.GetSection("YouTube"));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);
builder.Services.AddSwaggerGenWithBearer();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new ObjectConverter<DiffSummary>());
        options.JsonSerializerOptions.Converters.Add(new ObjectConverter<ObservingPayloadSummary>());
    });

builder.Services.AddProblemDetails();

builder.Services.AddJwtAuthentication(builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>() 
                                      ?? throw new InvalidOperationException("JwtOptions not configured"));
builder.Services.AddAuthorization();

var app = builder.Build();

await Database.CreateHangfireDatabaseAsync(builder.Configuration);
await Migrator.MigrateAsync(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapHangfireDashboard("/hangfire");
app.Run();