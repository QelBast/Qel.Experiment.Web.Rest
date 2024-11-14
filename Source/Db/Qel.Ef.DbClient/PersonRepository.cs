using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Qel.Ef.Models;

namespace Qel.Ef.DbClient;

public class PersonRepository<TContext> : BaseRepository<Person, TContext>, IPersonRepository
    where TContext : DbContext
{
    public PersonRepository(IDbContextFactory<TContext> db, IOptionsSnapshot<RepositoryOptions> options) : base(db, options)
    {
    }

    public async Task Add(Person person)
    {
        await DbContext.AddAsync(person);
        await DbContext.SaveChangesAsync();
    }

    public Task Delete(long passportId)
    {
        throw new NotImplementedException();
    }

    public async Task<Person?> Get(Passport passport)
    {
        var persons = Entities.ToList();
        return Entities.FirstOrDefault(x => x.PassportId == passport.Id);
    }

    public Task<Person?> Get(string? passportSerie, string? passportNumber)
    {
        return Entities
            .Include(x => x.Passport)
            .FirstOrDefaultAsync(x => 
                x.Passport!.Serie == passportSerie &&
                x.Passport!.Number == passportNumber);
    }

    public async Task<IEnumerable<Person>> Get()
    {
        return await Entities.ToListAsync();
    }

    public Task Update(Person entityNew)
    {
        throw new NotImplementedException();
    }

    public Task Update(Person entityOld, Person entityNew)
    {
        throw new NotImplementedException();
    }
}