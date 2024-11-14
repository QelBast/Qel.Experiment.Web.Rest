using Qel.Api.Transport.Behaviours;

namespace Qel.Api.Transport.Generic;

public interface ITransportReceiver
{
    public Task ReceiveAsync(IProcessBehaviour processer);
}
