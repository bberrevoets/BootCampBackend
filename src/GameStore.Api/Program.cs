#region

using System.ComponentModel.DataAnnotations;
using GameStore.Api.Data;
using GameStore.Api.Features.Games.CreateGame;
using GameStore.Api.Features.Games.GetGame;
using GameStore.Api.Features.Games.GetGames;

#endregion

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

GameStoreData data = new();

app.MapGetGames(data);
app.MapGetGame(data);
app.MapCreateGame(data);

app.MapPut("/games/{id:guid}", (Guid id, UpdateGameDto updatedGame) =>
{
    var genre = data.GetGenre(updatedGame.GenreId);

    if (genre is null) return Results.BadRequest("Invalid Genre Id");

    var existingGame = data.GetGame(id);
    if (existingGame is null) return Results.NotFound();

    existingGame.Name = updatedGame.Name;
    existingGame.Genre = genre;
    existingGame.Price = updatedGame.Price;
    existingGame.ReleaseDate = updatedGame.ReleaseDate;
    existingGame.Description = updatedGame.Description;

    return Results.NoContent();
}).WithParameterValidation();

app.MapDelete("/games/{id:guid}", (Guid id) =>
{
    data.DeleteGame(id);

    return Results.NoContent();
});

app.MapGet("/genres", () => data.GetGenres().Select(genre => new GenreDto(genre.Id, genre.Name)));

app.Run();

public record UpdateGameDto(
    [Required] [StringLength(50)] string Name,
    Guid GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate,
    [Required] [StringLength(500)] string Description);

public record GenreDto(Guid Id, string Name);