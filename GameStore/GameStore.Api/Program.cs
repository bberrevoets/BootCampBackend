using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres;
using GameStore.Api.Shared.Timing;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");

builder.Services.AddSqlite<GameStoreContext>(connString);

builder.Services.AddHttpLogging(options =>
{
    const HttpLoggingFields flags = HttpLoggingFields.RequestPath | HttpLoggingFields.RequestMethod | HttpLoggingFields.ResponseStatusCode;
    options.LoggingFields = flags;
    options.CombineLogs = true;
});

var app = builder.Build();

app.MapGames();
app.MapGenres();

// app.UseMiddleware<RequestTimingMiddleware>();
app.UseHttpLogging();

await app.InitializeDbAsync();

app.Run();