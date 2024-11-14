using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Qel.Ef.DbClient;

public interface IRepositoryRegistration<TContext>
    where TContext : DbContext
{
    public void AddTransientRepository<TIRepository, TRepository>()
        where TIRepository : class
        where TRepository : class, TIRepository;
    public void AddScopedRepository<TIRepository, TRepository>()
        where TIRepository : class
        where TRepository : class, TIRepository;
}