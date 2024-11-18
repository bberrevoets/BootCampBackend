using GameStore.Api.Data;
using GameStore.Api.Features.Games.Constants;
using GameStore.Api.Models;

namespace GameStore.Api.Features.Games.CreateGame;

public static class CreateGameEndpoint
{
    public static void MapCreateGame(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", (CreateGameDto createGameDto, GameStoreData data, GameDataLogger logger) =>
        {
            var genre = data.GetGenre(createGameDto.GenreId);

            if (genre is null) return Results.BadRequest("Invalid Genre Id");

            var game = new Game
            {
                Name = createGameDto.Name,
                Genre = genre,
                Price = createGameDto.Price,
                ReleaseDate = createGameDto.ReleaseDate,
                Description = createGameDto.Description
            };

            data.AddGame(game);

            logger.PrintGames();

            return Results.CreatedAtRoute(EndpointNames.GetGame, new { id = game.Id },
                new GameDetailsDto(game.Id, game.Name, game.Genre.Id, game.Price, game.ReleaseDate, game.Description));
        }).WithParameterValidation();
    }
}