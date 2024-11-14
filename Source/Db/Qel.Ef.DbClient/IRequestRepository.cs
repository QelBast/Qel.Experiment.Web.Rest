using Qel.Ef.Models;

namespace Qel.Ef.DbClient;

public interface IRequestRepository
{
    /// <summary>
    /// Create new request
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Task Add(Request request);

    /// <summary>
    /// Get all requests for person
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="passportSerie"></param>
    /// <param name="passportNumber"></param>
    /// <returns></returns>
    public Task<IEnumerable<Request>> Get(string firstName, string lastName, string passportSerie, string passportNumber);
}