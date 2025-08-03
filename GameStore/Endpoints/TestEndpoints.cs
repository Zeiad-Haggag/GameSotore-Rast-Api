using GameStore.Dtos;

namespace GameStore.Endpoints;

public static class TestEndpoints
{
    private static readonly List<GameDto> games =
    [
        new(1, "Stree Fighter 101", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
        new(2, "Stree Fighter 102", "Fighting2", 12.99M, new DateOnly(2000, 7, 15)),
        new(3, "Stree Fighter 103", "Fighting 3", 1.434M, new DateOnly(2020, 7, 15))
    ];

    public static RouteGroupBuilder MapTestEndpoints(this WebApplication app) {
        var group = app.MapGroup("test");

        group.MapGet("/", () => games);

        group.MapGet("/{id}", (int id) => {
            GameDto? game = games.Find(g => g.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        });

        return group;
    }
}
