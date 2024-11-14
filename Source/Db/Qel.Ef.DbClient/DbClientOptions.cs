namespace Qel.Ef.DbClient;

public class DbClientOptions()
{
    public string? Connection { get; set; }
    public required string DbProvider { get; set; }

    public bool DetailedErrors { get; set; } = false;
    public bool SensitiveDataLogging { get; set; } = false;
    public bool ServiceProviderCaching { get; set; } = false;
    public bool ThreadSafetyChecks { get; set; } = false;

    public List<RepositoryOptions>? Repositories { get; set; }
}