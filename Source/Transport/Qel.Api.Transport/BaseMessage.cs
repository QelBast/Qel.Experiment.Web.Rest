namespace Qel.Api.Transport;

public class BaseMessage(string? content)
{
    public string? Content { get; set; } = content;

    public override string ToString()
    {
        return Content ?? string.Empty;
    }
}
