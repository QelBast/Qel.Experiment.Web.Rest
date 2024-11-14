using Microsoft.EntityFrameworkCore;
using Qel.Ef.Models.Bases;

namespace Qel.Ef.Models;

/// <summary>
/// Физические лица, отправляющие заявки в банк
/// </summary>
[Comment("Физические лица, отправляющие заявки в банк")]
public class RequestedPerson : BaseEntity<long>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime Birthdate { get; set; }
}
