using Microsoft.AspNetCore.Mvc;
using Qel.Ef.DbClient;
using Qel.Ef.Models;

namespace Qel.Experiments.Web.Rest.PassportProviderApi.Controllers;

/// <summary>
/// CRUD-контроллер для управления паспортными данными
/// </summary>
/// <param name="logger">Логгер</param>
/// <param name="passportRepo">Репозитория для работы с паспортными данными</param>
[ApiController]
[Route("api/[controller]", Name = "Passports")]
public class PassportController(ILogger<PassportController> logger,
    IPassportRepository passportRepo) : ControllerBase
{
    readonly ILogger<PassportController> _logger = logger;
    readonly IPassportRepository _passportRepo = passportRepo;

    /// <summary>
    /// Добавление паспортных данных в БД
    /// </summary>
    /// <param name="passport">Паспорт</param>
    /// <returns></returns>
    /// <remarks>
    /// 
    /// </remarks>
    [HttpPost(Name = "AddPassport")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<IActionResult> Create(Passport passport)
    {
        await _passportRepo.Add(passport);
        return CreatedAtAction(nameof(Create), new { serie = passport.Serie, number = passport.Number }, passport );
    }

    /// <summary>
    /// Получение паспортных данных по заявителю
    /// </summary>
    /// <param name="person">Заявитель</param>
    /// <returns></returns>
    [HttpGet("Person", Name = "GetPassportByPerson")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<ActionResult<Passport?>> Read(Person person)
    {
        return await _passportRepo.Get(person);
    }

    /// <summary>
    /// Получение паспортных данных по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetPassportById")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<ActionResult<Passport?>> Read(long id)
    {
        return await _passportRepo.Get(id);
    }

    /// <summary>
    /// Получение паспортных данных по серии и номеру
    /// </summary>
    /// <param name="serie"></param>
    /// <param name="number"></param>
    /// <returns></returns>
    [HttpGet("{serie}/{number}", Name = "GetPassportBySerieAndNumber")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<ActionResult<Passport?>> Read(string serie, string number)
    {
        return await _passportRepo.Get(serie, number);
    }

    /// <summary>
    /// Получение всех зарегистрированных паспортных данных
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name = "GetAllPassports")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<ActionResult<List<Passport>>> Read()
    {
        var passports = await _passportRepo.Get();
        if(passports is not null)
        {
            return passports.ToList();
        }
        else
        {
            return NoContent();
        }
    }

    /// <summary>
    /// Обновление паспортных данных
    /// </summary>
    /// <param name="passport">Паспорт</param>
    /// <returns></returns>
    [HttpPut(Name = "UpdatePassport")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<IActionResult> Update(Passport passport)
    {
        await _passportRepo.Update(passport);
        return NoContent();
    }

    /// <summary>
    /// Удаление паспортных данных из БД
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns></returns>
    [HttpDelete("{id}", Name = "DeletePassport")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(long id)
    {
        await _passportRepo.Delete(id);
        return NoContent();
    }
}