using GameStore.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Games.GetGames;

public static class GetGamesEndpoint
{
    public static void MapGetGames(this IEndpointRouteBuilder app)
    {
        app.MapGet("/",
            (GameStoreContext dbContext) => dbContext.Games
                .Include(game => game.Genre)
                .Select(game => new GameSummeryDto(
                    game.Id,
                    game.Name,
                    game.Genre!.Name,
                    game.Price,
                    game.ReleaseDate))
                .AsNoTracking());
    }
}