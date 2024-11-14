using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Qel.Ef.Contexts.BlacklistContext;

public class DbContextMainFactory() : IDbContextFactory<DbContextBlacklist>
{
    public DbContextBlacklist CreateDbContext()
    {
        var confs = new ConfigurationManager();
        confs
        .AddJsonFile("appsettings.json", false, false)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true);

        var options = new DbContextOptionsBuilder<DbContextBlacklist>().Options;
        return new DbContextBlacklist(options: options);
    }

    IConfiguration? Configuration { get; set; } = new ConfigurationManager().AddEnvironmentVariables().Build();
}