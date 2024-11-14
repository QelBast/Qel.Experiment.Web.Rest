namespace Qel.Experiments.Web.Rest.GatewayApi.Models;

/// <summary>
/// Заявитель
/// </summary>
public class Person
{
    /// <summary>
    /// Имя заявителя
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Фамилия заявителя
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public required string BirthDate { get; set; }
}