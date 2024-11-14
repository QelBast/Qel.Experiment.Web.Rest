namespace Qel.Experiments.Web.Rest.GatewayApi.Models;

/// <summary>
/// Подробности по заявке
/// </summary>
public class Request
{
    /// <summary>
    /// Сумма кредита
    /// </summary>
    public double Summa { get; set; }   

    /// <summary>
    /// Период кредита
    /// </summary>
    public double Period { get; set; }
}