using Qel.Ef.Models.Bases;

namespace Qel.Ef.Models.BlacklistContext;

public class BlacklistedPassportData : BaseEntity<long>
{
    public required string Serie { get; set; }
    public required string Number { get; set; }
}