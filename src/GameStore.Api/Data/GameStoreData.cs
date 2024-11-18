using GameStore.Api.Models;

namespace GameStore.Api.Data;

public class GameStoreData
{
    private readonly List<Game> _games;

    private readonly List<Genre> _genres =
    [
        new() { Id = new Guid("3a2f2abc-3a4a-4e1a-b838-8c52590f67e5"), Name = "Fighting" },
        new() { Id = new Guid("5299de81-95fe-47b9-8db1-05f4ad52f698"), Name = "Kids and Family" },
        new() { Id = new Guid("dcbc0d6e-4e5f-4b92-b950-eeed7a185d7e"), Name = "Racing" },
        new() { Id = new Guid("62998639-ff87-4399-85d1-942bfde92e02"), Name = "Roleplaying" },
        new() { Id = new Guid("20ee5594-ba79-4a54-922b-ddb59c233aca"), Name = "Sports" }
    ];

    public GameStoreData()
    {
        _games =
        [
            new Game
            {
                Id = Guid.CreateVersion7(),
                Name = "Street Fighter II",
                Genre = _genres[0],
                Price = 19.99m,
                ReleaseDate = new DateOnly(1992, 7, 15),
                Description
                    = "Street Fighter 2, the most iconic fighting game of all time, is back on the Nintendo Switch! The newest iteration of SFII in nearly 10 years, Ultra Street Fighter 2 features all of the classic characters, a host of new single player and multiplayer features, as well as two new fighters: Evil Ryu and Violent Ken!"
            },
            new Game
            {
                Id = Guid.CreateVersion7(),
                Name = "Final Fantasy XIV",
                Genre = _genres[3],
                Price = 59.99m,
                ReleaseDate = new DateOnly(2010, 9, 30),
                Description
                    = "Join over 27 million adventurers worldwide and take part in an epic and ever-changing FINAL FANTASY. Experience an unforgettable story, exhilarating battles, and a myriad of captivating environments to explore."
            },
            new Game
            {
                Id = Guid.CreateVersion7(),
                Name = "FIFA 23",
                Genre = _genres[4],
                Price = 69.99m,
                ReleaseDate = new DateOnly(2022, 9, 27),
                Description
                    = "FIFA 23 brings The World's Game to the pitch, with HyperMotion2 Technology, men's and women's FIFA World Cup™, women's club teams, cross-play features, and more."
            }
        ];
    }

    public IEnumerable<Game> GetGames()
    {
        return _games;
    }

    public Game? GetGame(Guid id)
    {
        return _games.Find(g => g.Id == id);
    }

    public Game AddGame(Game game)
    {
        game.Id = Guid.CreateVersion7();
        _games.Add(game);
        return game;
    }

    public Game UpdateGame(Game game)
    {
        var existingGame = _games.Find(g => g.Id == game.Id);
        if (existingGame == null) throw new KeyNotFoundException($"Game with id {game.Id} not found");

        existingGame.Name = game.Name;
        existingGame.Genre = game.Genre;
        existingGame.Price = game.Price;
        existingGame.ReleaseDate = game.ReleaseDate;
        existingGame.Description = game.Description;

        return existingGame;
    }

    public void DeleteGame(Guid id)
    {
        _games.RemoveAll(game => game.Id == id);
    }

    public IEnumerable<Genre> GetGenres()
    {
        return _genres;
    }

    public Genre? GetGenre(Guid id)
    {
        return _genres.Find(genre => genre.Id == id);
    }
}