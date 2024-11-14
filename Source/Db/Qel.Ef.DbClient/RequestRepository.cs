using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Qel.Ef.Models;

namespace Qel.Ef.DbClient;

public class RequestRepository<TContext> : BaseRepository<Request, TContext>, IRequestRepository
    where TContext : DbContext
{
    private readonly DbSet<RequestedPassport> _dbSetPassports;
    private readonly DbSet<RequestedPerson> _dbSetPersons;
    public RequestRepository(IDbContextFactory<TContext> db, IOptionsSnapshot<RepositoryOptions> options) : base(db, options)
    {
        _dbSetPassports = DbContext.Set<RequestedPassport>();
        _dbSetPersons = DbContext.Set<RequestedPerson>();
    }

    public async Task Add(Request request)
    {
        await Entities.AddAsync(request);
        DbContext.SaveChanges();
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Request>> Get(string firstName, string lastName, string passportSerie, string passportNumber)
    {
        var res = await Entities
            .Where(x => 
                x.Person!.FirstName == firstName &&
                x.Person!.LastName == lastName &&
                x.Passport!.Serie == passportSerie &&
                x.Passport!.Number == passportNumber).ToListAsync();
        return res;
    }
}