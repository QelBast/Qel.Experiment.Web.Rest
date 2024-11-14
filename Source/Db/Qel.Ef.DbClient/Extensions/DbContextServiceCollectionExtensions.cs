using System.Reflection;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Qel.Ef.Providers.Common;

namespace Qel.Ef.DbClient.Extensions;

public static class DbContextServiceCollectionExtensions
{
    public static IServiceCollection AddRepository<TIRepository, TRepository, TContext>(
        this IServiceCollection services, 
        IConfiguration configuration, 
        IEnumerable<IProviderConfigurator> providerConfigurators)
        
        where TIRepository : class
        where TRepository : class, TIRepository
        where TContext : DbContext
    {
        return services.AddTransient<TIRepository, TRepository>()
            .AddPooledDbContextFactory<TContext>(builder => 
                {
                    var section = JsonSerializer.Deserialize<DbClientOptions>(
                        configuration.GetRequiredSection($"{typeof(TContext).Name}{typeof(TRepository).Name}").ToString() ?? string.Empty) 
                        ?? new() { DbProvider = "Postgres" };
                    builder.EnableDetailedErrors(section.DetailedErrors)
                        .EnableSensitiveDataLogging(section.SensitiveDataLogging)
                        .EnableServiceProviderCaching(section.ServiceProviderCaching)
                        .EnableThreadSafetyChecks(section.ThreadSafetyChecks);
                    var providerConfiguredOptions = 
                    builder = new ProviderSelector(providerConfigurators).SelectProvider(
                        key: typeof(TContext).Name,
                        builder: builder,
                        config: configuration);
                }
            );
    }

    public static IServiceCollection AddDbClient<TContext>(
        this IServiceCollection services,
        Action<IRepositoryRegistration<TContext>> registerRepositories,
        IConfiguration configuration,
        IEnumerable<IProviderConfigurator> providerConfigurators,
        string? key = null)
        where TContext : DbContext
    {
        key ??= typeof(TContext).Name;

        var section = configuration.GetRequiredSection(key);
        DbClientOptions options = section.Get<DbClientOptions>() ?? 
                throw new NullReferenceException("Ожидался провайдер БД");

        services.AddPooledDbContextFactory<TContext>(builder => 
            {
                builder.EnableDetailedErrors(options.DetailedErrors)
                    .EnableSensitiveDataLogging(options.SensitiveDataLogging)
                    .EnableServiceProviderCaching(options.ServiceProviderCaching)
                    .EnableThreadSafetyChecks(options.ThreadSafetyChecks);
                var providerConfiguredOptions = 
                builder = new ProviderSelector(providerConfigurators).SelectProvider(
                    key: key,
                    builder: builder,
                    config: configuration);
            }
        );

        var registration = new RepositoryRegistration<TContext>(services, options.Repositories);
        registerRepositories(registration);

        return services;
    }
}