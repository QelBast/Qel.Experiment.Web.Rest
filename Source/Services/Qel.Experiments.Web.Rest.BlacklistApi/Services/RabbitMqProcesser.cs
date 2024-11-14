using System.Net;
using System.Text.Json;
using Qel.Api.Transport;
using Qel.Api.Transport.Behaviours;
using Qel.Experiments.Web.Rest.Domain;
using Qel.Experiments.Web.Rest.GatewayApi.Models;

namespace Qel.Experiments.Web.Rest.BlacklistApi.Services;

/// <summary>
/// Обработчик сообщений полученных из RabbitMq
/// </summary>
/// <param name="logger">Логгер</param>
/// <param name="clientFactory">Фабрика создающая Http-клиент для взаимодействия с Api</param>
[Obsolete("Внутренние API не обмениваются сообщениями через кролика. Это делается через RequestProvider", true)]
public sealed class RabbitMqProcesser(ILogger<RabbitMqProcesser> logger,
    IHttpClientFactory clientFactory) : IProcessBehaviour
{
    private ILogger<RabbitMqProcesser>_logger = logger;
    private IHttpClientFactory _clientFactory = clientFactory;

    /// <inheritdoc/>
    public async Task<BaseMessage?> Process(BaseMessage? inObj)
    {
        using var httpClient = _clientFactory.CreateClient("BlacklistVerifyClient");
        FullRequest content;
        try
        {
            content = JsonSerializer.Deserialize<FullRequest>(inObj?.Content!) 
                ?? throw new InvalidDataException("Пустое сообщение");
        }
        catch (Exception ex)
        {
            _logger.LogError("Ошибка при обработке сообщения {ex}", ex.Message);
            return inObj;
        }
        var message = await httpClient.GetAsync($"api/blacklist/RequesterData/{content.Person.FirstName}/{content.Person.LastName}/{content.Passport.Serie}/{content.Passport.Number}");

        BlacklistApiResponce processResponce; 
        if(message.StatusCode == HttpStatusCode.Accepted)
        {
            processResponce = new BlacklistApiResponce(true);
        }
        else if(message.StatusCode == HttpStatusCode.NoContent)
        {
            processResponce = new BlacklistApiResponce(false);
        }
        else
        {
            processResponce = new BlacklistApiResponce(true);
            _logger.LogWarning("Необрабатываемый StatusCode {sc}", message.StatusCode);
        }
        
        return new BaseMessage(JsonSerializer.Serialize(processResponce));
    }
}