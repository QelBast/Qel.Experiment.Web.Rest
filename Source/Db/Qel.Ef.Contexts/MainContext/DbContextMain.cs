using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Qel.Ef.Contexts.Bases;
using Qel.Ef.Models;
using Qel.Ef.Providers.Common;
using Qel.Ef.Providers.Postgres;

namespace Qel.Ef.Contexts.MainContext;

public class DbContextMain : MyCustomDbContextBase
{
    public DbContextMain(DbContextOptions options) : base(options)
    {

    }

    static List<IProviderConfigurator> Configurators { get; } = [new Configurator(nameof(DbContextMain))];
    IConfiguration? Configuration { get; set; }

    #region Sets
    public DbSet<RequestedPerson>? Persons { get; set; }
    public DbSet<RequestedPassport>? Passports { get; set; }
    public DbSet<Request>? Requests { get; set; }
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
                        x == typeof(Request)
                        || x == typeof(RequestedPassport)
                        || x == typeof(RequestedPerson)
                    )
                );
        }
    }
}
