namespace Qel.Experiments.Web.Rest.RequestProvider.Models;

public class HttpClientOptions
{
    public required string Key { get; set; }
    public required string Schema { get; set; }
    public required string Host { get; set; }
    public string? Port { get; set; }
    public required string Address { get; set; }
}
