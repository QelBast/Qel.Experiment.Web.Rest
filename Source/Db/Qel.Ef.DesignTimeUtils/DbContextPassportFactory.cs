using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Qel.Ef.Contexts.PassportContext;
using Qel.Ef.Providers.Common;
using Qel.Ef.Providers.Postgres;

namespace Qel.Ef.Migrations;

public class DbContextPassportFactory : IDesignTimeDbContextFactory<DbContextPassport>
{
    public DbContextPassport CreateDbContext(string[] args)
    {
        var confs = new ConfigurationManager();
        confs
        .AddJsonFile("migrsettings.json", false, false)
        .AddJsonFile($"migrsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true)
        .AddEnvironmentVariables()
        .AddCommandLine(args)
        .Build();

        var builder =  new DbContextOptionsBuilder<DbContextPassport>();
        var providedOptions = new ProviderSelector([new Configurator(nameof(DbContextPassport))]).SelectProvider(
                        key: nameof(DbContextPassport),
                        builder: builder,
                        config: confs);
        
        return new DbContextPassport(providedOptions.Options);
    }
}
