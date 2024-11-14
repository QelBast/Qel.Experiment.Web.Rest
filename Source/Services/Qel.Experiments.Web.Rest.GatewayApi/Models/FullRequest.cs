namespace Qel.Experiments.Web.Rest.GatewayApi.Models;

/// <summary>
/// Заявка отправляемая в шлюз
/// </summary>
public class FullRequest
{
    /// <summary>
    /// Паспортные данные заявителя
    /// </summary>
    public required Passport Passport { get; set; }

    /// <summary>
    /// Личность заявителя
    /// </summary>
    public required Person Person { get; set; }

    /// <summary>
    /// Подробности по заявке
    /// </summary>
    public required Request Request { get; set; }
}