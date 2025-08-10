using api.MiniCatalogo.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace api.MiniCatalogo.Configuration.Seed
{
    public static class GenerateDb
    {
        public static WebApplicationBuilder Generate(this WebApplicationBuilder build)
        {
            SQLitePCL.Batteries_V2.Init();

            build.Services.AddDbContextFactory<EntityContext>((serviceProvider, options) =>
                options.UseSqlite(build.Configuration["Config:DefaultConnection"]).AddInterceptors(new SqliteForeignKeyEnablerInterceptor()));         

            return build;
        }
        public static void SeedDatabase(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<EntityContext>>();
                using var context = factory.CreateDbContext();
                context.Database.Migrate();
            }
        }
    }
}

public class SqliteForeignKeyEnablerInterceptor : DbConnectionInterceptor
{
    public override void ConnectionOpened(DbConnection connection, ConnectionEndEventData eventData)
    {
        var command = connection.CreateCommand();
        command.CommandText = "PRAGMA foreign_keys=ON;";
        command.ExecuteNonQuery();
    }
}