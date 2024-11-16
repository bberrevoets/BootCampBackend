using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

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

app.MapGet("/", () => "Hello World!");

app.Run();