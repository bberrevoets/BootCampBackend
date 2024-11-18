using GameStore.Api.Data;

namespace GameStore.Api.Features.Games.UpdateGame;

public static class UpdateGameEndpoint
{
    public static void MapUpdateGame(this IEndpointRouteBuilder app, GameStoreData data)
    {
        app.MapPut("/{id:guid}", (Guid id, UpdateGameDto updatedGame) =>
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
    }
}