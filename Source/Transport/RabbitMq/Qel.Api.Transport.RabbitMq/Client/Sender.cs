using System.Text;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Qel.Api.Transport.Generic;
using Qel.Api.Transport.RabbitMq.Models;

namespace Qel.Api.Transport.RabbitMq.Client;

public class Sender(ILogger<Sender> logger,
    IOptions<SenderOptions> options, 
    ISenderConnection connection) : ITransportSender, IDisposable
{
    readonly ILogger<Sender>? _logger = logger;
    readonly SenderOptions _options = options.Value;
    readonly ISenderConnection _connection = connection;

    bool _disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if(_disposed) {return;}
        if(disposing)
        {
            _connection?.Abort();
            _connection?.Dispose();
            _disposed = true;
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Send(BaseMessage message)
    {
        using var channel = _connection.CreateModel();
        _logger?.LogInformation("Publish message with content '{content}' to {key}", message.Content, _options.RoutingKey);
        channel.BasicPublish(
            exchange: _options.ExchangeName,
            routingKey: _options.RoutingKey,
            mandatory: _options.IsMandatory,
            basicProperties: null,
            body: Encoding.UTF8.GetBytes(message!.ToString() ?? string.Empty)
        );
    }
}
