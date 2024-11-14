namespace Qel.Experiments.Web.Rest.RequestProvider.Models;

public class VerificationOptions
{
    public double MinCreditSum { get; set; } = 10000;
    public double MaxCreditSum { get; set; } = 10000000;
    public double MinCreditDuration { get; set; } = 3;
    public double MaxCreditDuration { get; set; } = 120;
    public int MinAge { get; set; } = 18;
    public int MaxDebtsCount { get; set; } = 5;
    public int MaxDebtsSum { get; set; } = 20000000;

    public required IEnumerable<HttpClientOptions> HttpClientOptions { get; set; }
}
