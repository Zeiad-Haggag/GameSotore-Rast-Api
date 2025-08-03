using GameStore.Data;
using GameStore.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("GameStore");

builder.Services.AddDbContext<GameStoreContext>(options => options.UseSqlite(connectionString));
// builder.Services.AddSqlite<GameStoreContext>(connectionString);

var app = builder.Build();

app.MapGameEndpoints();
app.MapGenreEndpoint();
app.MapTestEndpoints();

await app.MigrateDBAsync();

app.Run();