using Microsoft.Extensions.Logging;
using Qel.Api.Transport;
using Qel.Api.Transport.Behaviours;
using Qel.Api.Transport.Generic;
using Qel.Api.Transport.RabbitMq.Client.Models;

namespace Qel.Common.Console.Hosting.RabbitMq;

public class ProcessBehaviourRabbitMq(ILogger<ProcessBehaviourRabbitMq> logger) : IProcessBehaviour
{
    readonly ILogger<ProcessBehaviourRabbitMq>? _logger = logger;
    public async Task<BaseMessage?> Process(BaseMessage? inObj)
    {
        _logger?.LogInformation("Processing message '{content}'", inObj?.Content);
        BaseMessage res = new DefaultRabbitMqMessage("");
        
        return default;
    }
}