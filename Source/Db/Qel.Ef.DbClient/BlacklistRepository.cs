using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Qel.Ef.Models.BlacklistContext;

namespace Qel.Ef.DbClient;

public class BlacklistRepository<TContext> : BaseRepository<BlacklistedPerson, TContext>, IBlacklistRepository
    where TContext : DbContext
{
    public BlacklistRepository(IDbContextFactory<TContext> db, IOptionsSnapshot<RepositoryOptions> options) : base(db, options)
    {
    }
    
    public async Task<List<BlacklistedPerson>> GetBlacklistedPeople(string firstName, string lastName, string serie, string number)
    {
        return await Entities
            .Include(p => p.Passport)
            .Where(x => 
                x.FirstName == firstName && x.LastName == lastName && 
                x.Passport!.Number == number && x.Passport!.Serie == serie)
            .ToListAsync();
    }
}