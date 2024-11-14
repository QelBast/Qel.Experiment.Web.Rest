using Qel.Api.Transport;
using Qel.Api.Transport.Behaviours;
using Qel.Api.Transport.Generic;
using Qel.Api.Transport.RabbitMq.Client.Models;

namespace Qel.Common.Console.Hosting.RabbitMq;

public class StartBehaviourRabbitMqConsume(
    ITransportReceiver receiver,
    IProcessBehaviour processBehaviour
) : IStartBehaviour
{
    readonly ITransportReceiver _receiver = receiver;
    readonly IProcessBehaviour _processer = processBehaviour;

    public async Task<BaseMessage?>? Start<TIn>()
        where TIn : BaseMessage
    {
        _receiver.ReceiveAsync(_processer);
        return null;
    }
}