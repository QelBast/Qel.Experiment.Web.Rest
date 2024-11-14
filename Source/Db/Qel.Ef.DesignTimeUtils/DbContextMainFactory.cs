using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Qel.Ef.Contexts.MainContext;
using Qel.Ef.Providers.Common;
using Qel.Ef.Providers.Postgres;

namespace Qel.Ef.Migrations;

public class DbContextMainFactory : IDesignTimeDbContextFactory<DbContextMain>
{
    public DbContextMain CreateDbContext(string[] args)
    {
        var confs = new ConfigurationManager();
        confs
        .AddJsonFile("migrsettings.json", false, false)
        .AddJsonFile($"migrsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true)
        .AddEnvironmentVariables()
        .AddCommandLine(args)
        .Build();

        var builder =  new DbContextOptionsBuilder<DbContextMain>();
        var providedOptions = new ProviderSelector([new Configurator(nameof(DbContextMain))]).SelectProvider(
                        key: nameof(DbContextMain),
                        builder: builder,
                        config: confs);
        
        //var options = new DbContextOptionsBuilder<DbContextMain>().Options;
        return new DbContextMain(providedOptions.Options);
    }
}
