using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.HttpOverrides;
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
builder.Services.Configure<FrontendOptions>(builder.Configuration.GetSection("Frontend"));
builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection("Email"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);
builder.Services.AddSwaggerGenWithBearer();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // дочерний класс апкастнутый до базового теряет свои поля при сериализации
        // конвертер ниже апкастит в object => поля сохраняются
        options.JsonSerializerOptions.Converters.Add(new ObjectConverter<DiffSummary>());
        options.JsonSerializerOptions.Converters.Add(new ObjectConverter<DiffPayload>());
        options.JsonSerializerOptions.Converters.Add(new ObjectConverter<ObservingPayloadSummary>());
        options.JsonSerializerOptions.Converters.Add(new ObjectConverter<ObservingPayload>());
    });

builder.Services.AddProblemDetails();

builder.Services.AddJwtAuthentication(builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>() 
                                      ?? throw new InvalidOperationException("JwtOptions not configured"));
builder.Services.AddAuthorization();

var app = builder.Build();

await Database.CreateHangfireDatabaseAsync(builder.Configuration);
await Migrator.MigrateAsync(app.Services);

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapHangfireDashboard("/hangfire", new DashboardOptions
    {
        Authorization = [new AnonymousAuthorizaiontFilter()]
    });

app.Run();