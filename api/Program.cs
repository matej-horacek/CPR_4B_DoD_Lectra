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

builder.Services.AddScoped<RecordingService>();

builder.Services.AddDbContext<AppDbContext>(o => o
    .UseNpgsql(connectionString)
    .UseSnakeCaseNamingConvention());
builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<AppDbContext>();
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