namespace Qel.Experiments.Web.Rest.GatewayApi.Models;

/// <summary>
/// Паспортные данные заявителя
/// </summary>
public class Passport
{
    /// <summary>
    /// Серия паспорта
    /// </summary>
    public required string Serie { get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required string Number { get; set; }
}