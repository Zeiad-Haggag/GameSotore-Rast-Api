namespace GameStore.Dtos;

public record class GameSummaryDto(
    int Id, 
    string Name, 
    string GenreName,
    decimal Price, 
    DateOnly ReleaseDate);
