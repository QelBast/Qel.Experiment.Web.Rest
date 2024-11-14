using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Qel.Api.Transport.Behaviours;
using Qel.Api.Transport.Generic;
using Qel.Api.Transport.RabbitMq.Client.Models;
using Qel.Api.Transport.RabbitMq.Models;

using RabbitMQ.Client.Events;

namespace Qel.Api.Transport.RabbitMq.Client;

public class Receiver(ILogger<Receiver>? logger,
    IOptions<ReceiverOptions> options, 
    IReceiverConnection connection, 
    ITransportSender? sender = null) : ITransportReceiver, IDisposable
{
    readonly ILogger<Receiver>? _logger = logger;
    readonly ReceiverOptions _options = options.Value;
    readonly IReceiverConnection _connection = connection;
    readonly ITransportSender? _sender = sender;
    bool _disposed = false;
    readonly object _lock = new();

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

    public async Task ReceiveAsync(IProcessBehaviour processer)
    {
        var channel = _connection.CreateModel();
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();

            if(body is not null)
            {
                lock (_lock)
                {
                    var message = TransportMessageConverter.GetUTF8String(body) ?? string.Empty;

                    DefaultRabbitMqMessage messageToProcess = new(message);
                    _logger?.LogInformation("Consumed message with content '{content}' from {queue}", message, _options.QueueName);
                    BaseMessage? processedMessage = new(string.Empty);
                    try
                    {
                        processedMessage = processer.Process(messageToProcess).Result;
                    }
                    catch
                    {
                        _logger?.LogWarning("Processing error");
                    }
                    if (!string.IsNullOrEmpty(processedMessage?.Content))
                    {
                        try
                        {
                            _sender?.Send(processedMessage);
                        }
                        catch (Exception ex)
                        {
                            _logger?.LogWarning("Wrong message publishing: {ex}, {site}", ex.Message, ex.TargetSite);
                        }
                        try
                        {
                            if (channel.IsOpen)
                            {
                                channel.BasicAck(ea.DeliveryTag, false);
                            }
                            else
                            {
                                _logger?.LogWarning("Channel closed before acking message.");
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger?.LogWarning("Message acking error: {ex}", ex.Message);
                        }
                    }
                    
                }
            }
        };
        channel.BasicConsume(queue: _options.QueueName,
                            autoAck: _options.IsAutoAck,
                            consumerTag: _options.ConsumerTag,
                            consumer: consumer);
    }
}
