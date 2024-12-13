using GameStore.Api.Data;
using GameStore.Api.Features.Games.Constants;
using GameStore.Api.Models;

namespace GameStore.Api.Features.Games.CreateGame;

public static class CreateGameEndpoint
{
    public static void MapCreateGame(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (CreateGameDto createGameDto, GameStoreContext dbContext) =>
        {
            var game = new Game
            {
                Name = createGameDto.Name,
                GenreId = createGameDto.GenreId,
                Price = createGameDto.Price,
                ReleaseDate = createGameDto.ReleaseDate,
                Description = createGameDto.Description
            };

            dbContext.Games.Add(game);
            
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(EndpointNames.GetGame, new { id = game.Id },
                new GameDetailsDto(game.Id, game.Name, game.GenreId, game.Price, game.ReleaseDate, game.Description));
        }).WithParameterValidation();
    }
}