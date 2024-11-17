#region

using System.ComponentModel.DataAnnotations;

#endregion

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

const string getGameEndpointName = "GetGame";

app.MapGet("/games",
    () => games.Select(game => new GameSummeryDto(game.Id, game.Name, game.Genre.Name, game.Price,
        game.ReleaseDate)));

app.MapGet("/games/{id:guid}", (Guid id) =>
{
    var game = games.Find(g => g.Id == id);

    return game is null
        ? Results.NotFound()
        : Results.Ok(new GameDetailsDto(game.Id, game.Name, game.Genre.Id, game.Price, game.ReleaseDate,
            game.Description));
}).WithName(getGameEndpointName);

app.MapPost("/games", (CreateGameDto createGameDto) =>
{
    var genre = genres.Find(genre => genre.Id == createGameDto.GenreId);

    if (genre is null) return Results.BadRequest("Invalid Genre Id");

    var game = new Game
    {
        Name = createGameDto.Name,
        Genre = genre,
        Price = createGameDto.Price,
        ReleaseDate = createGameDto.ReleaseDate,
        Description = createGameDto.Description
    };

    games.Add(game);

    return Results.CreatedAtRoute(getGameEndpointName, new { id = game.Id },
        new GameDetailsDto(game.Id, game.Name, game.Genre.Id, game.Price, game.ReleaseDate,
            game.Description));
}).WithParameterValidation();

app.MapPut("/games/{id:guid}", (Guid id, UpdateGameDto updatedGame) =>
{
    var genre = genres.Find(genre => genre.Id == updatedGame.GenreId);

    if (genre is null) return Results.BadRequest("Invalid Genre Id");

    var existingGame = games.Find(game => game.Id == id);
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
    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();
});

app.MapGet("/genres", () => genres.Select(genre => new GenreDto(genre.Id, genre.Name)));

app.Run();

public record GameDetailsDto(
    Guid Id,
    string Name,
    Guid GenreId,
    decimal Price,
    DateOnly ReleaseDate,
    string Description);

public record GameSummeryDto(Guid Id, string Name, string Genre, decimal Price, DateOnly ReleaseDate);

public record CreateGameDto(
    [Required] [StringLength(50)] string Name,
    Guid GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate,
    [Required] [StringLength(500)] string Description);

public record UpdateGameDto(
    [Required] [StringLength(50)] string Name,
    Guid GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate,
    [Required] [StringLength(500)] string Description);

public record GenreDto(Guid Id, string Name);