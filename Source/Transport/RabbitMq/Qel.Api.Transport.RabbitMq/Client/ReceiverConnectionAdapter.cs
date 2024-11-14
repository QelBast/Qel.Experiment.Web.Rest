using RabbitMQ.Client.Events;

namespace Qel.Api.Transport.RabbitMq.Client;
public class ReceiverConnectionAdapter : IReceiverConnection
{
    private readonly IConnection _connection;

    public event EventHandler<CallbackExceptionEventArgs> CallbackException;
    public event EventHandler<ConnectionBlockedEventArgs> ConnectionBlocked;
    public event EventHandler<ShutdownEventArgs> ConnectionShutdown;
    public event EventHandler<EventArgs> ConnectionUnblocked;

    public ushort ChannelMax => _connection.ChannelMax;

    public IDictionary<string, object> ClientProperties => _connection.ClientProperties;

    public ShutdownEventArgs CloseReason => _connection.CloseReason;

    public AmqpTcpEndpoint Endpoint => _connection.Endpoint;

    public uint FrameMax => _connection.FrameMax;

    public TimeSpan Heartbeat => _connection.Heartbeat;

    public bool IsOpen => _connection.IsOpen;

    public AmqpTcpEndpoint[] KnownHosts => _connection.KnownHosts;

    public IProtocol Protocol => _connection.Protocol;

    public IDictionary<string, object> ServerProperties => _connection.ServerProperties;

    public IList<ShutdownReportEntry> ShutdownReport => _connection.ShutdownReport;

    public string ClientProvidedName => _connection.ClientProvidedName;

    public int LocalPort => _connection.LocalPort;

    public int RemotePort => _connection.RemotePort;

    public ReceiverConnectionAdapter(IConnection connection)
    {
        _connection = connection;
    }

    // Реализация всех методов и свойств IConnection, делегируя их вызов _connection.
    public void Dispose()
    {
        _connection.Dispose();
        GC.SuppressFinalize(this);
    }

    public IModel CreateModel() => _connection.CreateModel();

    public void UpdateSecret(string newSecret, string reason)
    {
        _connection.UpdateSecret(newSecret, reason);
    }

    public void Abort()
    {
        _connection.Abort();
    }

    public void Abort(ushort reasonCode, string reasonText)
    {
        _connection.Abort(reasonCode, reasonText);
    }

    public void Abort(TimeSpan timeout)
    {
        _connection.Abort(timeout);
    }

    public void Abort(ushort reasonCode, string reasonText, TimeSpan timeout)
    {
        _connection.Abort(reasonCode, reasonText, timeout);
    }

    public void Close()
    {
        _connection.Close();
    }

    public void Close(ushort reasonCode, string reasonText)
    {
        _connection.Close(reasonCode, reasonText);
    }

    public void Close(TimeSpan timeout)
    {
        _connection.Close(timeout);
    }

    public void Close(ushort reasonCode, string reasonText, TimeSpan timeout)
    {
        _connection.Close(reasonCode, reasonText, timeout);
    }

    public void HandleConnectionBlocked(string reason)
    {
        _connection.HandleConnectionBlocked(reason);
    }

    public void HandleConnectionUnblocked()
    {
        _connection.HandleConnectionUnblocked();
    }
}
