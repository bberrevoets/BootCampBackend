namespace GameStore.Api.Features.Games.GetGames;

public record GetGamesDto(int PageNumber = 1, int PageSize = 5);

public record GamesPageDto(int TotalPages, IEnumerable<GameSummeryDto> Data);

public record GameSummeryDto(Guid Id, string Name, string Genre, decimal Price, DateOnly ReleaseDate);