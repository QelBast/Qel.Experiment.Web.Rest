using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Qel.Api.Transport.Behaviours;
using Qel.Common.Console.Hosting.Models;

namespace Qel.Common.Console.Hosting.RabbitMq;

public sealed class RabbitMqHostedCustomService(
    ILogger<RabbitMqHostedCustomService> logger,
    HealthCheckService health,
    IStartBehaviour? start,
    IProcessBehaviour? process,
    IFinishBehaviour? finish,
    IOptions<HostedOptions> options,
    IHostApplicationLifetime? lifetime = null
) : HostedCustomService(
        logger, health, start, process, finish, options, lifetime)
{
        
}