using Microsoft.AspNetCore.Mvc;
using Qel.Ef.DbClient;
using Qel.Ef.Models;

namespace Qel.Experiments.Web.Rest.PassportProviderApi.Controllers;

/// <summary>
/// CRUD-контроллер для управления заявителями
/// </summary>
/// <param name="logger">Логгер</param>
/// <param name="personRepo">Репозитория для работы с заявителями</param>
/// <param name="passportRepo">Репозитория для работы с паспортами заявителей</param>
[ApiController]
[Route("api/[controller]", Name = "Persons")]
public class PersonController(ILogger<PassportController> logger,
    IPersonRepository personRepo, 
    IPassportRepository passportRepo) : ControllerBase
{
    readonly ILogger<PassportController> _logger = logger;
    readonly IPersonRepository _personRepo = personRepo;
    readonly IPassportRepository _passportRepo = passportRepo;

    /// <summary>
    /// Добавление заявителя в БД
    /// </summary>
    /// <param name="person">Заявитель</param>
    /// <returns></returns>
    [HttpPost(Name = "AddPerson")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<IActionResult> Create(Person person)
    {
        await _personRepo.Add(person);
        return CreatedAtAction(nameof(Create), new { firstName = person.FirstName, lastName = person.LastName, passport = person.PassportId }, person );
    }

    /// <summary>
    /// Получение заявителя по его паспорту
    /// </summary>
    /// <param name="passport">Паспорт</param>
    /// <returns>Объект, содержащий данные о заявителе</returns>
    [HttpGet("Passport", Name = "GetPersonByPassport")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<ActionResult<Domain.Person?>> Read(Passport passport)
    {
        var person = await _personRepo.Get(passport);
        Domain.Person res = new() 
        { 
            FirstName = person!.FirstName!,
            LastName = person!.LastName!,
            BirthDate = person!.Birthdate
        };
        return res;
    }

    /// <summary>
    /// Получение заявителя по его паспорту
    /// </summary>
    /// <param name="passportSerie">Серия паспорта</param>
    /// <param name="passportNumber">Номер паспорта</param>
    /// <returns>Объект, содержащий данные о заявителе</returns>
    [HttpGet("{passportSerie}/{passportNumber}", Name = "GetPersonByPassportData")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<ActionResult<Domain.Person?>> Read(string? passportSerie, string? passportNumber)
    {
        try
        {
            var person = await _personRepo.Get(passportSerie, passportNumber);
            Domain.Person res = new() 
            { 
                FirstName = person!.FirstName!,
                LastName = person!.LastName!,
                BirthDate = person!.Birthdate
            };
            return res;
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Получение всех заявителей
    /// </summary>
    /// <returns>Список объектов, содержащих данные о заявителе</returns>
    [HttpGet(Name = "GetAllPersons")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<ActionResult<List<Person>>> Read()
    {
        var persons = await _personRepo.Get();

        if(persons is not null)
        {
            return persons.ToList();
        }
        else
        {
            return NoContent();
        }
        
    }

    /// <summary>
    /// Обновление данных заявителя
    /// </summary>
    /// <param name="person">Заявитель</param>
    /// <returns></returns>
    [HttpPut(Name = "UpdatePerson")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<ActionResult> Update(Person person)
    {
        await _personRepo.Update(person);
        return Ok();
    }

    /// <summary>
    /// Удаление заявителя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns></returns>
    [HttpDelete("{id}", Name = "DeletePerson")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(long id)
    {
        await _personRepo.Delete(id);
        return NoContent();
    }
}