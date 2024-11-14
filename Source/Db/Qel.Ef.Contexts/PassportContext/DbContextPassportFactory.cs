using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Qel.Ef.Contexts.PassportContext;

public class DbContextPassportFactory() : IDbContextFactory<DbContextPassport>
{
    public DbContextPassport CreateDbContext()
    {
        var confs = new ConfigurationManager();
        confs
        .AddJsonFile("appsettings.json", false, false)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true);

        var options = new DbContextOptionsBuilder<DbContextPassport>().Options;
        return new DbContextPassport(options: options);
    }

    IConfiguration? Configuration { get; set; } = new ConfigurationManager().AddEnvironmentVariables().Build();
}