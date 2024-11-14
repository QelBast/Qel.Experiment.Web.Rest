using Qel.Ef.Models;

namespace Qel.Ef.DbClient;

public interface IPersonRepository
{
    /// <summary>
    /// Add person entity to Db
    /// </summary>
    /// <param name="person"></param>
    /// <returns></returns>
    public Task Add(Person person);

    /// <summary>
    /// Get person entity by passport
    /// </summary>
    /// <param name="passport"></param>
    /// <returns></returns>
    public Task<Person?> Get(Passport passport);

    /// <summary>
    /// Get person entity by passport data
    /// </summary>
    /// <param name="passportSerie"></param>
    /// <param name="passportNumber"></param>
    /// <returns></returns>
    public Task<Person?> Get(string? passportSerie, string? passportNumber);

    /// <summary>
    /// Get all persons
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<Person>> Get();

    /// <summary>
    /// Update passport entity with the same id
    /// </summary>
    /// <param name="entityNew"></param>
    /// <returns></returns>
    public Task Update(Person entityNew);

    /// <summary>
    /// Replace one passport entity with another new
    /// </summary>
    /// <param name="entityOld"></param>
    /// <param name="entityNew"></param>
    /// <returns></returns>
    public Task Update(Person entityOld, Person entityNew);

    /// <summary>
    /// Delete passport entity
    /// </summary>
    /// <returns></returns>
    public Task Delete(long passportId);
}