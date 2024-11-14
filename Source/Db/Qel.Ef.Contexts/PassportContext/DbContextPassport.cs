using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Qel.Ef.Contexts.Bases;
using Qel.Ef.Models;
using Qel.Ef.Providers.Common;
using Qel.Ef.Providers.Postgres;

namespace Qel.Ef.Contexts.PassportContext;

public class DbContextPassport : MyCustomDbContextBase
{
    public DbContextPassport(DbContextOptions options) : base(options)
    {

    }

    static List<IProviderConfigurator> Configurators { get; } = [new Configurator(nameof(DbContextPassport))];
    IConfiguration? Configuration { get; set; }

    #region Sets
    public DbSet<Person>? Persons { get; set; }
    public DbSet<Passport>? Passports { get; set; }
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
                        x == typeof(Person)
                        || x == typeof(Passport)
                    )
                );
        }
    }
}
