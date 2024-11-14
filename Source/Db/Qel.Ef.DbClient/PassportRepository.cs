using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Qel.Ef.Models;

namespace Qel.Ef.DbClient;

public class PassportRepository<TContext> : BaseRepository<Passport, TContext>, IPassportRepository
    where TContext : DbContext
{
    public PassportRepository(IDbContextFactory<TContext> db, IOptionsSnapshot<RepositoryOptions> options) : base(db, options)
    {
    }

    public async Task Add(Passport passport)
    {
        await Entities.AddAsync(passport);
        await DbContext.SaveChangesAsync();
    }

    public async Task Delete(long passportId)
    {
        var passport = await Get(passportId);
        if(passport is not null)
        {
            Entities.Remove(passport);
        }
    }

    public Task<Passport?> Get(Person person)
    {
        return new Task<Passport?>(() => person.Passport);
    }

    public async Task<Passport?> Get(string serie, string number)
    {
        var passport = Entities.FirstOrDefaultAsync(x => x.Serie == serie && x.Number == number);
        return await passport;
    }

    public async Task<Passport?> Get(long id)
    {
        var passport = Entities.FirstOrDefaultAsync(x => x.Id == id);
        return await passport;
    }

    public async Task<IEnumerable<Passport>> Get()
    {
        return await Entities.ToListAsync();
    }

    public async Task Update(Passport passportNew)
    {
        //var passportOld = await Get(passportNew.Id);
        await Task.Run(() => Entities.Update(passportNew));
    }

    public Task Update(Passport passportOld, Passport passportNew)
    {
        throw new NotImplementedException();
    }
}