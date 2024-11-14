using Microsoft.EntityFrameworkCore;
using Qel.Ef.Models.Bases;

namespace Qel.Ef.Models;

/// <summary>
/// Паспортные данные клиента
/// </summary>
[Comment("Паспортные данные клиента")]
public class Passport : BaseEntity<long>
{
    public required string Serie { get; set; }
    public required string Number { get; set; }
}
