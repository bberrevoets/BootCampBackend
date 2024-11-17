var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

const string getGameEndpointName = "GetGame";

List<Genre> genres =
[
    new() { Id = new("3a2f2abc-3a4a-4e1a-b838-8c52590f67e5"), Name = "Fighting" },
    new() { Id = new("5299de81-95fe-47b9-8db1-05f4ad52f698"), Name = "Kids and Family" },
    new() { Id = new("dcbc0d6e-4e5f-4b92-b950-eeed7a185d7e"), Name = "Racing" },
    new() { Id = new("62998639-ff87-4399-85d1-942bfde92e02"), Name = "Roleplaying" },
    new() { Id = new("20ee5594-ba79-4a54-922b-ddb59c233aca"), Name = "Sports" }
];

List<Game> games =
[
    new()
    {
        Id          = Guid.CreateVersion7(),
        Name        = "Street Fighter II",
        Genre       = genres[0],
        Price       = 19.99m,
        ReleaseDate = new(1992, 7, 15),
        Description
            = "Street Fighter 2, the most iconic fighting game of all time, is back on the Nintendo Switch! The newest iteration of SFII in nearly 10 years, Ultra Street Fighter 2 features all of the classic characters, a host of new single player and multiplayer features, as well as two new fighters: Evil Ryu and Violent Ken!"
    },
    new()
    {
        Id          = Guid.CreateVersion7(),
        Name        = "Final Fantasy XIV",
        Genre       = genres[3],
        Price       = 59.99m,
        ReleaseDate = new(2010, 9, 30),
        Description
            = "Join over 27 million adventurers worldwide and take part in an epic and ever-changing FINAL FANTASY. Experience an unforgettable story, exhilarating battles, and a myriad of captivating environments to explore."
    },
    new()
    {
        Id          = Guid.CreateVersion7(),
        Name        = "FIFA 23",
        Genre       = genres[4],
        Price       = 69.99m,
        ReleaseDate = new(2022, 9, 27),
        Description
            = "FIFA 23 brings The World's Game to the pitch, with HyperMotion2 Technology, men's and women's FIFA World Cup™, women's club teams, cross-play features, and more."
    }
];

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

app.MapPost("/games", (Game game) =>
{
    game.Id = Guid.CreateVersion7();
    var t = Guid.CreateVersion7();

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

app.MapGet("/genres", () => genres.Select(genre => new GenreDto(genre.Id, genre.Name)));

app.Run();

public record GameDetailsDto(
    Guid     Id,
    string   Name,
    Guid     GenreId,
    decimal  Price,
    DateOnly ReleaseDate,
    string   Description);

public record GameSummeryDto(Guid Id, string Name, string Genre, decimal Price, DateOnly ReleaseDate);

public record GenreDto(Guid Id, string Name);