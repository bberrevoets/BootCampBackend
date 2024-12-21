using GameStore.Api.Data;
using GameStore.Api.Features.Games.Constants;

namespace GameStore.Api.Features.Games.UpdateGame;

public static class UpdateGameEndpoint
{
    public static RouteHandlerBuilder MapUpdateGame(this IEndpointRouteBuilder app)
    {
        return app.MapPut("/{id:guid}", async (Guid id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
            {
                var existingGame = await dbContext.Games.FindAsync(id);

                if (existingGame is null) return Results.NotFound();

                existingGame.Name = updatedGame.Name;
                existingGame.GenreId = updatedGame.GenreId;
                existingGame.Price = updatedGame.Price;
                existingGame.ReleaseDate = updatedGame.ReleaseDate;
                existingGame.Description = updatedGame.Description;

                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            }).WithParameterValidation()
            .WithName(EndpointNames.UpdateGame);
    }
}