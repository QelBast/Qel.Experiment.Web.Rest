using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Qel.Api.Transport.RabbitMq.Client;
using Qel.Api.Transport.RabbitMq.Models;

namespace Qel.Api.Transport.RabbitMq.Extensions;

public static class RabbitMqDependencyInjectionExtensions
{
    public static IHostApplicationBuilder AddRabbitMqCustomClient(this IHostApplicationBuilder builder, string sectionName = "RabbitMqClient", bool dualConnection = true)
    {
        ClientOptions config = builder.Configuration.GetSection(sectionName).Get<ClientOptions>() ?? throw new NullReferenceException("RabbitMQ client options does not exist");
        if (dualConnection)
        {
            builder.Services.AddSingleton<ISenderConnection>((provider) => new SenderConnectionAdapter(Modeller.CreateConnection(config)));
            builder.Services.AddSingleton<IReceiverConnection>((provider) => new ReceiverConnectionAdapter(Modeller.CreateConnection(config)));
        }
        else
        {
            builder.Services.AddSingleton((provider) => Modeller.CreateConnection(config));
        }
        return builder;
    }

}
