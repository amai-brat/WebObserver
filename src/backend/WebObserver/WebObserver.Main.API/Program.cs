using Microsoft.AspNetCore.Authorization;
using WebObserver.Main.API;
using WebObserver.Main.Application;
using WebObserver.Main.Application.Options;
using WebObserver.Main.Infrastructure;
using WebObserver.Main.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddSwaggerGenWithBearer();
builder.Services.AddControllers();
builder.Services.AddProblemDetails();

builder.Services.AddJwtAuthentication(builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>() 
                                      ?? throw new InvalidOperationException("JwtOptions not configured"));

builder.Services.AddAuthorization();

var app = builder.Build();

await Migrator.MigrateAsync(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", [Authorize] () => Results.Ok("Hello"));
app.MapControllers();
app.Run();