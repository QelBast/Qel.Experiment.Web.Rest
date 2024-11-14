using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Qel.Ef.Contexts.MainContext;

public class DbContextMainFactory() : IDbContextFactory<DbContextMain>
{
    public DbContextMain CreateDbContext()
    {
        var confs = new ConfigurationManager();
        confs
        .AddJsonFile("appsettings.json", false, false)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true);

        var options = new DbContextOptionsBuilder<DbContextMain>().Options;
        return new DbContextMain(options: options);
    }

    IConfiguration? Configuration { get; set; } = new ConfigurationManager().AddEnvironmentVariables().Build();
}