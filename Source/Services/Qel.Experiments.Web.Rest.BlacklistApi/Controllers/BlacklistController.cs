using Microsoft.AspNetCore.Mvc;
using Qel.Ef.DbClient;

namespace Qel.Experiments.Web.Rest.BlacklistApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlacklistController(ILogger<BlacklistController> logger,
    IBlacklistRepository blacklistRepository
) : ControllerBase
{
    ILogger<BlacklistController> _logger = logger;
    IBlacklistRepository _blacklistRepository = blacklistRepository;
    /// <summary>
    /// Получение паспортных данных по заявителю
    /// </summary>
    /// <param name="firstName">Имя заявителя</param>
    /// <param name="lastName">Фамилия заявителя</param>
    /// <param name="serie">Серия паспорта заявителя</param>
    /// <param name="number">Номер паспорта заявителя</param>
    /// <returns></returns>
    [HttpGet("{firstName}/{lastName}/{serie}/{number}", Name = "CheckIsPersonIntoBlacklist")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<ActionResult> IsInBlacklist(string firstName, string lastName, string serie, string number)
    {
        var isBlackListed = (await _blacklistRepository.GetBlacklistedPeople(firstName, lastName, 
            serie, number)).Count > 0;
        if(isBlackListed)
        {
            return Accepted();
        }
        else
        {
            return NoContent();
        }
    }
}