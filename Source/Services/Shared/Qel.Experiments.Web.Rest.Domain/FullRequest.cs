namespace Qel.Experiments.Web.Rest.Domain;

public class FullRequest
{
    public required Person Person { get; set; }
    public required Passport Passport { get; set; }
    public required Request Request { get; set; }
}