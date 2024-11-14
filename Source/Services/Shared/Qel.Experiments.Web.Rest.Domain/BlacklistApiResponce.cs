namespace Qel.Experiments.Web.Rest.GatewayApi.Models;

/// <summary>
/// Ответ полученный от сервиса проверки по чёрному списку
/// </summary>
public class BlacklistApiResponce(bool responce)
{
    /// <summary>
    /// Флаг, указывающий на факт содержания заявителя в чёрном списке
    /// </summary>
    public bool Responce { get; set; } = responce;
}