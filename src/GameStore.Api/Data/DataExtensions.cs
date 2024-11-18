using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) return;

        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        context.Database.Migrate();
    }
}