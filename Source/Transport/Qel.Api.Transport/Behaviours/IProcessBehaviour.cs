namespace Qel.Api.Transport.Behaviours;
public interface IProcessBehaviour
{
    public Task<BaseMessage?> Process(BaseMessage? inObj);
}