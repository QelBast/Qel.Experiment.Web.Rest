using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Qel.Ef.Models;

namespace Qel.Ef.DbClient;

public class RequestRepository<TContext> : BaseRepository<Request, TContext>, IRequestRepository
    where TContext : DbContext
{
    public RequestRepository(IDbContextFactory<TContext> db, IOptionsSnapshot<RepositoryOptions> options) : base(db, options)
    {
    }

    /// <inheritdoc/>
    public async Task Add(Request request)
    {
        await Entities.AddAsync(request);
        return;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Request>> Get(string firstName, string lastName, string passportSerie, string passportNumber)
    {
        var res = await Entities
            .Include(x => x.Passport)
            .Include(x => x.Person)
            .Where(x => 
                x.Person!.FirstName == firstName &&
                x.Person!.LastName == lastName &&
                x.Passport!.Serie == passportSerie &&
                x.Passport!.Number == passportNumber).ToListAsync();
        return res;
    }
}