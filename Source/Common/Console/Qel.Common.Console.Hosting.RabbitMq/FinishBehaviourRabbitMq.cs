using Qel.Api.Transport;
using Qel.Api.Transport.Behaviours;

namespace Qel.Common.Console.Hosting.RabbitMq;

public class FinishBehaviourRabbitMqPublish : IFinishBehaviour
{
    public Task Finish<TOut>(TOut? outObj) where TOut : BaseMessage
    {
        throw new NotImplementedException();
    }
}