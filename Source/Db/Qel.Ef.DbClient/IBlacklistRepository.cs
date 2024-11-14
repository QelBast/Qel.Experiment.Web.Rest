using Qel.Ef.Models.BlacklistContext;

namespace Qel.Ef.DbClient;

public interface IBlacklistRepository
{
    public Task<List<BlacklistedPerson>> GetBlacklistedPeople(string firstName, string lastName, string serie, string number);
}