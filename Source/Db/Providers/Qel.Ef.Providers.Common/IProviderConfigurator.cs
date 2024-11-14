using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Qel.Ef.Providers.Common;

public interface IProviderConfigurator
{
    public DbContextOptionsBuilder ConfigureOptionsBuilder(DbContextOptionsBuilder options, IConfiguration configuration);
    public string? Tag { get; init; }

    public string? MigrationsAssemblyName { get; init; }
}
