using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Qel.Ef.Models.Bases;

namespace Qel.Ef.Contexts.Bases;

public abstract class MyCustomDbContextBase : DbContext
{
    public MyCustomDbContextBase(DbContextOptions options) : base(options: options)
    {
        
    }

    private void UpdateDateTimeProperty()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            var interfaces = entry.Entity.GetType()
                                  .GetInterfaces();

            if (interfaces.Contains(typeof(ICreationAndModifyTimeBehavior)))
            {
                var entity = (ICreationAndModifyTimeBehavior)entry.Entity;
                var timePoint = DateTime.UtcNow;
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreationTime = timePoint;
                        entity.ModifyTime = timePoint;
                        break;
                    case EntityState.Modified:
                        entity.ModifyTime = timePoint;
                        break;
                }
            }
        }
    }

    public override int SaveChanges()
    {
        UpdateDateTimeProperty();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateDateTimeProperty();
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableDetailedErrors()
        .EnableSensitiveDataLogging()
        .EnableServiceProviderCaching()
        .EnableThreadSafetyChecks();
    }
}