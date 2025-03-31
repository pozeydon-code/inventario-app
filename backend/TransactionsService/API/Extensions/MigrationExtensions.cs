using Microsoft.EntityFrameworkCore;
using Infrastructure;

namespace API.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<TransactionDbContext>();

        dbContext.Database.Migrate();
    }
}
