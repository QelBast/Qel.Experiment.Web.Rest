namespace Qel.Api.Transport.Behaviours;

public interface IFinishBehaviour
{
    public Task Finish<TOut>(TOut? outObj)
        where TOut : BaseMessage;
}