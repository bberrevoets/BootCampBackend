namespace GameStore.Api.Features.Games.GetGames;

public record GameSummeryDto(Guid Id, string Name, string Genre, decimal Price, DateOnly ReleaseDate);