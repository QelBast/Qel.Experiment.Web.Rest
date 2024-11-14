namespace Qel.Api.Transport.RabbitMq.Models;


public class ClientOptions
{
    const string DefaultUsername = "guest";
    const string DefaultPassword = "guest";
    const string DefaultHostname = "localhost";
    const int DefaultPort = 5672;

    public string? Hostname { get; init; } = DefaultHostname;
    public int Port { get; init; } = DefaultPort;
    public string? Name { get; init; } = Guid.NewGuid().ToString();
    public required string Username { get; init; } = DefaultUsername;
    public required string Password { get; init; } = DefaultPassword;
}
