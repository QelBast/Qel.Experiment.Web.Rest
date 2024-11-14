using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Qel.Ef.Contexts.Bases;
using Qel.Ef.Models.BlacklistContext;
using Qel.Ef.Providers.Common;
using Qel.Ef.Providers.Postgres;

namespace Qel.Ef.Contexts.BlacklistContext;

public class DbContextBlacklist : MyCustomDbContextBase
{
    public DbContextBlacklist(DbContextOptions options) : base(options)
    {

    }

    static List<IProviderConfigurator> Configurators { get; } = [new Configurator(nameof(DbContextBlacklist))];
    IConfiguration? Configuration { get; set; }

    #region Sets
    public DbSet<BlacklistedPassportData>? BlacklistedPassportData { get; set; }
    public DbSet<BlacklistedPerson>? BlacklistedPeson { get; set; }
    #endregion
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }

    string? EntityConfigurationsAssembly { get; init; } = "Qel.Ef.Models";
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if(EntityConfigurationsAssembly is not null)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.LoadFrom(Path.Combine($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}", $"{EntityConfigurationsAssembly}.dll")),
                type =>
                    type.GenericTypeArguments.Any(x => 
                        x == typeof(BlacklistedPassportData)
                        || x == typeof(BlacklistedPerson)
                    )
                );
        }
    }
}
