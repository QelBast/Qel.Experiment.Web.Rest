using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Qel.Api.Transport;
using Qel.Api.Transport.Generic;
using Qel.Experiments.Web.Rest.GatewayApi.Models;

namespace Qel.Experiments.Web.Rest.GatewayApi.Controllers;

/// <summary>
/// Контроллер для работы с заявками
/// </summary>
[ApiController]
[Route("api/[controller]", Name = "Requests")]
public class RequestConroller(ILogger<RequestConroller> logger,
ITransportSender sender) : ControllerBase
{
    
    ILogger<RequestConroller> _logger = logger;
    ITransportSender _sender = sender;

    /// <summary>
    /// Отправка на обработку новой заявки
    /// </summary>
    /// <param name="request">Данные по заявке</param>
    /// <returns></returns>
    /// <remarks>
    /// 
    /// </remarks>
    [HttpPost(Name = "SendNewRequest")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<IActionResult> Create(FullRequest request)
    {
        try
        {
            var content = JsonSerializer.Serialize(request);
            var message = new BaseMessage(content);
            _sender.Send(message);
            return CreatedAtAction(nameof(Create), request);
        }
        catch (Exception ex)
        {
            _logger.LogError("Ошибка отправки заявки {ex}", ex.Message);
            return StatusCode(500);
        }
        
    }
}