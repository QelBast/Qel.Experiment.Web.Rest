using Microsoft.EntityFrameworkCore;
using Qel.Ef.Models.Bases;

namespace Qel.Ef.Models;

/// <summary>
/// Заявка в банк
/// </summary>
[Comment("Заявка в банк")]
public class Request : BaseEntity<long>, ICreationAndModifyTimeBehavior
{
    public int Summa { get; set; }
    public int Period { get; set; }
    public DateTime? CreationTime { get; set; }
    public DateTime? ModifyTime { get; set; }

    public long PersonId { get; set; }
    public RequestedPerson? Person { get; set; }
    public long PassportId { get; set; }
    public RequestedPassport? Passport { get; set; }
}
