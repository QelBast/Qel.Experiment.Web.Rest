using Qel.Ef.Models.Bases;

namespace Qel.Ef.Models.BlacklistContext;

public class BlacklistedPerson : BaseEntity<long>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime Birthdate { get; set; }

    public long PassportId { get; set; }
    public BlacklistedPassportData? Passport { get; set; }
}