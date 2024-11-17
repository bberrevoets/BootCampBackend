var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

const string getGameEndpointName = "GetGame";

List<Game> games =
[
    new()
    {
        Id          = Guid.CreateVersion7(),
        Name        = "Street Fighter II",
        Genre       = "Fighting",
        Price       = 19.99m,
        ReleaseDate = new(1992, 7, 15)
    },
    new()
    {
        Id          = Guid.CreateVersion7(),
        Name        = "Final Fantasy XIV",
        Genre       = "RolePlaying",
        Price       = 59.99m,
        ReleaseDate = new(2010, 9, 30)
    },
    new()
    {
        Id          = Guid.CreateVersion7(),
        Name        = "FIFA 23",
        Genre       = "Sports",
        Price       = 69.99m,
        ReleaseDate = new(2022, 9, 27)
    }
];

app.MapGet("/games", () => games);

app.MapGet("/games/{id:guid}", (Guid id) =>
{
    var game = games.Find(g => g.Id == id);
    
    return game is null ? Results.NotFound() : Results.Ok(game);
    
}).WithName(getGameEndpointName);

app.MapPost("/games", (Game game) =>
{
    game.Id = Guid.CreateVersion7();
    
    games.Add(game);

    return Results.CreatedAtRoute(getGameEndpointName, new { id = game.Id }, game);
    
}).WithParameterValidation();

app.MapPut("/games/{id:guid}", (Guid id, Game updatedGame) =>
{
    var existingGame = games.Find(g => g.Id == id);
    
    if (existingGame is null)
    {
        return Results.NotFound();
    }

    existingGame.Name        = updatedGame.Name;
    existingGame.Genre       = updatedGame.Genre;
    existingGame.Price       = updatedGame.Price;
    existingGame.ReleaseDate = updatedGame.ReleaseDate;

    return Results.NoContent();

}).WithParameterValidation();

app.MapDelete("/games/{id:guid}", (Guid id) =>
{
    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();
});


app.Run();