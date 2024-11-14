namespace Qel.Api.Transport.Behaviours;

public interface IStartBehaviour
{
    public Task<BaseMessage?>? Start<TIn>() 
        where TIn : BaseMessage;
}