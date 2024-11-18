using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres;

var builder = WebApplication.CreateBuilder(args);

const string connString = "Data Source=GameStore.db";

builder.Services.AddSqlite<GameStoreContext>(connString);

builder.Services.AddTransient<GameDataLogger>();

builder.Services.AddSingleton<GameStoreData>();

var app = builder.Build();

app.MapGames();

app.MapGenres();

app.Run();