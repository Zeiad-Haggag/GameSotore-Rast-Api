using GameStore.Data;
using GameStore.Dtos;
using GameStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Endpoints;

public static class GameEndpoints
{
    public static RouteGroupBuilder MapGameEndpoints(this WebApplication app) {
        var group = app.MapGroup("games");

        group.MapGet("/", async (GameStoreContext dbContext) => 
            await dbContext.Games
                .Include(game => game.Genre)
                .Select(g => g.ToGameSummaryDto())
                .AsNoTracking()
                .ToListAsync()
        );

        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) => {
            Game? game = await dbContext.Games
                .FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        })
        .WithName("GetGameById");

        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) => {
            Game game = newGame.ToEntity();
            // EF will take care of this if we add genreID
            // game.Genre = dbContext.Genres.Find(newGame.GenreId);

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(
                "GetGameById", 
                new { id = game.Id }, 
                game.ToGameDetailsDto()
            );
        })
        .WithParameterValidation();

        group.MapPut("/{id}", async (int id, CreateGameDto updatedGame, GameStoreContext dbContext) => {
            var existingGame = await dbContext.Games.FindAsync(id);

            if(existingGame is null) return Results.NotFound();

            dbContext.Entry(existingGame)
                .CurrentValues
                .SetValues(updatedGame.ToEntity(id));
            
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) => {
            await dbContext.Games
                .Where(game => game.Id == id)
                .ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}
