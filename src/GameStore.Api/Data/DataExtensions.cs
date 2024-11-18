using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static void InitializeDb(this WebApplication app)
    {
        app.MigrateDb();
        app.SeedDb();
    }
    
    private static void MigrateDb(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) return;

        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        context.Database.Migrate();
    }

    private static void SeedDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        if (dbContext.Genres.Any()) return;
        
        dbContext.Genres.AddRange(
            new Genre { Id = new Guid("3a2f2abc-3a4a-4e1a-b838-8c52590f67e5"), Name = "Fighting" },
            new Genre { Id = new Guid("5299de81-95fe-47b9-8db1-05f4ad52f698"), Name = "Kids and Family" },
            new Genre { Id = new Guid("dcbc0d6e-4e5f-4b92-b950-eeed7a185d7e"), Name = "Racing" },
            new Genre { Id = new Guid("62998639-ff87-4399-85d1-942bfde92e02"), Name = "Roleplaying" },
            new Genre { Id = new Guid("20ee5594-ba79-4a54-922b-ddb59c233aca"), Name = "Sports" }
        );
        dbContext.SaveChanges();
    }
}