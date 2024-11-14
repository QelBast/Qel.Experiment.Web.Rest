using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Qel.Ef.DbClient;

[Obsolete("Перегружен. Проще всё же задавать DbContext и сет в реализации")]
public class BaseRepository<T, TContext> : IDisposable 
    where TContext : DbContext
    where T : class
{
    public BaseRepository(IDbContextFactory<TContext> db, IOptionsSnapshot<RepositoryOptions> options)
    {
        Options = options.Get($"{typeof(TContext).Name}_{GetType().Name}".Replace("`1", string.Empty));
        DbContext = db.CreateDbContext();
        var defaultSetName = GetType().Name
            .Replace("Repository", string.Empty)
            .Replace("`1", string.Empty);
        defaultSetName = defaultSetName.Insert(defaultSetName.Length, "s");
        var props = DbContext.GetType()?.GetProperties();
        Options.DbSetName ??= defaultSetName;
        var prop = props!.FirstOrDefault(x => x.Name == Options.DbSetName);
        Entities = (DbSet<T>?)prop?.GetValue(DbContext)! ?? 
            throw new NullReferenceException($"Ошибка получения DbSet из {DbContext.GetType().Name}");
    }

    public RepositoryOptions Options { get; set; } 
    public TContext DbContext { get; }
    public DbSet<T> Entities { get; set; }

    public void Dispose()
    {
        DbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}