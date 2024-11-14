using Microsoft.EntityFrameworkCore;

namespace Qel.Ef.Providers.Postgres;

using System.Reflection;
using Microsoft.Extensions.Configuration;
using Qel.Ef.Providers.Common;

public class Configurator(string contextName) : IProviderConfigurator
{
    public string? Tag { get; init; } = "Postgres";

    public string? MigrationsAssemblyName { get; init; } = "Qel.Ef.DesignTimeUtils";

    public DbContextOptionsBuilder ConfigureOptionsBuilder(DbContextOptionsBuilder options, IConfiguration configuration)
    {
        return options.UseNpgsql(
            configuration.GetRequiredSection(contextName)["Connection"],
            providerOptions => 
            { providerOptions
                .MigrationsAssembly(MigrationsAssemblyName)
                //.EnableRetryOnFailure()
                //.UseAdminDatabase("")
                .UseRelationalNulls();
            }
        );
    }
}