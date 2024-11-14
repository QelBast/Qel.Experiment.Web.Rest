using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Qel.Ef.DbClient;

public class RepositoryRegistration<TContext> : IRepositoryRegistration<TContext>
    where TContext : DbContext
{
    private readonly IServiceCollection _services;
    private readonly List<RepositoryOptions> _repositoryOptions;

    public RepositoryRegistration(IServiceCollection services, IEnumerable<RepositoryOptions> repositoryOptions)
    {
        _services = services;
        _repositoryOptions = repositoryOptions.ToList();
    }

    public void AddTransientRepository<TIRepository, TRepository>()
        where TIRepository : class
        where TRepository : class, TIRepository
    {
        _services.AddTransient<TIRepository, TRepository>();

        var name = typeof(TRepository).Name.Replace("`1", string.Empty);
        RepositoryOptions thisOptions = _repositoryOptions.First(x => x.Key == name);
        _services.Configure<RepositoryOptions>($"{typeof(TContext).Name}_{name}", x => 
        {
            x.Key = thisOptions.Key;
            x.DbSetName = thisOptions.DbSetName;
        });
    }

    public void AddScopedRepository<TIRepository, TRepository>()
        where TIRepository : class
        where TRepository : class, TIRepository
    {
        _services.AddScoped<TIRepository, TRepository>();

        var name = typeof(TRepository).Name.Replace("`1", string.Empty);
        RepositoryOptions thisOptions = _repositoryOptions.First(x => x.Key == name);
        _services.Configure<RepositoryOptions>($"{typeof(TContext).Name}_{name}", x =>
        {
            x.Key = thisOptions.Key;
            x.DbSetName = thisOptions.DbSetName;
        });
    }
}