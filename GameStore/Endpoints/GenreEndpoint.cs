using GameStore.Data;
using GameStore.Mapper;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Endpoints;

public static class GenreEndpoint
{
    public static RouteGroupBuilder MapGenreEndpoint(this WebApplication app) {
        var group = app.MapGroup("genres");

        group.MapGet("/", async (GameStoreContext dbContext) => {
            return await dbContext.Genres
                .Select(g => g.ToDto())
                .AsNoTracking()
                .ToListAsync();
        });

        return group;
    }
}
