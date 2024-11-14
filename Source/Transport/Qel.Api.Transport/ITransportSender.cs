namespace Qel.Api.Transport.Generic;

public interface ITransportSender
{
    public void Send(BaseMessage message);
}
