using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Qel.Api.Transport;
using Qel.Api.Transport.Behaviours;
using Qel.Api.Transport.RabbitMq.Client;
using Qel.Api.Transport.RabbitMq.Models;
using Qel.Api.Transport.RabbitMq.Extensions;
using Qel.Api.Transport.Generic;
using Microsoft.Extensions.Configuration;

namespace Qel.Common.Console.Hosting.RabbitMq;

public static class HostRabbitMqUtils
{
    public static HostApplicationBuilder PackRabbitMqConsoleApplicationHost(this HostApplicationBuilder builder)
    {
        builder.Logging.AddMyLoggingBuilderConfigures();
        builder.Configuration.AddMyStandartConfigureProviders();

        var senderOps = builder.Configuration.GetRequiredSection(nameof(SenderOptions));
        var receiverOps = builder.Configuration.GetRequiredSection(nameof(ReceiverOptions));

        builder.Services.AddCustomDefaultConsoleServices(
            loggings: builder.Logging)
            .AddSingleton<IStartBehaviour, StartBehaviourRabbitMqConsume>()
            .AddSingleton<IFinishBehaviour, FinishBehaviourRabbitMqPublish>()
            .AddSingleton<ITransportReceiver, Receiver>()
            .AddSingleton<ITransportSender, Sender>()
            .AddHostedService<RabbitMqHostedCustomService>();
        builder.AddRabbitMqCustomClient("RabbitMqClient");
        //builder.Metrics
        
        builder.Services.Configure<SenderOptions>(senderOps)
            .Configure<ReceiverOptions>(receiverOps);
        return builder;
    }
}
