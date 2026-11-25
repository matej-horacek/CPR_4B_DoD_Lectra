using api.Data;
using api.Data.Entities;
using api.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var cfg = builder.Configuration;

var connectionString = new NpgsqlConnectionStringBuilder
{
    Host     = cfg["POSTGRES_HOST"] ?? "localhost",
    Port     = int.Parse(cfg["POSTGRES_PORT"] ?? "5432"),
    Database = cfg["POSTGRES_DB"] ?? "lectora",
    Username = cfg["POSTGRES_USER"] ?? "postgres",
    Password = cfg["POSTGRES_PASSWORD"] ?? "postgres",
}.ConnectionString;

// Services
builder.Services.AddDbContext<AppDbContext>(o => o
    .UseNpgsql(connectionString)
    .UseSnakeCaseNamingConvention());

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddHttpClient<IWhisperClient, WhisperClient>((sp, client) =>
{
    var whisperUrl = cfg["WHISPER_URL"] ?? "http://localhost:8000";
    if (!whisperUrl.EndsWith('/'))
    {
        whisperUrl += "/";
    }
    client.BaseAddress = new Uri(whisperUrl);
    // Transcriptions of full lectures can take several minutes
    client.Timeout = TimeSpan.FromMinutes(10);
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapGet("/health", () => Results.Ok());   // Docker healthcheck
app.MapControllers();
app.Run();
