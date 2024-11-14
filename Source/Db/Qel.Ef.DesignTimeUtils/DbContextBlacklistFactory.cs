using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Qel.Ef.Contexts.BlacklistContext;
using Qel.Ef.Providers.Common;
using Qel.Ef.Providers.Postgres;

namespace Qel.Ef.Migrations;

public class DbContextBlacklistFactory : IDesignTimeDbContextFactory<DbContextBlacklist>
{
    public DbContextBlacklist CreateDbContext(string[] args)
    {
        var confs = new ConfigurationManager();
        confs
        .AddJsonFile("migrsettings.json", false, false)
        .AddJsonFile($"migrsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true)
        .AddEnvironmentVariables()
        .AddCommandLine(args)
        .Build();

        var builder =  new DbContextOptionsBuilder<DbContextBlacklist>();
        var providedOptions = new ProviderSelector([new Configurator(nameof(DbContextBlacklist))]).SelectProvider(
                        key: nameof(DbContextBlacklist),
                        builder: builder,
                        config: confs);
        
        return new DbContextBlacklist(providedOptions.Options);
    }
}
